using AutoMapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.RequestModels.Customer;

namespace SampleProject.Service.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerRequestModel, CustomerModel>()
             .ForMember(dest => dest.CustomerGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
             .ForMember(dest => dest.CreatedDateUtc, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateCustomerRequestModel, CustomerModel>()
            .ForMember(dest => dest.ModifiedDateUtc, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
