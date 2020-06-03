using AutoMapper;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query.Application
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<TblRestaurant, Restaurant>()
                .ForMember(m => m.Location, opt => opt.MapFrom(d => d.TblLocation))
                .ForMember(m => m.Offers, opt => opt.MapFrom(d => d.TblOffer))
                .ForMember(m => m.RestaurantDetails, opt => opt.MapFrom(d => d.TblRestaurantDetails))
                .ReverseMap();
            CreateMap<TblLocation, Location>().ReverseMap();
            CreateMap<TblRestaurantDetails, RestaurantDetails>().ReverseMap();
            CreateMap<TblOffer, Offer>()
                .ForMember(x => x.ItemId, opt => opt.MapFrom(d => d.TblMenu.Id))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(d => d.TblMenu.Quantity))
                .ForMember(x => x.Item, opt => opt.MapFrom(d => d.TblMenu.Item))
                .ForMember(x => x.Cuisine, opt => opt.MapFrom(d => d.TblMenu.TblCuisine.Cuisine));
        }
    }
}
