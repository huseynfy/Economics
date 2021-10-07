using System.ComponentModel.DataAnnotations;

namespace Ekonomiks.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string Youtube { get; set; }
        public string Email { get; set; }
    }
}
