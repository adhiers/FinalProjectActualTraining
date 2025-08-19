using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.WebForm.Models
{
    public class ConsultationInsert
    {
        public string ConsultId { get; set; }
        public int? ScheduleId { get; set; }

        public string Spid { get; set; }

        public int CustomerBudget { get; set; }

        public DateTime ConsultDate { get; set; }

        public string Note { get; set; }
    }
}