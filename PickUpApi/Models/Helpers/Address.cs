using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace PickUpApi.Models.Helpers
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual Location Location { get; set; }
    }
}
