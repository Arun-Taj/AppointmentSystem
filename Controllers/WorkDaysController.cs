using AppointmentSystem.Models;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    public class WorkDaysController : Controller
    {
        private readonly WorkDaysService _workDaysService;

        public WorkDaysController(WorkDaysService workDaysService)
        {
            _workDaysService = workDaysService;
        }

        public IActionResult Create()
        {
            return View(new WorkDay());  // Updated from WorkDays to WorkDay
        }

        [HttpPost]
        public IActionResult Create(WorkDay workDay)  // Updated from WorkDays to WorkDay
        {
            if (ModelState.IsValid)
            {
                _workDaysService.AddWorkDay(workDay);
                return RedirectToAction("Index", "Officer");
            }
            return View(workDay);
        }
    }
}
