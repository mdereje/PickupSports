﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models
{
    public class Sport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SportId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime SeasonStartDate { get; set; }
        public DateTime? SeasonEndDate { get; set; }
    }
}
