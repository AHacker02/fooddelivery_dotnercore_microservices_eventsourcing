using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace OMF.Common.Helpers
{
    public class MappingResolver : IValueResolver<DateTime, DateTime,DateTime>
    {
        public DateTime Resolve(DateTime source, DateTime destination, DateTime destMember, ResolutionContext context)
        {
            if (source == DateTime.MinValue)
            {
                return destination;
            }

            return source;
        }
    }
}
