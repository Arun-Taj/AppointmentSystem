namespace AppointmentSystem.Models
{
    public class Officer
    {
        public int Id { get; set; }
        public int PostId { get; set; } // Foreign Key
        public string Name { get; set; }= string.Empty;  // Initialize with an empty string to avoid warning
        public bool IsActive { get; set; } // Active, Inactive
        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
