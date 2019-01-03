using AutoMapper;
using MTest.Data.Entities;
using MTest.Models;
using MTest.Services.Contracts;

namespace MTest.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<AddCustomerRequest, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
            CreateMap<GetCustomersRequest, GetCustomersFilter>();
        }
    }
}
