using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class CarInsertDTO
    {
        public string CarId { get; set; }

        public string ModelType { get; set; }

        public string FuelType { get; set; }

        public int BasePrice { get; set; }

        public int Stock { get; set; }
    }
}
