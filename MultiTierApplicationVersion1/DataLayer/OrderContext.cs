using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderContext : IDb<Order, int>
    {
        private OnlineShopDbContext dbContext;

        public OrderContext(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Order item)
        {
            try
            {
                Customer customerFromDb = await dbContext.Customers.FindAsync(item.CustomerId);

                if (customerFromDb != null)
                {
                    item.Customer = customerFromDb;
                }

                dbContext.Orders.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> ReadAsync(int key, bool useNavigationProperties = false)
        {
            try
            {
                if (useNavigationProperties)
                {
                    return await dbContext.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.ProductsOrders)
                            .ThenInclude(po => po.Product)
                        .FirstOrDefaultAsync(o => o.Id == key);
                }
                else
                {
                    return await dbContext.Orders.FindAsync(key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> ReadAllAsync(bool useNavigationProperties = false)
        {
            try
            {
                // The Way of Ivan Iliev: :)
                IQueryable<Order> query = dbContext.Orders;

                if (useNavigationProperties)
                {
                    query = query.Include(o => o.Customer)
                        .Include(o => o.ProductsOrders)
                            .ThenInclude(po => po.Product);
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Order item, bool useNavigationProperties)
        {
            try
            {
                Order orderFromDb = await ReadAsync(item.Id, useNavigationProperties);

                if (useNavigationProperties)
                {
                    Customer customerFromDb = await dbContext.Customers.FindAsync(item.CustomerId);

                    if (customerFromDb != null)
                    {
                        orderFromDb.Customer = customerFromDb;
                    }
                    else
                    {
                        orderFromDb.Customer = item.Customer;
                    }

                    List<ProductsOrders> productsOrders = new List<ProductsOrders>();
                    item.Price = 0;

                    foreach (ProductsOrders po in item.ProductsOrders)
                    {
                        ProductsOrders poFromDb = await dbContext.ProductsOrders.Include(po => po.Product)
                            .FirstOrDefaultAsync(po => po.ProductId == po.ProductId && po.OrderId == po.OrderId);

                        if (poFromDb != null)
                        {
                            item.Price = item.Price + poFromDb.Product.Price * poFromDb.Quantity;
                            productsOrders.Add(poFromDb);
                        }
                        else
                        {
                            item.Price = item.Price + po.Product.Price * po.Quantity;
                            productsOrders.Add(po);
                        }
                    }

                    item.ProductsOrders = productsOrders;
                }

                // II way: Use only this line and SaveChanges()
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
                dbContext.Orders.Remove(await ReadAsync(key));
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> OrderExists(int id)
        {
            return await dbContext.Orders.AnyAsync(e => e.Id == id);
        }

    }
}
