using AutoMapper;
using OMF.CustomerManagementService.Command.Repository.DataContext;
using OMF.CustomerManagementService.Command.Service.Command;

namespace OMF.CustomerManagementService.Command.Api.Application
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateUserCommand, TblCustomer>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DeleteUserCommand, TblCustomer>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap<UpdateUserCommand, TblCustomer>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}