using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Models;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query.Application
{
    public class RestaurantProfile:Profile
    {
        public RestaurantProfile()
        {
            CreateMap<TblRestaurant, Restaurant>()
                .ForMember(m => m.Location, opt => opt.MapFrom(d => d.TblLocation))
                .ForMember(m => m.Offers, opt => opt.MapFrom(d => d.TblOffer))
                .ForMember(m => m.RestaurantDetails, opt => opt.MapFrom(d => d.TblRestaurantDetails))
                .ForMember(m => m.Ratings, opt => opt.MapFrom(d => d.TblRating))
                .ReverseMap();
            CreateMap<TblLocation, Location>().ReverseMap();
            CreateMap<TblRating, Rating>().ForMember(m => m.Rest_Rating, opt => opt.MapFrom(d => d.Rating))
                .ReverseMap();
            CreateMap<TblRestaurantDetails, RestaurantDetails>().ReverseMap();
            CreateMap<TblOffer, Offer>().ReverseMap();
        }
    }
}
