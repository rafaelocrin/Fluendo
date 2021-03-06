﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fluendo.FluendoPlatform.Infrastructure.Authentication.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private const string JwtKey = "Jwt:Key";
        private const string JwtIssuer = "Jwt:Issuer";
        private const string JwtExpiration = "Jwt:Expiration";

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken()
        {
            IActionResult response = Unauthorized();
            
            var tokenString = BuildToken();
            response = Ok(new { token = tokenString });

            return response;
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[JwtKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config[JwtIssuer], _config[JwtIssuer], 
                        expires: DateTime.Now.AddSeconds(Convert.ToInt32(_config[JwtExpiration])),
                        signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}