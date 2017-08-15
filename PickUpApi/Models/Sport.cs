using System;

namespace PickUpApi.Models
{
    public class Sport
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SportId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? SeasonStartDate { get; set; }
        public DateTime? SeasonEndDate { get; set; }
    }
}
