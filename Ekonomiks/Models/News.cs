using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ekonomiks.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Body { get; set; }
        public string Image { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int ReadTime { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
    }
}
