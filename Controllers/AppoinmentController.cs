using AppointmentSystem.Models;
using AppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly OfficerService _officerService;
        private readonly VisitorService _visitorService;

        public AppointmentController(AppointmentService appointmentService, OfficerService officerService, VisitorService visitorService)
        {
            _appointmentService = appointmentService;
            _officerService = officerService;
            _visitorService = visitorService;
        }

        public IActionResult Index()
        {
            var appointments = _appointmentService.GetAllAppointments();
            return View(appointments);
        }

        public IActionResult Create()
        {
            ViewBag.Officers = _officerService.GetAllOfficers();
            ViewBag.Visitors = _visitorService.GetAllVisitors();
            return View(new Appointment());
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Perform business logic checks here
                _appointmentService.AddAppointment(appointment);
                return RedirectToAction("Index");
            }

            ViewBag.Officers = _officerService.GetAllOfficers();
            ViewBag.Visitors = _visitorService.GetAllVisitors();
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null) return NotFound();

            ViewBag.Officers = _officerService.GetAllOfficers();
            ViewBag.Visitors = _visitorService.GetAllVisitors();
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.UpdateAppointment(appointment);
                return RedirectToAction("Index");
            }

            ViewBag.Officers = _officerService.GetAllOfficers();
            ViewBag.Visitors = _visitorService.GetAllVisitors();
            return View(appointment);
        }

        public IActionResult Cancel(int id)
        {
            _appointmentService.CancelAppointment(id);
            return RedirectToAction("Index");
        }
    }
}
