using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace OnlineStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Please Enter a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter a Descriptipn")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter a positive price")]
        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please Enter a Category")]
        public string Category { get; set; }
    }
}
