using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AZepher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
       
       

        // GET: api/Table/5
        [HttpGet("[action]")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Table
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Table/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
