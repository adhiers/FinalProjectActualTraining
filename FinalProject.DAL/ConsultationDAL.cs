using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class ConsultationDAL : IConsultation
    {
        private readonly FinalProjectDBContext _context;
        public ConsultationDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public Consultation Create(Consultation item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Consultation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Consultation GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Consultation Update(Consultation item)
        {
            throw new NotImplementedException();
        }
    }
}
