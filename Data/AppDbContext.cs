using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Models;

namespace AppointmentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Activities> Activities { get; set; }
    }
}