using System;
using System.Collections.Generic;

namespace PickUpApi.Data
{
    public static class SeedData
    {
        public static readonly List<string> SportName = new List<string>
        {
            "Soccer",
            "Volleyball",
            "Hockey",
            "Basketball",
            "Ultimate Frisbee",
            "Running"
        };

        public static readonly List<string> SportType = new List<string>
        {
            "Outdoor Grass",
            "Outdoor Turf",
            "Outdoor Concret",
            "Indoor Gym",
            "Indoor Turf"
        };

        public static readonly List<DateTime?> EndDate = new List<DateTime?>()
        {
            DateTime.Now,
            DateTime.Now.Add(new TimeSpan(10, 0, 0, 0)),
            null
        };

        public static List<long?> ListOfLong(int num)
        {
            var _long = new List<long?>();

            for (var i = 1; i <= num; i++)
            {
                _long.Add(i);
                if (i%5 == 0)
                {
                    _long.Add(null);
                }
            }

            return _long;
        }
    }
}
