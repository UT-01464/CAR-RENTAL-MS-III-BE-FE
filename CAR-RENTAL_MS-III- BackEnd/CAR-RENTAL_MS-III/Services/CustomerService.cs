using AutoMapper;
using Azure.Identity;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.I_Services;
using CAR_RENTAL_MS_III.Models.Customer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace CAR_RENTAL_MS_III.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            return customer;
        }

        public async Task RegisterCustomerAsync(CustomerResponseDTO customerResponseDTO)
        {
            if (customerResponseDTO.Password != customerResponseDTO.ConformPassword)
                throw new ValidationException("Passwords do not match.");

            var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(customerResponseDTO.Email);
            if (existingCustomer != null)
                throw new ValidationException("Email is already registered.");

            var passwordHasher = new PasswordHasher<Customer>();
            var customer = new Customer
            {
                NicNumber = customerResponseDTO.NicNumber,
                FirstName = customerResponseDTO.FirstName,
                LastName = customerResponseDTO.LastName,
                Username = customerResponseDTO.Username,
                Email = customerResponseDTO.Email,
                PhoneNumber = customerResponseDTO.PhoneNumber,
                Address = customerResponseDTO.Address,
                RegistrationDate = DateTime.UtcNow,
                //PasswordHash = passwordHasher.HashPassword(null, customerResponseDTO.Password)
                PasswordHash=BCrypt.Net.BCrypt.HashPassword(customerResponseDTO.Password),
               
            };

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(int id, CustomerResponseDTO customerResponseDTO)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            customer.FirstName = customerResponseDTO.FirstName;
            customer.LastName = customerResponseDTO.LastName;
            customer.Username = customerResponseDTO.Username;
            customer.PhoneNumber = customerResponseDTO.PhoneNumber;
            customer.Address = customerResponseDTO.Address;

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }





    }
}
