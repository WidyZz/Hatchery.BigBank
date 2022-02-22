using AutoMapper;
using Hatchery.BigBank.Data.Entities;
using Hatchery.BigBank.Data.Models;

namespace Hatchery.BigBank.Data
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Partner, PartnerModel>()
                .ReverseMap();

            CreateMap<LoanRequest, CalculatorModel>()
                .ReverseMap();
        }
    }
}
