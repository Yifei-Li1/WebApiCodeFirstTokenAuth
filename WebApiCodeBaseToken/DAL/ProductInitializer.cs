using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApiCodeBaseToken.Models;

namespace WebApiCodeBaseToken.DAL
{
    public class ProductInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            var Products = new List<Product>
            {
                new Product{Name = "Microwave",Manufacturer="manufacture1",Price= 70.99m,ImgPath = "~/Content/temppath"},
                new Product{Name = "Refrigerator", Manufacturer="Manufacturer2", Price= 150.49m, ImgPath = "~/Content/Images/Refrigerator.jpg"},
                new Product{Name = "Dishwasher", Manufacturer="Manufacturer3", Price= 120.00m, ImgPath = "~/Content/Images/Dishwasher.jpg"},
                new Product{Name = "Toaster", Manufacturer="Manufacturer4", Price= 35.75m, ImgPath = "~/Content/Images/Toaster.jpg"}
            };
            Products.ForEach(e => context.Products.Add(e));
            context.SaveChanges();
            var Users = new List<User>
            {
                new User{Username = "first@123.com", Password = "1234567"},
                new User{Username = "second@123.com", Password = "1234567"},
                new User{Username = "third@123.com", Password = "1234567"},
            };
            Users.ForEach(e => context.Users.Add(e));
            context.SaveChanges();
        }
    }
  
}