using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersInputModel, Supplier>();
            CreateMap<PartsInputModel, Part>();
            CreateMap<Class1, Car>();
            CreateMap<CustomerInputModel, Customer>();
            CreateMap<SalesInputModel, Sale>();
            CreateMap<SuppliersInputModel, Supplier>();
        }
    }
}
