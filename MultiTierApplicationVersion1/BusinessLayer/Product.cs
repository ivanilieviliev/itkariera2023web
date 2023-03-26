using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public abstract class Product
    {
        [Key]
        public int Id { get; protected set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 30000, ErrorMessage = "Price must be between 0 and 30000!")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100!")]
        public int Quantity { get; set; }

        // nullable!
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        public double? Rating { get; set; }

        // You must add this property so Entity Framework can make many-to-many relationship.
        // Otherwise it will be one-to-many!
        public List<Customer> Customers { get; set; }

        protected Product()
        {
            Customers = new List<Customer>();
        }

        protected Product(string name, decimal price, int quantity, double? rating = null)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Rating = rating;
            Customers = new List<Customer>();
        }

        protected Product(int id, string name, decimal price, int quantity, double? rating = null)
        : this(name, price, quantity, rating)
        {
            Id = id;
        }
    }
}
