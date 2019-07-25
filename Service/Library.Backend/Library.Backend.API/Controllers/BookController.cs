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
    public class BookController : Controller
    {
        readonly IBookBL _bookBL;

        public BookController(IBookBL bookBL)
        {
            _bookBL = bookBL;
        }

        [HttpGet]
        [Produces(typeof(Response<Book>))]
        public IActionResult Get()
        {
            return Ok(_bookBL.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Response<Book>))]
        public IActionResult GetxId(string id)
        {
            return Ok(_bookBL.Get(id));
        }

        [HttpPost]
        [Produces(typeof(Response<Book>))]
        public IActionResult Post([FromBody]BookRequest request)
        {
            return Ok(_bookBL.Create(request));
        }

        [HttpPut("{id}")]
        [Produces(typeof(Response<Book>))]
        public IActionResult Put(string id, [FromBody]BookRequest request)
        {
            return Ok(_bookBL.Update(id, request));
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Response<Book>))]
        public IActionResult Delete(string id)
        {
            return Ok(_bookBL.Delete(id));
        }
    }
}