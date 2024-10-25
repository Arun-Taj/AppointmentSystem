using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSystem.Models
{
    public class WorkDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OfficerId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
    }
}
