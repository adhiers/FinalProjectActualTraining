using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.WebForm.Models
{
    public class TestDrive
    {
        public string Tdid { get; set; }

        public string ConsultId { get; set; }

        public int ScheduleId { get; set; }

        public string DealerCarId { get; set; }

        public string Spid { get; set; }

        public string Note { get; set; }

        public DateTime Tddate { get; set; }
    }
}