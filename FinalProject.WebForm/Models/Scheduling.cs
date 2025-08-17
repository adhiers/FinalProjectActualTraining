using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.WebForm.Models
{
    public class Scheduling
    {
        public int ScheduleId { get; set; }

        public int GuestId { get; set; }

        public string DealerId { get; set; }

        public string Program { get; set; }

        public DateTime AvailableStart { get; set; }

        public DateTime AvailableEnd { get; set; }
    }
}