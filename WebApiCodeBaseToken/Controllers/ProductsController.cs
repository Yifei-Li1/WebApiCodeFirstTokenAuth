using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiCodeBaseToken.DAL;
using WebApiCodeBaseToken.Models;
using WebApiCodeBaseToken.CustomFilters;
using System.Web.Http.Cors;


namespace WebApiCodeBaseToken.Controllers
{
    [CustomExceptionFilter]
    public class ProductsController : ApiController
    {
        private ProductContext db = new ProductContext();

        // GET: api/Products
        [HttpGet]
        [Route("api/products")]
        [Authorize]
        public HttpResponseMessage GetProducts()
        {
            //try
            //{
                var products = db.Products;
                
                if(products == null || products.Count() == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);  // HTTPResponseException 
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, products);
                }
               

            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            //}


        }

        // GET: api/Products/5
        [HttpGet]
        [Route("api/product/{id}")]
        [ResponseType(typeof(Product))]
        [Authorize]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            try
            {
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return Content(HttpStatusCode.NotFound, "Product not found.");
                }
                return Ok(product);
            }
            catch (System.Data.Entity.Core.EntityException) // Catch entity framework related exceptions
            {
                // Log the exception here
                return InternalServerError(new Exception("An error occurred while accessing the database. Please try again later."));
            }
            catch (System.OperationCanceledException) // Catch task cancellation exceptions
            {
                // Log the exception here
                return StatusCode(HttpStatusCode.RequestTimeout); // Or another appropriate status code
            }
            catch (Exception ex) // General exception catch block for unexpected errors
            {
                // Log the exception here
                return InternalServerError(new Exception("An unexpected error occurred. Please try again later."));
            }
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Admin,User")]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}