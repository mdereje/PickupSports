using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PickUpApi.Models.Relationship;

namespace PickUpApi.Models
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlayerId { get; set; }
        public Name Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }
    }
}
