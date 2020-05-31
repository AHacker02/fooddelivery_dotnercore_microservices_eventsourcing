using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using OMF.Common.Enums;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class CancelOrderCommand : IRequest<Response>
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        [EnumDataType(typeof(Domain))]
        public string Domain { get; set; }
    }

}
