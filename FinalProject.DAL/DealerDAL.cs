using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BO;

namespace FinalProject.DAL
{
    public class DealerDAL : IDealer
    {
        private readonly FinalProjectDBContext _context;
        public DealerDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public Dealer Create(Dealer item)
        {
            _context.Dealers.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Dealers.Remove(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Dealer not found");
            }
        }

        public IEnumerable<Dealer> GetAll()
        {
            var results = _context.Dealers.OrderBy(d => d.DealerId).ToList();
            return results;
        }

        public Dealer GetById(string id)
        {
            var result = _context.Dealers.Find(id);
            if (result == null)
            {
                throw new Exception("Dealer not found");
            }
            return result;
        }

        public Dealer Update(Dealer item)
        {
            _context.Dealers.Update(item);
            _context.SaveChanges();
            return item;
        }
    }
}
