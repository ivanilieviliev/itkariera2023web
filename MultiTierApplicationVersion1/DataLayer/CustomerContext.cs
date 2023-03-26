using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CustomerContext : IDb<Customer, string>
    {
        private OnlineShopDbContext dbContext;

        public CustomerContext(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Customer item)
        {
            try
            {
                dbContext.Customers.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer> ReadAsync(string key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Customer> query = dbContext.Customers;

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Products)
                        .Include(c => c.Orders)
                            .ThenInclude(o => o.ProductsOrders)
                                .ThenInclude(po => po.Product);
                }

                return await query.FirstOrDefaultAsync(c => c.Id == key);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Customer>> ReadAllAsync(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Customer> query = dbContext.Customers;

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Products)
                        .Include(c => c.Orders)
                            .ThenInclude(o => o.ProductsOrders)
                                .ThenInclude(po => po.Product);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Customer item, bool useNavigationProperties = false)
        {
            try
            {
                // I way:
                Customer customerFromDb = await ReadAsync(item.Id);
                customerFromDb.Name = item.Name;
                customerFromDb.Age = item.Age;

                if (useNavigationProperties)
                {
                    List<Product> products = new List<Product>();

                    foreach (Product p in item.Products)
                    {
                        Product productFromDb = dbContext.Products.Find(p.Id);

                        if (productFromDb != null)
                        {
                            products.Add(productFromDb);
                        }
                        else
                        {
                            products.Add(p);
                        }
                    }

                    customerFromDb.Products = products;
                }

                // II way:
                //dbContext.Customers.Update(item);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                dbContext.Customers.Remove(await ReadAsync(key));
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CustomerExists(string id)
        {
            return await dbContext.Customers.AnyAsync(e => e.Id == id);
        }
    }
}
