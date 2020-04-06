using AutoMapper;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Application.Commands;

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
