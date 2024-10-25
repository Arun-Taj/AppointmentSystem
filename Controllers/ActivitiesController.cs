using AppointmentSystem.Models;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ActivitiesService _activitiesService;
        private readonly OfficerService _officerService;

        public ActivitiesController(ActivitiesService activitiesService, OfficerService officerService)
        {
            _activitiesService = activitiesService;
            _officerService = officerService;
        }

        public IActionResult Index()
        {
            var activities = _activitiesService.GetAllActivities();
            return View(activities);
        }

        public IActionResult Create()
        {
            ViewBag.Officers = _officerService.GetAllOfficers();
            return View(new Activity());
        }

        [HttpPost]
        public IActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activitiesService.AddActivity(activity);
                return RedirectToAction("Index");
            }

            ViewBag.Officers = _officerService.GetAllOfficers();
            return View(activity);
        }

        public IActionResult Edit(int id)
        {
            var activity = _activitiesService.GetActivityById(id);
            if (activity == null) return NotFound();

            ViewBag.Officers = _officerService.GetAllOfficers();
            return View(activity);
        }

        [HttpPost]
        public IActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activitiesService.UpdateActivity(activity);
                return RedirectToAction("Index");
            }

            ViewBag.Officers = _officerService.GetAllOfficers();
            return View(activity);
        }

        public IActionResult Cancel(int id)
        {
            _activitiesService.CancelActivity(id);
            return RedirectToAction("Index");
        }
    }
}
