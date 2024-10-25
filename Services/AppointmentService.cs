using AppointmentSystem.Data;
using AppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Services
{
    public class AppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            var officer = await _context.Officers.FindAsync(appointment.OfficerId);
            if (officer == null || !officer.IsActive)
                throw new InvalidOperationException("Cannot create an appointment for an inactive officer.");

            var visitor = await _context.Visitors.FindAsync(appointment.VisitorId);
            if (visitor == null || !visitor.IsActive)
                throw new InvalidOperationException("Cannot create an appointment for an inactive visitor.");

            bool conflictExists = await _context.Appointments.AnyAsync(a =>
                a.OfficerId == appointment.OfficerId &&
                a.IsActive &&
                a.StartTime < appointment.EndTime &&
                appointment.StartTime < a.EndTime &&
                a.Date == appointment.Date);

            if (conflictExists)
                throw new InvalidOperationException("An appointment already exists for the specified time.");

            appointment.IsActive = true;
            appointment.AddedOn = DateTime.Now;
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var activity = new Activity
            {
                Type = "Appointment",
                OfficerId = appointment.OfficerId,
                StartDate = appointment.Date,
                StartTime = appointment.StartTime,
                EndDate = appointment.Date,
                EndTime = appointment.EndTime,
                Status = "Active"
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(appointment.Id);
            if (existingAppointment == null)
                throw new InvalidOperationException("Appointment not found.");

            existingAppointment.LastUpdatedOn = DateTime.Now;
            existingAppointment.Name = appointment.Name;
            existingAppointment.Date = appointment.Date;
            existingAppointment.StartTime = appointment.StartTime;
            existingAppointment.EndTime = appointment.EndTime;
            existingAppointment.IsActive = appointment.IsActive;

            _context.Appointments.Update(existingAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByOfficerIdAsync(int officerId)
        {
            return await _context.Appointments
                .Where(a => a.OfficerId == officerId && a.IsActive)
                .ToListAsync();
        }

        public async Task CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return;

            appointment.IsActive = false;
            await _context.SaveChangesAsync();

            var relatedActivity = await _context.Activities
                .FirstOrDefaultAsync(a =>
                    a.Type == "Appointment" &&
                    a.OfficerId == appointment.OfficerId &&
                    a.StartDate == appointment.Date &&
                    a.StartTime == appointment.StartTime);

            if (relatedActivity != null)
            {
                relatedActivity.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }
        }
    }
}
