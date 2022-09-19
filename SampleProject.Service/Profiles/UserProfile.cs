using AutoMapper;
using SampleProject.Common.Infrastructure.Models.Entities;
using SampleProject.Common.Infrastructure.Models.RequestModels.User;

namespace SampleProject.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequestModel, UserModel>()
                .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedDateUtc, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateUserRequestModel, UserModel>()
                .ForMember(dest => dest.ModifiedDateUtc, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
