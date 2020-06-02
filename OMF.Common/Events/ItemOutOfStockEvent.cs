﻿using ServiceBus.Abstractions;

namespace OMF.Common.Events
{
    public class ItemOutOfStockEvent : Event
    {
        public ItemOutOfStockEvent(int itemId)
        {
            ItemId = itemId;
        }

        public int ItemId { get; set; }
    }
}