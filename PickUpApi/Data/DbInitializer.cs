using System;
using System.Linq;
using GenFu;
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

            var i = 1;
            A.Configure<Address>().Fill(a => a.AddressId, () => new int());
            A.Configure<Location>().Fill(a => a.LocationId, () => new int())
                                    .Fill(a => a.Latitude, () => RandomFloat(new Random(i++)))
                                    .Fill(a => a.Longitude, () => RandomFloat(new Random(i++)));
            var addresses = A.ListOf<Address>();
            var locations = A.ListOf<Location>();

            A.Configure<Game>().Fill(g => g.Address).WithRandom(addresses)
                               .Fill(g => g.Location).WithRandom(locations)
                               .Fill(g => g.GameId, () => new long())
                               .Fill(g => g.SportId).WithinRange(1, sportContexts.Length);

            var gameContexts = A.ListOf<Game>(100);

            foreach (Game g in gameContexts)
            {
                context.Games.Add(g);
            }

            context.SaveChanges();
        }

        //https://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp
        //generates a float between ~128 - 128
        private static float RandomFloat(Random random)
        {
            var mantissa = (random.NextDouble() * 2.0) - 1.0;
            var exponent = Math.Pow(2.0, random.Next(0, 7));
            return (float)(mantissa * exponent);
        }
    }
}