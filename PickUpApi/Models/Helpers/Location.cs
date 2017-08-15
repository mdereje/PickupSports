using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models.Helpers
{
    public class Location
    {
        //TODO: Need to find the appropriate data format for this.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
