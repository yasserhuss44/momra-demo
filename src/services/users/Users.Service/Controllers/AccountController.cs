using Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Users.BL;
using Users.Model.Dtos;

namespace Users.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase//, IAccountLogic
    {
        private readonly IAccountLogic _accountLogic;
        public AccountController(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<ResponseDetailsList<UserDto>> Get()
        {
            try
            {
                return _accountLogic.GetAllUsers();
            }
            catch (Exception ex)
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
        public ResponseDetailsBase Post(UserDto user)
        {
            try
            {
                var response = _accountLogic.AddNewUser(user);
                return response;
            }
            catch (Exception ex)
            {
                return new ResponseDetailsBase(ex);
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
    }
}
