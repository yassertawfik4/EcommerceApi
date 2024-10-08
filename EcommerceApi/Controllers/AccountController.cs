﻿using data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IConfiguration configuration) 
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =new ApplicationUser()
                {
                   UserName=userDto.Username,
                   Email=userDto.Email,
                   FirstName=userDto.FirstName,
                   LastName=userDto.LastName,
                   Age=userDto.Age,
                  
                };
                await userManager.CreateAsync(user, userDto.Password);
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDtO loginDto)
        {
            if (ModelState.IsValid)
            {
                // محاولة تسجيل الدخول
                var user = await userManager.FindByNameAsync(loginDto.UserName);
                if (user != null)
                {
                    var cheack = await userManager.CheckPasswordAsync(user, loginDto.Password);
                    if (cheack)
                    {
                        await signInManager.SignInAsync(user, loginDto.RememberMe);
                        var claims= new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        var Roles =await userManager.GetRolesAsync(user);
                        foreach (var role in Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var sc= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: configuration["JWT:Issuer"],
                            audience: configuration["JWT:Audience"],
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: sc
                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("AllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = userManager.Users.ToList(); 
            var model = users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                Age = user.Age,
                LastName = user.LastName,
                CardCount = context.CardItems.Count(t => t.UserId == user.Id),
                FavoriteCount = context.favoriteItems.Count(t => t.UserId == user.Id),
            }).ToList();

            return Ok(model);
        }
    }
}