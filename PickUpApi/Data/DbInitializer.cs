using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GenFu;
using PickUpApi.Models;
using PickUpApi.Models.Helpers;

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
            A.Configure<Location>().Fill(a => a.LocationId, () => new int())
                                   .Fill(a => a.Latitude, () => RandomFloat(new Random(i++)))
                                   .Fill(a => a.Longitude, () => RandomFloat(new Random(i++)));
            var locations = A.ListOf<Location>();

            A.Configure<Address>().Fill(a => a.AddressId, () => new int())
                                  .Fill(a => a.Location).WithRandom(locations);
            var addresses = A.ListOf<Address>();

            A.Configure<Name>().Fill(n => n.NameId, () => new long());
            var names = A.ListOf<Name>();

            A.Configure<Player>().Fill(p => p.PlayerId, () => new long())
                                 .Fill(p => p.Name).WithRandom(names);

            var playerContexts = A.ListOf<Player>(34);

            foreach (var p in playerContexts)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();
            
            ICollection<Player> playerCollection = new Collection<Player>();
            playerCollection.Add(context.Players as Player);
            //playerCollection.Add(context.Players.ToList().GetRange(10, 18));
            //playerCollection.Add(context.Players.ToList().GetRange(28, 5));

            A.Configure<Game>().Fill(g => g.Address).WithRandom(addresses)
                .Fill(g => g.GameId, () => new long())
                .Fill(g => g.Referee).WithRandom(new[] {true, false})
                .Fill(g => g.FreeToPlayer).WithRandom(new[] {true, false})
                .Fill(g => g.SportId).WithinRange(1, sportContexts.Length)
                .Fill(g => g.SkillLevel).WithRandom(new [] {SkillLevel.Advanced, SkillLevel.Elite, SkillLevel.Medium, SkillLevel.OpenToAll, SkillLevel.Recreation})
                                .Fill(g => g.Players).WithRandom(playerCollection);

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