using AutoMapper;
using OMF.Common.Models;
using OMF.CustomerManagementService.Command.Application.Command;
using OMF.CustomerManagementService.Command.Application.Event;

namespace OMF.CustomerManagementService.Command.Application
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(o => o.TimeStamp))
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.PasswordSalt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DeleteUserCommand, User>()
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(o => o.TimeStamp))
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.PasswordSalt, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
