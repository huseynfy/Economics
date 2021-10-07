using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ekonomiks.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        public string Info { get; set; }
        public string Importance { get; set; }
        public string Goal { get; set; }
        public string Image { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        [NotMapped, Required]
        public IFormFile Photo { get; set; }
    }
}
