using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace YouToob.Models
{
    public class User
    {
        private readonly IConfiguration _configuration;

        public User(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Avatar { get; set; }
        public string? CoverImage { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public ICollection<Video> WatchHistory { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsPasswordCorrect(string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, this.Password);
        }
        public string GenerateAccessToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration["AccessTokenSecret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new("Id", this.Id.ToString()),
                    new("Email", this.Email),
                    new("Username", this.Username),
                    new("FullName", this.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("AccessTokenExpiry")),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration["RefreshTokenSecret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new("Id", this.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("RefreshTokenExpiry")),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
