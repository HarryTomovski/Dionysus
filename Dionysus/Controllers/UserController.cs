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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Dionysus.Models.RequestModels;
using Dionysus.Models.ResponceModels;
using Dionysus.DTO_s;

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
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
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
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpPost(nameof(UpdateUserRole))]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<string>> UpdateUserRole(UpdateUserRoleModel model)
        {
            try
            {
                var succeess = await userBusinessLogic.ChangeUserRole(model.Username, model.Role);
                if (succeess == true)
                {
                    return StatusCode(StatusCodes.Status200OK, succeess);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpPost(nameof(GetAllUsers))]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await userBusinessLogic.getAllUsers();
                if (users != null)
                {
                    return StatusCode(StatusCodes.Status200OK, users);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }

        }

    }
}
