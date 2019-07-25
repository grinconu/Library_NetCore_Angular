namespace Library.Backend.API.Controllers
{
    using Library.Backend.Contracts.Business;
    using Library.Backend.Entities.Models;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    //[Authorize]
    public class AuthorController : Controller
    {
        readonly IAuthorBL _authorBL;

        public AuthorController(IAuthorBL authorBL)
        {
            _authorBL = authorBL;
        }
        
        [HttpGet]
        [Produces(typeof(Response<Author>))]
        public IActionResult Get()
        {
            return Ok(_authorBL.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Response<Author>))]
        public IActionResult GetxId(string id)
        {
            return Ok(_authorBL.Get(id));
        }

        [HttpPost]
        [Produces(typeof(Response<Author>))]
        public IActionResult Post([FromBody]AuthorRequest request)
        {
            return Ok(_authorBL.Create(request));
        }

        
        [HttpPut("{id}")]
        [Produces(typeof(Response<Author>))]
        public IActionResult Put(string id, [FromBody]AuthorRequest request)
        {
            return Ok(_authorBL.Update(id, request));
        }

        
        [HttpDelete("{id}")]
        [Produces(typeof(Response<Author>))]
        public IActionResult Delete(string id)
        {
            return Ok(_authorBL.Delete(id));
        }
    }
}