using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CustomerManager
    {
        private readonly CustomerContext customerContext;

        public CustomerManager(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }

        public async Task CreateAsync(Customer customer)
        {
            try
            {
                await customerContext.CreateAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer> ReadAsync(string id, bool useNavigationalProperties = false)
        {
            try
            {
                return await customerContext.ReadAsync(id, useNavigationalProperties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Customer>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await customerContext.ReadAllAsync(useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Customer customer, bool useNavigationalProperties = false)
        {
            try
            {
                await customerContext.UpdateAsync(customer, useNavigationalProperties);
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
                await customerContext.DeleteAsync(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CustomerExists(string id)
        {
            return await customerContext.CustomerExists(id);
        }

    }
}
