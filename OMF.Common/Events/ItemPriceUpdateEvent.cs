using System;
using System.Collections.Generic;
using System.Text;
using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class ItemPriceUpdateEvent:Event
    {
        public ItemPriceUpdateEvent(int itemId, decimal price)
        {
            ItemId = itemId;
            Price = price;
        }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
    }
}
