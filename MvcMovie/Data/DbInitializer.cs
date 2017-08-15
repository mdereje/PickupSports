using System;
using System.Linq;
using PickUpApi.Models;

namespace PickUpApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SportContext context)
        {
            context.Database.EnsureCreated();

            // Look for any sports.
            if (context.Sports.Any())
            {
                return;   // DB has been seeded
            }

            var sportContexts = new Sport[]
            {
                new Sport{Name="Soccer",Type="Outdoor",SeasonStartDate= DateTime.Now.Add(new TimeSpan(-20,0,0)), SeasonEndDate =  DateTime.Now.Add(new TimeSpan(20,0,0))},
                new Sport{Name="Soccer",Type="Indoor",SeasonStartDate= DateTime.Now.Add(new TimeSpan(-40,0,0)), SeasonEndDate =  DateTime.Now.Add(new TimeSpan(70,0,0))},
                new Sport{Name="Volleyball",Type="Grass",SeasonStartDate= DateTime.Now.Add(new TimeSpan(-50,0,0)), SeasonEndDate =  DateTime.Now.Add(new TimeSpan(1,0,0))},
                new Sport{Name="Basketball",Type="Indoor",SeasonStartDate= DateTime.Now.Add(new TimeSpan(-10,0,0)), SeasonEndDate =  DateTime.Now.Add(new TimeSpan(50,0,0))}
            };
            foreach (Sport s in sportContexts)
            {
                context.Sports.Add(s);
            }
            context.SaveChanges();
        }
    }
}