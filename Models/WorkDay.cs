namespace AppointmentSystem.Models
{
    public class WorkDay
    {
        public int Id { get; set; }
        public int OfficerId { get; set; } // Foreign Key
        public DayOfWeek DayOfWeek { get; set; }
    }
}
