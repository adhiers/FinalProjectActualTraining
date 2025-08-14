using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class SalesPersonUpdateDTO
    {
        public string SPId { get; set; }

        public string SalesName { get; set; }

        public string DealerId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}
