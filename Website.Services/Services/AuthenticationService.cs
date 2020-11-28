using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Website.Data.Entities;
using Website.Data.Repositories;

namespace Website.Services.Services
{
    /// <summary>
    /// Service for authenticating users.
    /// </summary>
    public sealed class AuthenticationService
    {
        /// <summary>
        /// The salt length.
        /// </summary>
        private const int SALT_LENGTH = 24;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        private UserRepository UserRepository { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="userRepository">The user repository.</param>
        public AuthenticationService(IConfiguration configuration, UserRepository userRepository)
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }

        /// <summary>
        /// Tries the authenticate user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns>True if the user was authenticated, false otherwise.</returns>
        public bool TryAuthenticateUser(User user, out string token)
        {
            User? existingUser = UserRepository.GetByUsername(user.Username).Result;

            token = string.Empty;

            if (existingUser != null)
            {
                string storedHash = existingUser.Password[0..^SALT_LENGTH];
                string storedSalt = existingUser.Password[^SALT_LENGTH..];

                byte[] password = Encoding.UTF8.GetBytes($"{user.Password}{storedSalt}");
                byte[] hash = new SHA256CryptoServiceProvider().ComputeHash(password);

                string passwordToTest = Convert.ToBase64String(hash);

                if (passwordToTest.Equals(storedHash))
                {
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"]));
                    JwtSecurityToken securityToken = new JwtSecurityToken(Configuration["JwtToken:Issuer"],
                                                                          Configuration["JwtToken:Issuer"],
                                                                          signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                    token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                }
            }

            return !string.IsNullOrEmpty(token);
        }
    }
}
