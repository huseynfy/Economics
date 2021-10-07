using System.ComponentModel.DataAnnotations;

namespace Ekonomiks.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string AboutUs { get; set; }
        public string WhatWasDone { get; set; }
    }
}
