using System;
using System.Collections.Generic;
using System.Text;
using Geolocation;
using OMF.RestaurantService.Query.Repository.DataContext;

namespace OMF.RestaurantService.Query.Repository
{
    public static class Extensions
    {
        public static double Distance( this TblLocation location,double coordinateX, double coordinateY)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = Deg2Rad((double)location.X - coordinateX);  
            var dLon = Deg2Rad((double)location.Y - coordinateY);
            var a =
                    Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(Deg2Rad(coordinateX)) * Math.Cos(Deg2Rad((double)location.X)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
