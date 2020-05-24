using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OMF.Common.Models;
using OMF.CustomerManagementService.Query.Repository.DataContext;

namespace OMF.CustomerManagementService.Query.Api.Application
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<TblCustomer, User>().ReverseMap();
        }
    }
}
