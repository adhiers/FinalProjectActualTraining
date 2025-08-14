using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.BL.DTO;
using FinalProject.BO;

namespace FinalProject.BL.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Car, CarDTO>();
            CreateMap<CarInsertDTO, Car>();
            CreateMap<CarUpdateDTO, Car>();

            CreateMap<Dealer, DealerDTO>();
            CreateMap<DealerInsertDTO, Dealer>();
            CreateMap<DealerUpdateDTO, Dealer>();

            CreateMap<DealerCar, DealerCarDTO>();
            CreateMap<DealerCarInsertDTO, DealerCar>();
            CreateMap<DealerCarUpdateDTO, DealerCar>();

            CreateMap<SalesPerson, SalesPersonDTO>();
            CreateMap<SalesPersonInsertDTO, SalesPerson>();
            CreateMap<SalesPersonUpdateDTO, SalesPerson>();

            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestInsertDTO, Guest>();
            CreateMap<GuestUpdateDTO, Guest>();
        }
    }
}
