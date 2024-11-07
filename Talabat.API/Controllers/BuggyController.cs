using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly StoreDbContext dbcontext;

        public BuggyController(StoreDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = dbcontext.Set<Product>().Find(100);
            
            if(product is null)
                return NotFound(new APIResponse(404));
            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerErrorRequest()
        {
            var product = dbcontext.Set<Product>().Find(100);

            var productToReturn = product.ToString();

            return Ok(productToReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("BadRequest/{Id}")]
        public ActionResult GetBadRequest(int Id)
        {
            return Ok(); 
        }

        [HttpGet("Unauthorized")]
        public ActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(new APIResponse(401));
        }
    }
}
