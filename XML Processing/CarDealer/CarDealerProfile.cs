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
            this.CreateMap<ImportSupplierDto, Supplier>();
            this.CreateMap<ImportPartsDto, Part>().ForMember(
                    d => d.SupplierId, opt =>
                     opt.MapFrom(s => s.SupplierId.Value));

            //Car
            this.CreateMap<ImportCarDto, Car>()
                 .ForSourceMember(s => s.Parts, opt => opt.DoNotValidate());

            this.CreateMap<Car, ExportCarDto>();
            //Customer
            this.CreateMap<ImportCustomerDto, Customer>();
            //Sales
            this.CreateMap<ImportSalesDto, Sale>();
        }
    }
}
