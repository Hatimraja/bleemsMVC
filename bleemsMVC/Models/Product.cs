using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bleemsMVC.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        [Display(Name = "Product Name")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Description Name is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]

        public decimal? Price { get; set; }
        public string? Photo { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "CategoryId is required")]
        public int? CategoryId { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Category? Category { get; set; }



        [NotMapped]
        [Display(Name = "Supportive PDF * ")]
        public IFormFile contractdoc { get; set; }
    }
}
