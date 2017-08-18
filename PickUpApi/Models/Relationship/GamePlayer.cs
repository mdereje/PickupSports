using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models.Relationship
{
    //https://docs.microsoft.com/en-us/ef/core/modeling/relationships#many-to-many
    public class GamePlayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }
        public Game Game { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
