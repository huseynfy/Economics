using System;
using System.ComponentModel.DataAnnotations;

namespace Ekonomiks.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Post { get; set; }
        public DateTime Date { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
