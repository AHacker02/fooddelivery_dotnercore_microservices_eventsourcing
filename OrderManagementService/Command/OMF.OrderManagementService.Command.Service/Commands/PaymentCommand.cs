using System.ComponentModel.DataAnnotations;
using MediatR;
using OMF.Common.Enums;
using OMF.Common.Models;

namespace OMF.OrderManagementService.Command.Service.Commands
{
    public class PaymentCommand : IRequest<Response>
    {
        public string Remarks { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal TransactionAmount { get; set; }

        [EnumDataType(typeof(Domain))] public string Domain { get; set; }

        [EnumDataType(typeof(PaymentType))] public string PaymentType { get; set; }
    }
}