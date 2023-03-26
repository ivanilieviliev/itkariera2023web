using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderManager
    {
        private readonly OrderContext orderContext;

        public OrderManager(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await orderContext.CreateAsync(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> ReadAsync(int id, bool useNavigationalProperties = false)
        {
            try
            {
                return await orderContext.ReadAsync(id, useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await orderContext.ReadAllAsync(useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Order order, bool useNavigationalProperties = false)
        {
            try
            {
                await orderContext.UpdateAsync(order, useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await orderContext.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> OrderExists(int id)
        {
            return await orderContext.OrderExists(id);
        }

    }
}
