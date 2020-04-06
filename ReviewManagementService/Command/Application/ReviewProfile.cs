using AutoMapper;
using OMF.Common.Models;
using OMF.ReviewManagementService.Command.Application.Command;

namespace OMF.ReviewManagementService.Command.Application
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewCommand, Review>()
                .ForMember(x => x.Id,
                    opt => opt.Ignore()).ReverseMap();
        }
    }
}
