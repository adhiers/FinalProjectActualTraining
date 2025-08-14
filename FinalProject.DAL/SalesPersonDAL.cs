using FinalProject.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class SalesPersonDAL : ISalesPerson
    {
        private readonly FinalProjectDBContext _context;
        public SalesPersonDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public SalesPerson Create(SalesPerson item)
        {
            _context.SalesPeople.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.SalesPeople.Remove(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("SalesPerson not found");
            }
        }

        public IEnumerable<SalesPerson> GetAll()
        {
            var results = _context.SalesPeople.OrderBy(sp => sp.SPId).ToList();
            return results;
        }

        public SalesPerson GetById(string id)
        {
            var result = _context.SalesPeople.Find(id);
            if (result == null)
            {
                throw new Exception("Dealer not found");
            }
            return result;
        }

        public SalesPerson Update(SalesPerson item)
        {
            _context.SalesPeople.Update(item);
            _context.SaveChanges();
            return item;
        }
    }
}
