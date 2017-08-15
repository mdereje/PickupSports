using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models.Helpers
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
