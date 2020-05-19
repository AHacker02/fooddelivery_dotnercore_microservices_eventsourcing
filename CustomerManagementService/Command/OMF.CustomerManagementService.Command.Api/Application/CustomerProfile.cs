using AutoMapper;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Service.Command;

namespace OMF.CustomerManagementService.Command.Api.Application
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // CreateMap<CreateUserCommand, User>()
            //     .ForMember(m => m.CreatedAt, opt => opt.MapFrom(o => o.TimeStamp))
            //     .ForMember(m => m.Password, opt => opt.Ignore())
            //     .ForMember(m => m.PasswordSalt, opt => opt.Ignore())
            //     .ReverseMap();
            //
            // CreateMap<DeleteUserCommand, User>()
            //     .ForMember(m => m.CreatedAt, opt => opt.MapFrom(o => o.TimeStamp))
            //     .ForMember(m => m.Password, opt => opt.Ignore())
            //     .ForMember(m => m.PasswordSalt, opt => opt.Ignore())
            //     .ReverseMap();

        }
    }
}
