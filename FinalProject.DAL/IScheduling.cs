using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BO;

namespace FinalProject.DAL
{
    public interface IScheduling
    {
        Scheduling Create(Scheduling item);
        Scheduling GetById(int id);
        Scheduling Update(Scheduling item);
        void Delete(int id);
        IEnumerable<Scheduling> GetAll();
    }
}
