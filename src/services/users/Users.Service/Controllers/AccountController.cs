using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using Users.BL;
using Users.Model.Dtos;

namespace Users.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<ResponseDetailsList<UserDto>> Get()
        {
            try
            {
                var accountLogic = new AccountLogic();
                return accountLogic.GetAllUsers();
            }
            catch(Exception ex)
            {
                return new ResponseDetailsList<UserDto>(ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
