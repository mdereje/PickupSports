using System;
using System.Linq;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Extensions;
using GenFu;
using Microsoft.AspNetCore.Mvc.Internal;
using PickUpApi.Models;
using PickUpApi.Models.Helpers;
using GenFu = GenFu.GenFu;

namespace PickUpApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PickupContext context)
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

            A.Configure<Address>().Fill(a => a.AddressId, () => new int());
            A.Configure<Location>().Fill(a => a.LocationId, () => new int());
            var addresses = A.ListOf<Address>();
            var locations = A.ListOf<Location>();

            A.Configure<Game>().Fill(g => g.Address).WithRandom(addresses)
                               .Fill(g => g.Location).WithRandom(locations)
                               .Fill(g => g.GameId, () => new long())
                               .Fill(g => g.SportId).WithinRange(0, sportContexts.Length);

            var gameContexts = A.ListOf<Game>(100);

            foreach (Game g in gameContexts)
            {
                context.Games.Add(g);
            }

            context.SaveChanges();
        }
    }
}