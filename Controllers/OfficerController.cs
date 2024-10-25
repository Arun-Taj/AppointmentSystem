using AppointmentSystem.Models;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
namespace AppointmentSystem.Controllers
{
    public class OfficerController : Controller
    {
        private readonly OfficerService _officerService;
         private readonly PostService _postService;

         public OfficerController(OfficerService officerService, PostService postService)
        {
            _officerService = officerService;
            _postService = postService;
        }

        // GET: Officer
        public IActionResult Index()
        {
            var officers = _officerService.GetAll();
            return View(officers);
        }

        // GET: Officer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Officer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Officer officer)
        {
            if (ModelState.IsValid)
            {
                _officerService.Add(officer);
                return RedirectToAction(nameof(Index));
            }
            return View(officer);
        }

        // GET: Officer/Edit/{id}
        public IActionResult Edit(int id)
        {
            var officer = _officerService.GetById(id);
            if (officer == null)
            {
                return NotFound();
            }
            return View(officer);
        }

        // POST: Officer/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Officer officer)
        {
            if (ModelState.IsValid)
            {
                _officerService.Update(officer);
                return RedirectToAction(nameof(Index));
            }
            return View(officer);
        }

        // POST: Officer/Activate/{id}
        [HttpPost]
        public IActionResult Activate(int id)
        {
            _officerService.Activate(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Officer/Deactivate/{id}
        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            _officerService.Deactivate(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Officer/Appointments/{id}
        public IActionResult ViewAppointments(int id)
        {
            var appointments = _officerService.GetAppointmentsByOfficerId(id);
            return View(appointments);
        }
    }
}
