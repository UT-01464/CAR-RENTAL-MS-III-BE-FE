using AutoMapper;
using CAR_RENTAL_MS_III.Entities;
using CAR_RENTAL_MS_III.Models.Car;
using CAR_RENTAL_MS_III.Models.Customer;
using CAR_RENTAL_MS_III.Models.Manager;

namespace CAR_RENTAL_MS_III.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Mapping between Car entity and DTOs
            CreateMap<Car, CarResponseDTO>();
            CreateMap<CarRequestDTO, Car>();

            // Mapping between Customer entity and DTOs
            CreateMap<Customer, CustomerResponseDTO>();
            CreateMap<CustomerRequestDTO, Customer>();

            // Mapping between Manager entity and DTOs
            CreateMap<Manager, ManagerResponseDTO>();
            CreateMap<ManagerRequestDTO, Manager>();
        }

    }
}
