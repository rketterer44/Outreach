using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutreachCli
{
    [Route("api/v1/token")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        internal static string Code { get; set; }

        [HttpGet, Route("callback")]
        public ActionResult Callback(string code)
        {
            return Ok(Code = code);
        }
    }
}
