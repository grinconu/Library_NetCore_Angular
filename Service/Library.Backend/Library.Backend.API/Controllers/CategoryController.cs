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
    public class CategoryController : Controller
    {

        readonly ICategoryBL _categoryBL;

        public CategoryController(ICategoryBL categoryBL)
        {
            _categoryBL = categoryBL;
        }

        [HttpGet]
        [Produces(typeof(Response<Category>))]
        public IActionResult Get()
        {
            return Ok(_categoryBL.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Response<Category>))]
        public IActionResult GetxId(string id)
        {
            return Ok(_categoryBL.Get(id));
        }

        [HttpPost]
        [Produces(typeof(Response<Category>))]
        public IActionResult Post([FromBody]CategoryRequest request)
        {
            return Ok(_categoryBL.Create(request));
        }


        [HttpPut("{id}")]
        [Produces(typeof(Response<Category>))]
        public IActionResult Put(string id, [FromBody]CategoryRequest request)
        {
            return Ok(_categoryBL.Update(id, request));
        }


        [HttpDelete("{id}")]
        [Produces(typeof(Response<Category>))]
        public IActionResult Delete(string id)
        {
            return Ok(_categoryBL.Delete(id));
        }
    }
}