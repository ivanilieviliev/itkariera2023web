using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Required]
        [Range(0, 130, ErrorMessage = "You must be older! You will get free products! [|:)~")]
        public int Age { get; set; }

        public List<Order> Orders { get; set; }

        public List<Product> Products { get; set; }

        public Customer()
        {
            //Look in CustomersController => Create() and use one of the methods!
            //Id = Guid.NewGuid().ToString();
            Orders = new List<Order>();
            Products = new List<Product>();
        }

        public Customer(string name, int age = 0)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Age = age;
            Orders = new List<Order>();
            Products = new List<Product>();
        }

    }
}
