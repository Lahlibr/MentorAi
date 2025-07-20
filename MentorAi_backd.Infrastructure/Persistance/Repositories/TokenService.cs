using AutoMapper.Configuration;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MentorAi_backd.Repositories.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IGeneric<ReviewerProfile> _reviewerRepo;
        public  TokenService (IConfiguration config,IGeneric<ReviewerProfile> reviewerRepo)
        {
            _config = config;
            _reviewerRepo = reviewerRepo;

        }
        public async Task<string> GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.UserData, user.UserName),
        new Claim("Email", user.Email),
        new Claim(ClaimTypes.Role, user.UserRole.ToString()),
    };

            // Add ReviewerId only if user is a Reviewer
            if (user.UserRole == UserEnum.Reviewer)
            {
                var reviewerProfile = await _reviewerRepo.Query()
                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

                if (reviewerProfile != null)
                {
                    claims.Add(new Claim("ReviewerId", reviewerProfile.Id.ToString()));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
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
