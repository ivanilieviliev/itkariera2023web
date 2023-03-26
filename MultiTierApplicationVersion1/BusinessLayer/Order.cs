using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BusinessLayer
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Customer")]
        public Customer Customer { get; set; }

        [Required]
        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }

        // I way: cannot be used with Entitiy Framework
        //public List<Product> Products { get; set; }

        //public List<int> ProductsQuantities { get; set; }

        // II way: :)
        public List<ProductsOrders> ProductsOrders { get; set; }

        public Order()
        {
            ProductsOrders = new List<ProductsOrders>();
        }

        public Order(Customer customer)
        {
            Customer = customer;
            CustomerId = customer.Id;
            ProductsOrders = new List<ProductsOrders>();
        }

        public Order(int id, Customer customer)
            :this(customer)
        {
            Id = id;
        }

        // Won't use it. Just to show you:
        private decimal CalculatePrice()
        {
            return ProductsOrders.Sum(po => po.Quantity * po.Product.Price);
        }

    }
}