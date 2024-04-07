using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCodeBaseToken.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string ImgPath { get; set; }
        

    }
}