using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpApi.Models
{
    public class Name
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NameId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
