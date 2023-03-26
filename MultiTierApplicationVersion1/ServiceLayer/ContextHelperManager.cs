using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ContextHelperManager
    {
        private static OnlineShopDbContext CurrentContext;

        private static CustomerContext CurrentCustomerContext;

        private static ProductContext CurrentProductContext;

        private static OrderContext CurrentOrderContext;

        public static OnlineShopDbContext GenerateDbContext()
        {
            CurrentContext = new OnlineShopDbContext();
            return CurrentContext;
        }

        public static OnlineShopDbContext GetDbContext()
        {
            if (CurrentContext != null)
            {
                return CurrentContext;
            }
            return GenerateDbContext();
        }

        public static CustomerContext GenerateCustomerContext(OnlineShopDbContext dbContext)
        {
            CurrentCustomerContext = new CustomerContext(dbContext);
            return CurrentCustomerContext;
        }

        public static CustomerContext GetCustomerContext(OnlineShopDbContext dbContext)
        {
            if (CurrentCustomerContext != null)
            {
                return CurrentCustomerContext;
            }

            return GenerateCustomerContext(dbContext);
        }

        private static ProductContext GenerateProductContext(OnlineShopDbContext dbContext)
        {
            CurrentProductContext = new ProductContext(dbContext);

            return CurrentProductContext;
        }

        public static ProductContext GetProductContext(OnlineShopDbContext dbContext)
        {
            if (CurrentProductContext != null)
            {
                return CurrentProductContext;
            }

            return GenerateProductContext(dbContext);
        }

        public static OrderContext GenerateOrderContext(OnlineShopDbContext dbContext)
        {
            CurrentOrderContext = new OrderContext(dbContext);

            return CurrentOrderContext;
        }

        public static OrderContext GetOrderContext(OnlineShopDbContext dbContext)
        {
            if (CurrentOrderContext != null)
            {
                return CurrentOrderContext;
            }

            return GenerateOrderContext(dbContext);
        }
        
    }
}
