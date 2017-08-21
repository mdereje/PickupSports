using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GenFu;
using Microsoft.EntityFrameworkCore;
using PickUpApi.Models;
using PickUpApi.Models.Helpers;
using PickUpApi.Models.Relationship;

namespace PickUpApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PickupContext context)
        {
            //TODO: Remove
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
            // Look for any sports.
            if (context.Sports.Any())
            {
                return;   // DB has been seeded
            }

            //Create Sports
            A.Configure<Sport>()
                .Fill(x => x.SportId, () => new int())
                .Fill(x => x.Name).WithRandom(SeedData.SportName)
                .Fill(x => x.Type).WithRandom(SeedData.SportType)
                .Fill(x => x.SeasonStartDate).AsPastDate()
                .Fill(x => x.SeasonEndDate).WithRandom(SeedData.EndDate);

            var sportContexts = A.ListOf<Sport>(10);

            foreach (Sport s in sportContexts)
            {
                context.Sports.Add(s);
            }
            context.SaveChanges();

            // Create location, address, name, player
            var i = 1;
            A.Configure<Location>().Fill(a => a.LocationId, () => new int())
                                   .Fill(a => a.Latitude, () => RandomFloat(new Random(i++)))
                                   .Fill(a => a.Longitude, () => RandomFloat(new Random(i++)));
            var locations = A.ListOf<Location>();

            A.Configure<Address>().Fill(a => a.AddressId, () => new int())
                                  .Fill(a => a.UnitNo).AsAddressLine2()
                                  .Fill(a => a.Location).WithRandom(locations);
            var addresses = A.ListOf<Address>();

            A.Configure<Name>().Fill(n => n.NameId, () => new long())
                               .Fill(n => n.MiddleName).AsFirstName();
            var names = A.ListOf<Name>();

            //Player
            A.Configure<Player>().Fill(p => p.PlayerId, () => new long())
                .Fill(p => p.GameId, () => new long())
                .Fill(p => p.Name).WithRandom(names);

            var playerContexts = A.ListOf<Player>(34);

            foreach (var p in playerContexts)
            {
                System.Diagnostics.Debug.WriteLine("Player.GameId = {0}", p.GameId);
            }

            foreach (var p in playerContexts)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            ICollection<Player> playerCollection = new Collection<Player>();
            //playerCollection.Add(playerContexts.GetRange(0, 10));
            //playerCollection.Add(playerContexts.GetRange(10, 18));
            //playerCollection.Add(playerContexts.GetRange(28, 5));

            //Create Game
            var mockSkillLevel = new List<SkillLevel>
            {
                SkillLevel.Advanced,
                SkillLevel.Elite,
                SkillLevel.Medium,
                SkillLevel.OpenToAll,
                SkillLevel.Recreation
            };
            A.Configure<Game>().Fill(g => g.Address).WithRandom(addresses)
                .Fill(g => g.GameId, () => new long())
                .Fill(g => g.PlayerId, () => new long())
                .Fill(g => g.Referee).WithRandom(new[] {true, false})
                .Fill(g => g.FreeToPlayer).WithRandom(new[] {true, false})
                .Fill(g => g.SportId).WithinRange(1, sportContexts.Count)
                .Fill(g => g.SkillLevel).WithRandom(mockSkillLevel);
                                //.Fill(g => g.Players).WithRandom(playerCollection);

            var gameContexts = A.ListOf<Game>(100);

            foreach (Game g in gameContexts)
            {
                context.Games.Add(g);
            }

            foreach (var g in gameContexts)
            {
                System.Diagnostics.Debug.WriteLine("Game.GameId = {0}", g.GameId);
            }
            context.SaveChanges();

            //Add GamePlayerRelationship
            A.Configure<GamePlayer>().Fill(gp => gp.GameId).WithRandom(context.Games.Select(g => g.GameId))
                                     .Fill(gp => gp.PlayerId).WithRandom(context.Players.Select(p => p.PlayerId));

            var gamePlayerContext = A.ListOf<GamePlayer>(50);

            foreach (var gp in gamePlayerContext)
            {
                context.GamePlayers.Add(gp);
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

        private static List<long> GetGameIds(PickupContext context)
        {
            return context.Games.Select(g => g.GameId).ToList();
        }
    }
}