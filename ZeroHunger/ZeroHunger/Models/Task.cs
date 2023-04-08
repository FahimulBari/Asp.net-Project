using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Restaurant { get; set; }

        [Required]
        public DateTime ReserveTime { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Employee { get; set; }


    }
}