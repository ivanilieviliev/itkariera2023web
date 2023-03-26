using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Software : Product
    {
        [Required]
        public string Version { get; set; }

        [Required]
        public string License { get; set; }

        public Software() : base()
        {

        }

        public Software(string name, decimal price, int quantity, string version, string license, double? rating = null) 
            : base(name, price, quantity, rating)
        {
            Version = version;
            License = license;
        }

        public Software(int id, string name, decimal price, int quantity, string version, string license, double? rating = null) 
            : base(id, name, price, quantity, rating)
        {
            Version = version;
            License = license;
        }
    }
}
