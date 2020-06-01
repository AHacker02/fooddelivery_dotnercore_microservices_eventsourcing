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
                .ForMember(m => m.MenuId, opt => opt.MapFrom(d => d.TblMenuId));
            
            CreateMap<TblTableBooking, Booking>()
                .ForMember(m => m.RestaurantId, opt => opt.MapFrom(d => d.TblRestaurantId))
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(d => d.TblCustomerId))
                .ForMember(m => m.TableDetail, opt => opt.MapFrom(d => d.TblTableDetail));

            CreateMap<TableDetail, TblTableDetail>();

            CreateMap<TblOrderPayment, Payment>()
                .ForMember(m => m.CustomerId, opt => opt.MapFrom(d => d.TblCustomerId));
            

        }
        
    }
}