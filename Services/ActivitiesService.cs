using AppointmentSystem.Data;
using AppointmentSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSystem.Services
{
    public class ActivitiesService
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddActivity(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public void CancelActivity(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity != null)
            {
                activity.Status = "Cancelled";
                _context.SaveChanges();
            }
        }

        public IEnumerable<Activity> GetActivitiesByOfficerId(int officerId)
        {
            return _context.Activities.Where(a => a.OfficerId == officerId).ToList();
        }

        public Activity GetActivityById(int id)
        {
            return _context.Activities.Find(id);
        }

        public void UpdateActivity(Activity activity)
        {
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }
    }
}
