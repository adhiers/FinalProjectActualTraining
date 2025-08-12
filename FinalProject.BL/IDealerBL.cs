using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface IDealerBL
    {
        IEnumerable<DealerDTO> GetDealers();
        DealerDTO GetById(string dealerId);
        DealerDTO AddDealer(DealerInsertDTO dealerInsertDTO);
        DealerDTO UpdateDealer(DealerUpdateDTO dealerUpdateDTO);
        void DeleteDealer(string dealerId);
    }
}
