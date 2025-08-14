using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class ConsultationDTO
    {
        public string ConsultId { get; set; }

        public int? ScheduleId { get; set; }

        public string Spid { get; set; }

        public int CustomerBudget { get; set; }

        public DateTime ConsultDate { get; set; }

        public string Note { get; set; }

        public virtual SchedulingDTO Schedule { get; set; }

        public virtual SalesPersonDTO Sp { get; set; }
    }
}
