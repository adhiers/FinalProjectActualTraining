using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
            try
            {
                _context.Consultations.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding the consultation.", ex);
            }
        }

        public void Delete(string id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException("Consultation not found.");
                }
                _context.Consultations.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the consultation.", ex);
            }
        }

        public IEnumerable<Consultation> GetAll()
        {
            var results = _context.Consultations
                .Include(c => c.Schedule)
                .Include(c => c.Sp).AsNoTracking()
                .ToList();
            return results;
        }

        public Consultation GetById(string id)
        {
            var result = _context.Consultations
                .Include(c => c.Schedule)
                .Include(c => c.Sp)
                .FirstOrDefault(c => c.ConsultId == id);
            if (result == null) 
            {
                throw new KeyNotFoundException("Consultation not found.");
            }
            return result;
        }

        public Consultation Update(Consultation item)
        {
            try
            {
                var existingEntity = GetById(item.ConsultId);
                if (existingEntity == null)
                {
                    throw new KeyNotFoundException("Consultation not found.");
                }
                else
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return existingEntity;
                }
            }
        }
    }
}
