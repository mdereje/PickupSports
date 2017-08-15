using System.ComponentModel.DataAnnotations.Schema;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long GameId { get; set; }
        //Should be One-to-Many with Sport table.
        public long SportId { get; set; }
        public Address Address { get; set; }
        public Location Location { get; set; }
    }
}
