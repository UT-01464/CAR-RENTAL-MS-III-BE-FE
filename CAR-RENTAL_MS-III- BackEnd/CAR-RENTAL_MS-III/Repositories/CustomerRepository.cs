using CAR_RENTAL_MS_III.Data;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.I_Repositories;
using CAR_RENTAL_MS_III.Models;
using Microsoft.AspNetCore.Mvc;

namespace CAR_RENTAL_MS_III.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<Customer> CreateUserAccount(Customer customer)
        {
            var newuser = await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return newuser.Entity;
           
        }
       
    }
}
