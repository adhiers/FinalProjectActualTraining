using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface IDealerCarBL
    {
        IEnumerable<DealerCarDTO> GetAllDealerCars();
        DealerCarDTO GetDealerCarById(string dealerCarId);
        DealerCarDTO AddDealerCar(DealerCarInsertDTO dealerCarInsertDTO);
        DealerCarDTO UpdateDealerCar(DealerCarUpdateDTO dealerCarUpdateDTO);
        void DeleteDealerCar(string dealerCarId);
    }
}
