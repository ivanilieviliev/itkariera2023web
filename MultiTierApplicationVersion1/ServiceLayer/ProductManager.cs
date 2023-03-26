using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductManager
    {
        private readonly ProductContext productContext;

        public ProductManager(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                await productContext.CreateAsync(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> ReadAsync(int id, bool useNavigationalProperties = false)
        {
            try
            {
                return await productContext.ReadAsync(id, useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> ReadAllAsync(bool useNavigationalProperties = false)
        {
            try
            {
                return await productContext.ReadAllAsync(useNavigationalProperties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Product product, bool useNavigationalProperties = false)
        {
            try
            {
                await productContext.UpdateAsync(product, useNavigationalProperties);
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
                await productContext.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ProductExists(int id)
        {
            return await productContext.ProductExists(id);
        }

    }
}
