using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductContext : IDb<Product, int>
    {
        OnlineShopDbContext dbContext;

        public ProductContext(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Product item)
        {
            try
            {
                dbContext.Products.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> ReadAsync(int key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Product> query = dbContext.Products;

                if (useNavigationProperties)
                {
                    query = query.Include(p => p.Customers);
                }

                return await query.FirstOrDefaultAsync(p => p.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> ReadAllAsync(bool useNavigationProperties)
        {
            try
            {
                IQueryable<Product> query = dbContext.Products;

                if (useNavigationProperties)
                {
                    query = query.Include(p => p.Customers);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Product item, bool useNavigationProperties)
        {
            try
            {
                Product productFromDb = await dbContext.Products.FindAsync(item.Id);

                productFromDb.Name = item.Name;
                productFromDb.Price = item.Price;
                productFromDb.Quantity = item.Quantity;
                productFromDb.Rating = item.Rating;

                if (productFromDb is Hardware)
                {
                    (productFromDb as Hardware).Weight = (item as Hardware).Weight;
                }
                else
                {
                    (productFromDb as Software).License = (item as Software).License;
                    (productFromDb as Software).Version = (item as Software).Version;
                }

                if (useNavigationProperties)
                {
                    List<Customer> customers = new List<Customer>();

                    foreach (Customer c in item.Customers)
                    {
                        Customer customerFromDb = dbContext.Customers.Find(c.Id);

                        if (customerFromDb != null)
                        {
                            customers.Add(customerFromDb);
                        }
                        else
                        {
                            customers.Add(c);
                        }

                    }

                    productFromDb.Customers = customers;

                }


                //dbContext.Update(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                dbContext.Products.Remove(await ReadAsync(key));
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ProductExists(int id)
        {
            return await dbContext.Products.AnyAsync(e => e.Id == id);
        }
    }
}
