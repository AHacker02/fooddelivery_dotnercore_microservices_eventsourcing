using AutoMapper;
using OMF.ReviewManagementService.Command.Repository.DataContext;
using OMF.ReviewManagementService.Command.Service.Command;

namespace OMF.ReviewManagementService.Command.Application
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewCommand,TblRating>()
                .ForMember(x=>x.Id,opts=>opts.Ignore())
                .ForMember(x=>x.TblCustomerId,opts=>opts.MapFrom(d=>d.CustomerId))
                .ReverseMap();
        }
    }
}