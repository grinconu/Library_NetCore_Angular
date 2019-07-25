namespace Library.Backend.API.Controllers
{
    using Library.Backend.Contracts.Business;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;
    using Library.Backend.Entities.Response;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        readonly ISecurityBL _securityBL;

        public SecurityController(ISecurityBL securityBL)
        {
            _securityBL = securityBL;
        }

        [HttpPost]
        [Produces(typeof(Response<SecurityResponse>))]
        public IActionResult Post([FromBody]SecurityRequest request)
        {
            return Ok(_securityBL.ValidateUser(request));
        }

        [HttpPost]
        [Route(nameof(ResfreshToken))]
        [Produces(typeof(Response<SecurityResponse>))]
        public IActionResult ResfreshToken(string token)
        {
            return Ok(_securityBL.RefreshToken(token));
        }
    }
}