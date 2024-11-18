using AutoMapper;
using Azure.Identity;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Login;
using System.Net.WebSockets;

namespace CAR_RENTAL_MS_III.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<LoginRequest> CreateUserAccount(LoginRequest loginRequest)
        {

            var NewUser = new Customer
            {
                Username = loginRequest.Username,
                PasswordHash=BCrypt.Net.BCrypt.HashPassword(loginRequest.PasswordHash),
                Email = loginRequest.Email,
                Role = loginRequest.Role ?? "customer",
                FirstName = loginRequest.FirstName,
                LastName = loginRequest.LastName,
                NicNumber = loginRequest.NicNumber,
                ConformPassword = loginRequest.ConformPassword,
                PhoneNumber = loginRequest.PhoneNumber,



            };

            var createdUser = await _customerRepository.CreateUserAccount(NewUser);

            var response = new LoginRequest
            {
                Username = createdUser.FirstName,
                Email = createdUser.Email,
                Role = createdUser.Role
            };

            return response;

        }
    }
}
