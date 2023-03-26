using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    // Look in DatabaseLayer OnModelCreating() for the configuration!
    public class ProductsOrders
    {
        public int ProductId { get; set; }

        //II way:
        //[ForeignKey("Order")]
        public int OrderId { get; set; }

        // Value Types are [Required] / not null! by default!
        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100!")]
        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        public ProductsOrders()
        {

        }

        private ProductsOrders(int productId, int orderId, int quantity)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }

        public ProductsOrders(int productId, int orderId, int quantity, Product product)
            : this(productId, orderId, quantity)
        {
            Product = product;
        }

    }
}
