using AutoMapper;
using OMF.Common.Models;
using OMF.OrderManagementService.Query.Repository.DataContext;

namespace OMF.OrderManagementService.Query.Application
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<TblFoodOrder, Order>()
                .ForMember(m => m.RestaurantId, opt => opt.MapFrom(d => d.TblRestaurantId))
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(d => d.TblCustomerId))
                .ForMember(m => m.OrderItems, opt => opt.MapFrom(d => d.TblFoodOrderItem));

            CreateMap<TblFoodOrderItem, FoodOrderItem>()
                .ForMember(m => m.MenuId, opt => opt.MapFrom(d => d.TblMenuId))
                .ForMember(m => m.Quantity, opt => opt.MapFrom(d => d.Quantity==0?"Item Out ofStock":d.Quantity.ToString()));
            
            CreateMap<TblTableBooking, Booking>()
                .ForMember(m => m.RestaurantId, opt => opt.MapFrom(d => d.TblRestaurantId))
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(d => d.TblCustomerId))
                .ForMember(m => m.TableDetail, opt => opt.MapFrom(d => d.TblTableDetail));

            CreateMap<TableDetail, TblTableDetail>().ReverseMap();

            CreateMap<TblOrderPayment, Payment>()
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(d => d.TblCustomerId));
            

        }
        
    }
}