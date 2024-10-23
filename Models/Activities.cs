namespace AppointmentSystem.Models
{
    public class Activities
    {
        public int Id { get; set; }
        public string Type { get; set; } // Leave, Break, Appointment
        public int OfficerId { get; set; } // Foreign Key
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; } // Active, Cancelled
    }
}
