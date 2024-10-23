namespace AppointmentSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int OfficerId { get; set; } // Foreign Key
        public int VisitorId { get; set; } // Foreign Key
        public string Name { get; set; }
        public string Status { get; set; } // Active, Cancelled, Completed
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
