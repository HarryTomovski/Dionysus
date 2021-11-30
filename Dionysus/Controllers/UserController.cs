using Dionysus.DBAccess;
using Dionysus.Models;
using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLogic userBusinessLogic;

        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            this.userBusinessLogic = userBusinessLogic;
        }
        
        [HttpPost(nameof(Register))]
        public async Task<ActionResult<string>> Register(UserRegisterModel model)
        {
            try
            {
                var token = await userBusinessLogic.RegisterUser(model);
                if (String.IsNullOrEmpty(token) == false)
                {
                    return StatusCode(StatusCodes.Status200OK, token);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
            
            
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<string>> Login(UserLoginModel model)
        {
            try
            {
                var token = await userBusinessLogic.LoginUser(model);
                if (String.IsNullOrEmpty(token) == false)
                {
                    return StatusCode(StatusCodes.Status200OK, token);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
