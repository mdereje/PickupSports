using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlayerId { get; set; }
        //[ForeignKey("Game")]
        public long? GameId { get; set; }
        public Name Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
       // public virtual Game Game { get; set; }
    }
}
