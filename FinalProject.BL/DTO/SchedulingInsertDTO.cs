using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class SchedulingInsertDTO
    {
        public int GuestId { get; set; }

        public string DealerId { get; set; }

        public string Program { get; set; }

        public DateTime AvailableStart { get; set; }

        public DateTime AvailableEnd { get; set; }

        public virtual DealerDTO Dealer { get; set; }

        public virtual GuestDTO Guest { get; set; }
    }
}
