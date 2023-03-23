using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,1000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }
        [Required]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
        public string ImageUrl { get; set; }

        //Set FK
        [Required]
        public Category CategoryId { get; set; }
        //Nav Prty
        public Category Category { get; set; }

        //set FK
        [Required]
        public CoverType CoverTypeId { get; set; }
        //set Nav 
        public CoverType CoverType { get; set; }


    }
}
