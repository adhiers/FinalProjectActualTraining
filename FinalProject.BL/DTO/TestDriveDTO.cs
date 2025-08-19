using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class TestDriveDTO
    {
        public string Tdid { get; set; }

        public string? ConsultId { get; set; }

        public int ScheduleId { get; set; }

        public string DealerCarId { get; set; }

        public string Spid { get; set; }

        public string Note { get; set; }

        public DateTime Tddate { get; set; }

        public virtual ConsultationDTO Consult { get; set; }

        public virtual DealerCarDTO DealerCar { get; set; }

        public virtual SchedulingDTO Schedule { get; set; }

        public virtual SalesPersonDTO Sp { get; set; }
    }
}
