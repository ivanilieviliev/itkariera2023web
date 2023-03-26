using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductsOrdersContext : IDb<ProductsOrders, object[]>
    {
        private readonly OnlineShopDbContext dbContext;

        public ProductsOrdersContext(OnlineShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(ProductsOrders item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get ProductOrder by productId and orderId. :)
        /// </summary>
        /// <param name="key">(int productId, int orderId)</param>
        /// <param name="useNavigationProperties">Load Product's information</param>
        /// <returns>Hover the mouse over the method to see this text! :)</returns>
        public async Task<ProductsOrders> ReadAsync(object[] key, bool useNavigationProperties = false)
        {
            int productId = Convert.ToInt32(key[0]);
            int orderId = Convert.ToInt32(key[1]);

            IQueryable<ProductsOrders> query = dbContext.ProductsOrders;

            if (useNavigationProperties)
            {
                query = query.Include(po => po.Product);
            }

            return await query.FirstOrDefaultAsync(po => po.ProductId == productId && po.OrderId == orderId);
        }

        public async Task<List<ProductsOrders>> ReadAllAsync(bool useNavigationProperties = false)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ProductsOrders item, bool useNavigationProperties = false)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(object[] key)
        {
            throw new NotImplementedException();
        }
    }
}
