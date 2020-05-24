using AutoMapper;
using OMF.Common.Models;
using OMF.ReviewManagementService.Query.Repository.DataContext;

namespace OMF.ReviewManagementService.Query.Application
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<TblRating, Rating>()
                .ForMember(x => x.Rest_Rating, opts => opts.MapFrom(d => d.Rating))
                .ForMember(x => x.CustomerId, opts => opts.MapFrom(d => d.TblCustomerId))
                .ReverseMap();
        }
        
    }
}