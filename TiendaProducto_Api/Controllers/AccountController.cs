using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Api;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiendaProducto_Api.Helpers;

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
        private readonly APISettings _aPISettings;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _aPISettings = options.Value;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequestDto loginData)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginData.UserName, loginData.Password, false, false);

            if (signInResult.Succeeded)
            {
                //get user details
                var userDetails = await _userManager.FindByNameAsync(loginData.UserName);
                if (userDetails == null)
                {
                    return Unauthorized(new AuthResponseDto
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "invalid authentication"
                    });
                }

                var signingCredentials = GetSigningCredentials();
                var getClaims = await GetClaimsAsync(userDetails);

                //create token options
                var tokenOptions = new JwtSecurityToken
                    (
                        issuer: _aPISettings.ValidIssuer,
                        audience: _aPISettings.ValidAudience,
                        claims: getClaims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signingCredentials
                    );

                //create token 
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


                //return user info
                return Ok(new AuthResponseDto
                {
                    IsAuthenticated = true,
                    ErrorMessage = string.Empty,
                    Token = token,
                    UserInfo = new UserDto
                    {
                        Id = userDetails.Id,
                        Name = userDetails.Name,
                        Email = userDetails.Email,
                        PhoneNo = userDetails.PhoneNumber
                    }
                });

            }
            else
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsAuthenticated = false,
                    ErrorMessage = "invalid authentication"
                });

            }
        }

        //Getting Signing Credentials
        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));
            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        //create claims
        private async Task<List<Claim>> GetClaimsAsync(AppUser user)
        {
            //common claim
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            //get user roles
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

            foreach (var role in roles)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }

            return claimsList;


        }
    }
}
