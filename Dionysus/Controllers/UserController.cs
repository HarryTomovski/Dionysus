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
        private readonly IUserAccess userAccess;
        private IUserBusinessLogic userBusinessLogic;

        public UserController(IUserAccess userAccess)
        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            this.userAccess = userAccess;
            this.userBusinessLogic = userBusinessLogic;
        }
        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult> addUser(User user, [FromHeader] string? validationCode)
        {
            try
            {
                var result = await userBusinessLogic.addUser(user, validationCode);
                if (result is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<string>> Register(UserRegisterModel model)
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("getUser")]
        public async Task<ActionResult> getUser([FromHeader] string username, [FromHeader] string password)
        {
            try
            {
                var result = await userBusinessLogic.getUser(username, password);
                if (result is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
        {
            return await userAccess.RegisterAsync(model);
                    return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<string>> Login(UserLoginModel model)
            }
            catch (Exception e)
        {
            return await userAccess.LoginAsync(model);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        }

    }
}
