using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bleemsMVC.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
