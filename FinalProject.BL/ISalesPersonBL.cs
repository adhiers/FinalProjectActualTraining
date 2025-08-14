using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface ISalesPersonBL
    {
        IEnumerable<SalesPersonDTO> GetAllSalesPeople();
        SalesPersonDTO GetById(string id);
        SalesPersonDTO CreateSalesPerson(SalesPersonInsertDTO salesPersonInsertDTO);
        SalesPersonDTO UpdateSalesPerson(SalesPersonUpdateDTO salesPersonUpdateDTO);
        void DeleteSalesPerson(string id);

    }
}
