using AppointmentSystem.Data;
using AppointmentSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSystem.Services
{
    public class WorkDaysService
    {
        private readonly ApplicationDbContext _context;

        public WorkDaysService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddWorkDay(WorkDay workDay)  // Updated from WorkDays to WorkDay
        {
            _context.WorkDays.Add(workDay);
            _context.SaveChanges();
        }

        public void UpdateWorkDay(WorkDay workDay)  // Updated from WorkDays to WorkDay
        {
            _context.WorkDays.Update(workDay);
            _context.SaveChanges();
        }

        public WorkDay GetWorkDayById(int id)  // Updated from WorkDays to WorkDay
        {
            return _context.WorkDays.Find(id);
        }

        public IEnumerable<WorkDay> GetWorkDaysByOfficerId(int officerId)
        {
            return _context.WorkDays.Where(w => w.OfficerId == officerId).ToList();
        }
    }
}
