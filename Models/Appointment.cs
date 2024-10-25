using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSystem.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OfficerId { get; set; }

        [Required]
        public int VisitorId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public string Status => IsActive ? "Active" : "Cancelled";

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.Now;

        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }

        [ForeignKey("VisitorId")]
        public Visitor Visitor { get; set; }
    }
}
