using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "sss", "value2" };
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(new string("value"));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] User us)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(User);
            }
            return CreatedAtAction("Get", us);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
