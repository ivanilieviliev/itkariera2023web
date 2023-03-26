using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Hardware : Product
    {
        // Value Types are [Required] / not null by default!
        public double Weight { get; set; }

        public Hardware() : base()
        {

        }

        public Hardware(string name, decimal price, int quantity, double weight, double? rating = null) 
            : base(name, price, quantity, rating)
        {
            Weight = weight;
        }

        public Hardware(int id, string name, decimal price, int quantity, double weight, double? rating = null) 
            : base(id, name, price, quantity, rating)
        {
            Weight = weight;
        }
    }
}
