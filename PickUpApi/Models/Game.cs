using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }
        //Should be One-to-Many with Sport table.
        public int SportId { get; set; }
        [IgnoreDataMember]
        public int AddressId { get; set; }
        [IgnoreDataMember]
        public int LocationId { get; set; }
        public virtual Address Address { get; set; }
        public virtual Location Location { get; set; }
    }
}
