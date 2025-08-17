using FinalProject.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL
{
    public interface ISchedulingBL
    {
        IEnumerable<SchedulingDTO> GetSchedulings();
        SchedulingDTO GetById(int id);
        SchedulingDTO AddScheduling(SchedulingInsertDTO schedulingInsertDTO);
        SchedulingDTO UpdateScheduling(SchedulingUpdateDTO schedulingUpdateDTO);
        void DeleteScheduling(int id);
        IEnumerable<SchedulingDTO> GetBySearch(string search);
    }
}
