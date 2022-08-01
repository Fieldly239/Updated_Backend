using KM_Management_Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KM_Management_Api.Services
{
    public interface IAccountService
    {
        Task<string> GetTokenLogin(GetTokenRequest getTokenRequest);
        Task<bool> ValidateCredentials(string empId, string password);

    }
    public class AccountService : IAccountService
    {
        private readonly IConfiguration configuration;
        public AccountService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> GetTokenLogin(GetTokenRequest getTokenRequest)
        {
            var response = await ValidateCredentials(getTokenRequest.EmpId, getTokenRequest.Password);
            if (response)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, getTokenRequest.EmpId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearer:JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtBearer:JwtExpireDays"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JwtBearer:JwtIssuer"],
                    audience: configuration["JwtBearer:JwtAudience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                var genToken = new JwtSecurityTokenHandler().WriteToken(token);
                return  genToken;
            }
            else
            {
                throw new Exception("Wrong Username or Password");

            }
        }


        public async Task<bool> ValidateCredentials(string empId, string password)
        {
            var ldapDomain = this.configuration.GetSection("LdapDomain").Value;
            using (var adContext = new PrincipalContext(ContextType.Domain, ldapDomain))
            {
                return adContext.ValidateCredentials(empId, password);
            }
        }
    }
}
