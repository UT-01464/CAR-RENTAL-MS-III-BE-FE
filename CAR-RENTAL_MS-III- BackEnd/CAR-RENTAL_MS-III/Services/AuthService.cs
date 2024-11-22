using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CAR_RENTAL_MS_III.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IAuthRepository authRepository,IConfiguration configuration, ApplicationDbContext context)
        { 
            _authRepository = authRepository;
            _configuration = configuration;
            _context = context;
        }



        public async Task<string> RegisterUser(CustomerRegisterDto customerDto)
        {
            if (await _authRepository.UserExists(customerDto.Username))
                return "Username already exists.";

            var user = new User
            {
                Username = customerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(customerDto.PasswordHash),
                Role = "Customer"
            };

            await _authRepository.AddUser(user);

            var customer = new Customer
            {
                NicNumber = customerDto.NicNumber,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Address = customerDto.Address,
                Role = "Customer",
                RegistrationDate = DateTime.Now,
                Username = customerDto.Username

            };

            var passwordHash = new PasswordHasher<Customer>().HashPassword(customer, customerDto.PasswordHash);
            customer.PasswordHash = passwordHash;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<string> LoginUser(UserLoginDto userDto)
        {
            var existingUser = await _authRepository.GetUserByUsername(userDto.Username);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, existingUser.Password))
                return null;  // Invalid credentials

            return GenerateJwtToken(existingUser);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
