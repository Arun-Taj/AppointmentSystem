using AppointmentSystem.Data;
using AppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Services
{
    public class VisitorService
    {
        private readonly ApplicationDbContext _context;

        public VisitorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Visitor>> GetAllAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task<Visitor> GetByIdAsync(int id)
        {
            return await _context.Visitors.FindAsync(id);
        }

        public async Task AddAsync(Visitor visitor)
        {
            visitor.IsActive = true; // Default status is Active
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Visitor visitor)
        {
            _context.Visitors.Update(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task ActivateAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null && !visitor.IsActive)
            {
                visitor.IsActive = true;
                await _context.SaveChangesAsync();

                // Reactivate future appointments that were deactivated due to visitor being inactive
                var futureDeactivatedAppointments = await _context.Appointments
                    .Where(a => a.VisitorId == id && !a.IsActive && a.Date > DateTime.Now)
                    .Include(a => a.Officer)
                    .ToListAsync();

                foreach (var appointment in futureDeactivatedAppointments)
                {
                    if (appointment.Officer.IsActive) // Only reactivate if officer is also active
                    {
                        appointment.IsActive = true;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null && visitor.IsActive)
            {
                visitor.IsActive = false;
                await _context.SaveChangesAsync();

                // Deactivate future appointments for the visitor
                var futureAppointments = await _context.Appointments
                    .Where(a => a.VisitorId == id && a.IsActive && a.Date > DateTime.Now)
                    .ToListAsync();

                foreach (var appointment in futureAppointments)
                {
                    appointment.IsActive = false;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
