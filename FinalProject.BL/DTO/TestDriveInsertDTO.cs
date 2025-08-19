using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class TestDriveInsertDTO
    {
        public string Tdid { get; set; }

        public string ConsultId { get; set; }

        public int? ScheduleId { get; set; }

        public string DealerCarId { get; set; }

        public string Spid { get; set; }

        public string Note { get; set; }

        public DateTime Tddate { get; set; }

        //public virtual Consultation Consult { get; set; }

        //public virtual Customer Customer { get; set; }

        //public virtual DealerCar DealerCar { get; set; }

        //public virtual Scheduling Schedule { get; set; }

        //public virtual SalesPerson Sp { get; set; }
    }
}
