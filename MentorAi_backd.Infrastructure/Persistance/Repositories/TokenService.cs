﻿using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;

namespace MentorAi_backd.Repositories.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public  TokenService (IConfiguration config)
        {
            _config = config;
        }
        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.UserData ,user.UserName),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,user.UserRole.ToString())
            };
            //createing a security key from configuration secret
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            //create signing Credinntials using the key and HMAC-SHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresInMinutes = 60; 
            int.TryParse(_config["JwtSettings:ExpiresInMinutes"], out expiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
