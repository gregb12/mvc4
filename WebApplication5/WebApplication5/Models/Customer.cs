using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Customer
    {
        [Required]
        public int id { get; set; }


        [Required]
        [DisplayName("First name")]
        public string first_name { get; set; }

        [DisplayName("Last name")]

        [Required]
        public string last_name { get; set; }
    }
}
