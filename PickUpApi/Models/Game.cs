using System.ComponentModel.DataAnnotations.Schema;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Models
{
    public class Game
    {
        //TODO: need to make this db generated.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }
        //Should be One-to-Many with Sport table.
        public int SportId { get; set; }
        public Address Address { get; set; }
        public Location Location { get; set; }
    }
}
