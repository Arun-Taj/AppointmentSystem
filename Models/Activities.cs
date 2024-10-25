using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSystem.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string Type { get; set; }  // Leave, Break, Appointment

        [Required]
        public int OfficerId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }

        [Required]
        public string Status { get; set; } = "Active";  // Active, Cancelled

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
    }
}
