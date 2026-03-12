using KAshop.DAL.DTO.Request;
using KAshop.DAL.DTO.Response;
using KAshop.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IEmailSender _EmailSender;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> UserManager,
           IEmailSender emailSender, 
           IConfiguration configuration)
        {
            _UserManager = UserManager;
            _EmailSender = emailSender;
            _configuration = configuration;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _UserManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResponse() { Success = false, Message = errors };
            }
            await _UserManager.AddToRoleAsync(user, "User");

            var token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var EmailUrl = $"https://localhost:7136/api/Account/ConfirmEmail?token={token}&userid={user.Id}";
            // للتاكد انه الي دخل على الصفحة وصلته رسالة عالايميل, مش حدت عشوائي استخدم الرابط

            await _EmailSender.SendEmailAsync(user.Email, "welcom", $"<h1> welcom {request.UserName} </h1>" + "  "
                + $"<a href='{EmailUrl}'> confirm </a> ");

            return new RegisterResponse() { Success = true, Message = "Success" };

        }

       public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);

            if (user == null) 
            {
                return new LoginResponse() { Success = false, Message = "Invalid Email" };
            }

            if (!await _UserManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse() { Success = false, Message = "Email is not confirmed" };
            }


            var result = await _UserManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new LoginResponse() { Success = false, Message = "Invalid Passwoed" };
            }

            return new LoginResponse() { Success = true, Message = "succcess", AccessToken = await GenerateAccessToken(user) };

        }


        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var UserClaims = new List<Claim>()
            { // المعلومات الي باخذها لما افك تشفير التوكن
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName), 
                new Claim(ClaimTypes.Email, user.Email),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
       issuer: _configuration["Jwt:Issuer"],
       audience: _configuration["Jwt:Audience"],
       claims: UserClaims,
       expires: DateTime.Now.AddDays(5),
       signingCredentials: credentials
       );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<bool> ConfirmEmailAsync(string token, string UserId)
        {
            var user = await _UserManager.FindByIdAsync(UserId);
            if (user == null) { return false; }

            var result = await _UserManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded) { return false; }

            return true;

        }
    }
}
