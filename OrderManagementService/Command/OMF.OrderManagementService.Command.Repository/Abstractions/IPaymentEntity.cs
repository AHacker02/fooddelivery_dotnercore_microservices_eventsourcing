using System;
using System.Collections.Generic;
using System.Text;

namespace OMF.OrderManagementService.Command.Repository.Abstractions
{
    public interface IPaymentEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
