using System;
using AutoMapper;
using OMF.Common.Helpers;
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
                .ForMember(m => m.Active, opt => opt.MapFrom(x=>true))
                .ForMember(m=>m.CreatedDate,opt=>opt.MapFrom((src, dst) =>
                {
                    if (dst.CreatedDate != DateTime.MinValue)
                    {
                        return DateTime.UtcNow;
                    }

                    return dst.CreatedDate;
                }))
                .ReverseMap();

            CreateMap<DeleteUserCommand, TblCustomer>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Active, opt => opt.MapFrom(x => true))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom((src, dst) =>
                {
                    if (dst.CreatedDate != DateTime.MinValue)
                    {
                        return DateTime.UtcNow;
                    }

                    return dst.CreatedDate;
                }))
                .ReverseMap();
            
            CreateMap<UpdateUserCommand, TblCustomer>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Active, opt => opt.MapFrom(x => true))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom((src, dst) =>
                {
                    if (dst.CreatedDate != DateTime.MinValue)
                    {
                        return DateTime.UtcNow;
                    }

                    return dst.CreatedDate;
                }))
                .ReverseMap();
        }
    }
}