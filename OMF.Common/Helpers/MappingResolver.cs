using AutoMapper;
using System;

namespace OMF.Common.Helpers
{
    public class MappingResolver : IValueResolver<DateTime, DateTime, DateTime>
    {
        public DateTime Resolve(DateTime source, DateTime destination, DateTime destMember, ResolutionContext context)
        {
            if (source == DateTime.MinValue) return destination;

            return source;
        }
    }
}