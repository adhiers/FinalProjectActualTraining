using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class GuestDAL : IGuest
    {
        private readonly FinalProjectDBContext _context;
        public GuestDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public Guest Create(Guest item)
        {
            _context.Guests.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Guest with ID {id} not found.");
            }
            _context.Guests.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Guest> GetAll()
        {
            var result = _context.Guests
                .Include(g => g.Schedulings)
                .ToList();
            if (result == null || !result.Any())
            {
                throw new KeyNotFoundException("No guests found.");
            }
            return result;
        }

        public Guest GetById(int id)
        {
            var result = _context.Guests
                .Include(g => g.Schedulings)
                .FirstOrDefault(g => g.GuestId == id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Guest with ID {id} not found.");
            }
            return result;
        }

        public Guest Update(Guest item)
        {
            _context.Guests.Update(item);
            _context.SaveChanges();
            return item;
        }
    }
}
