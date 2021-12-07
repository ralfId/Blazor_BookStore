using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto registerData)
        {
            //Check for valid data
            if (registerData == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            //new user
            var newUser = new AppUser()
            {
                Name = registerData.Name,
                UserName = registerData.Email,
                Email = registerData.Email,
                PhoneNumber = registerData.PhoneNo,
                EmailConfirmed = true
            };

            //create new user
            var resultCreateUser = await _userManager.CreateAsync(newUser, registerData.Password);

            //if there are errors when creating the new user
            if (!resultCreateUser.Succeeded)
            {
                var errors = resultCreateUser.Errors.Select(x => x.Description);
                return BadRequest(new RegisterResponseDto
                {
                    IsRegistered = false,
                    Errors = errors
                });
            }

            //adding a role to the new user
            var enroleUser = await _userManager.AddToRoleAsync(newUser, ConstantsCommon.Role_Client);

            //if there are errors when adding a role to the new user
            if (!enroleUser.Succeeded)
            {
                var errors = resultCreateUser.Errors.Select(x => x.Description);
                return BadRequest(new RegisterResponseDto
                {
                    IsRegistered = false,
                    Errors = errors
                });
            }

            //successful response
            return StatusCode(201);
        }
    }
}
