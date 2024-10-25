using Microsoft.AspNetCore.Mvc;
using AppointmentSystem.Services;
using AppointmentSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Controllers
{
    public class VisitorController : Controller
    {
        private readonly VisitorService _visitorService;

        public VisitorController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        // GET: Visitor
        public async Task<IActionResult> Index()
        {
            var visitors = await _visitorService.GetAllAsync();
            return View(visitors);
        }

        // GET: Visitor/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visitor visitor)
        {
            if (!ModelState.IsValid)
            {
                // Logging validation errors (can be used for debugging purposes)
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    // You can log the errors or use breakpoints here to inspect them
                    System.Diagnostics.Debug.WriteLine(error);
                }
                return View(visitor); // Return the form with validation errors displayed
            }

            await _visitorService.AddAsync(visitor);
            return RedirectToAction(nameof(Index));
        }

        // GET: Visitor/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var visitor = await _visitorService.GetByIdAsync(id);
            if (visitor == null) return NotFound();
            return View(visitor);
        }

        // POST: Visitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Visitor visitor)
        {
            if (!ModelState.IsValid)
            {
                return View(visitor);
            }

            await _visitorService.UpdateAsync(visitor);
            return RedirectToAction(nameof(Index));
        }

        // POST: Visitor/Activate/5
        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            await _visitorService.ActivateAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Visitor/Deactivate/5
        [HttpPost]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _visitorService.DeactivateAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
