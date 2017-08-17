using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public long GameId { get; set; }
        //Sports 1 -- Game Many
        public int SportId { get; set; }
        public virtual Address Address { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MaximumNumberOfPlayers { get; set; }
        public bool Referee { get; set; }
        //Current Number of players counting depends on this flag. Only payed count.
        public bool FreeToPlayer { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}
