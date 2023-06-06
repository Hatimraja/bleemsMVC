using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bleemsMVC.Data;
using bleemsMVC.Models;
using NToastNotify;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace BleemsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly bleemsContext db;

        public ProductsController(bleemsContext context)
        {
            db = context;
        }

        [HttpGet("GetProductsListRandomOrder")]
        public IActionResult GetProductsListRandomOrder()
        {
            var products = db.Products.OrderBy(p => Guid.NewGuid())
                                     .Select(p => new
                                     {
                                         p.Name,
                                         p.Description,
                                         CategoryName = p.Category.CategoryName
                                     })
                                     .ToList();

            return Ok(products);
        }

        [HttpGet("GetProductDetailsById/{productId}")]
        public IActionResult GetProductDetailsById(int productId)
        {
            var product = db.Products.Include(p => p.Category)
                                    .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var productDetails = new
            {
                product.Name,
                product.Description,
                CategoryName = product.Category.CategoryName
            };

            return Ok(productDetails);
        }

        [HttpGet("details/{productId}")]
        public IActionResult GetProductDetailsByIdWithCurrency(int productId)
        {
            var product = db.ProductDetails.FromSqlRaw($"EXEC GetItemDetails {productId}")
                                                  .AsEnumerable()
                                                  .FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


    }
}