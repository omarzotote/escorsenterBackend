using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace versusBackend.Controllers.api
{
    public class LoginApiController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult checkLogin([FromBody]UserDTO user)
        {
            if(user.UserName == "admin" && user.Password == "versus123")
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
