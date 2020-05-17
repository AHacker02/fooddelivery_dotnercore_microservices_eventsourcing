using AutoMapper;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Application
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCommand, Order>()
                .ReverseMap();

        }
    }
}
