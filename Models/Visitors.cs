using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailAddress { get; set; }

          public bool IsActive { get; set; } // Active, Inactive
        
        public List<Appointment> Appointments { get; set; }
    }
}
