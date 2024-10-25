using AppointmentSystem.Data;
using AppointmentSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSystem.Services
{
    public class OfficerService
    {
        private readonly ApplicationDbContext _context;

        public OfficerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Officer> GetAll() => _context.Officers.ToList();

        public Officer? GetById(int id) => _context.Officers.FirstOrDefault(o => o.Id == id);

        public void Add(Officer officer)
        {
            officer.IsActive = true; // Ensure new officer is active by default
            _context.Officers.Add(officer);
            _context.SaveChanges();
        }

        public void Update(Officer officer)
        {
            var existingOfficer = GetById(officer.Id);
            if (existingOfficer != null)
            {
                existingOfficer.Name = officer.Name;
                existingOfficer.PostId = officer.PostId;
                existingOfficer.WorkStartTime = officer.WorkStartTime;
                existingOfficer.WorkEndTime = officer.WorkEndTime;
                _context.SaveChanges();
            }
        }

        public void Activate(int id) => ChangeStatus(id, true);

        public void Deactivate(int id) => ChangeStatus(id, false);

        private void ChangeStatus(int id, bool status)
        {
            var officer = GetById(id);
            if (officer != null)
            {
                officer.IsActive = status;
                _context.SaveChanges();
            }
        }

        public List<Appointment> GetAppointmentsByOfficerId(int officerId)
        {
            return _context.Appointments.Where(a => a.OfficerId == officerId).ToList();
        }
    }
}
