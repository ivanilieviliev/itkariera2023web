using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ProductViewModel
    {
        // , double Weight
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

        public bool IsRated { get; set; }

        public ProductType Type { get; set; }

        // Software
        public string License { get; set; }

        public string Version { get; set; }

        // Hardware
        public double Weight { get; set; }

        public ProductViewModel()
        {

        }
    }
}
