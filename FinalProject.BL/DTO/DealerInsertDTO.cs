using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class DealerInsertDTO
    {
        public string DealerId { get; set; }

        public string DealerName { get; set; }

        public string DealerAddress { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public int TaxRate { get; set; }
    }
}
