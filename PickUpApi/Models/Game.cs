using System.ComponentModel.DataAnnotations.Schema;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }
        //Should be One-to-Many with Sport table.
        public int SportId { get; set; }
        public virtual Address Address { get; set; }
    }
}
