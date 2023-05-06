using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierInputModel, Supplier>();
            CreateMap<PartInputModel, Part>();
            CreateMap<CarInputModel,Car>();
        }
    }
}
