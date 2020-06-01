using System;
using AutoMapper;
using OMF.Common.Enums;
using OMF.Common.Helpers;
using OMF.Common.Models;
using OMF.OrderManagementService.Command.Repository.DataContext;
using OMF.OrderManagementService.Command.Service.Commands;

namespace OMF.OrderManagementService.Command.Application
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCommand, TblFoodOrder>()
                .ForMember(m => m.TblCustomerId, opt => opt.MapFrom(d => d.CustomerId))
                .ForMember(m => m.TblRestaurantId, opt => opt.MapFrom(d => d.RestaurantId))
                .ForMember(m => m.Status, opt => opt.MapFrom(d => OrderStatus.Cart.ToString()))
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<PaymentCommand, TblOrderPayment>()
                .ForMember(m => m.TblCustomerId, opt => opt.MapFrom(d => d.CustomerId))
                .ForMember(m => m.PaymentStatus, opt => opt.MapFrom(d => PaymentStatus.PaymentSuccessful.ToString()))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => DateTime.UtcNow))
                .ForMember(m => m.ModifiedDate, opt => opt.MapFrom(d => DateTime.UtcNow))
                .ForMember(m => m.Id, opt => opt.Ignore());


            CreateMap<FoodOrderItem, TblFoodOrderItem>()
                .ForMember(m => m.TblMenuId, opt => opt.MapFrom(d => d.MenuId))
                .ReverseMap();

            CreateMap<TableBookingCommand, TblTableBooking>()
                .ForMember(m => m.TblCustomerId, opt => opt.MapFrom(d => d.CustomerId))
                .ForMember(m => m.TblRestaurantId, opt => opt.MapFrom(d => d.RestaurantId))
                .ForMember(m => m.Status, opt => opt.MapFrom(d => OrderStatus.PaymentPending.ToString()))
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => DateTime.UtcNow))
                .ForMember(m => m.ModifiedDate, opt => opt.MapFrom(d => DateTime.UtcNow));


        }
    }
}
