using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class DealerCarDTO
    {
        public string DealerCarId { get; set; }
        public string CarId { get; set; }
        public string DealerId { get; set; }
        public int DealerCarPrice { get; set; }
        public virtual CarDTO Car { get; set; }
        public virtual DealerDTO Dealer { get; set; }
    }
}
