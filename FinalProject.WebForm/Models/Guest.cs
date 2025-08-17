using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.WebForm.Models
{
    public class Guest
    {
        public int GuestId { get; set; }

        public string GuestName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}