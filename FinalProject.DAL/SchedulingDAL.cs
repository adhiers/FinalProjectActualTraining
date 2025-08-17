using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class SchedulingDAL : IScheduling
    {
        private readonly FinalProjectDBContext _context;
        public SchedulingDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public Scheduling Create(Scheduling item)
        {
            _context.Schedulings.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {

            _context.Schedulings.Remove(new Scheduling { ScheduleId = id });
            _context.SaveChanges();
        }

        public IEnumerable<Scheduling> GetAll()
        {
            var result = _context.Schedulings
                .Include(s => s.Guest)
                .Include(s => s.Dealer)
                .Include(s => s.Consultation)
                .ToList();
            if (result == null || !result.Any())
            {
                throw new KeyNotFoundException("No schedulings found.");
            }
            return result;
        }

        public Scheduling GetById(int id)
        {
            var result = _context.Schedulings
                .Include(s => s.Guest)
                .Include(s => s.Dealer)
                .Include(s => s.Consultation)
                .FirstOrDefault(s => s.ScheduleId == id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Scheduling with ID {id} not found.");
            }
            return result;
        }

        public Scheduling Update(Scheduling item)
        {
            try
            {
                var existingScheduling = _context.Schedulings.Find(item.ScheduleId);
                if (existingScheduling == null)
                {
                    throw new KeyNotFoundException($"Scheduling with ID {item.ScheduleId} not found.");
                }
                existingScheduling.GuestId = item.GuestId;
                existingScheduling.DealerId = item.DealerId;
                existingScheduling.Program = item.Program;
                existingScheduling.AvailableStart = item.AvailableStart;
                existingScheduling.AvailableEnd = item.AvailableEnd;
                _context.SaveChanges();
                return existingScheduling;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Concurrency error occurred while updating the scheduling.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the scheduling: {ex.Message}");
            }
        }

        public IEnumerable<Scheduling> GetBySearch(string search)
        {
            var result = _context.Schedulings
                .Include(s => s.Guest)
                .Include(s => s.Dealer)
                .Include(s => s.Consultation)
                .Where(s => s.Program.Contains(search) ||
                            s.Guest.GuestId.ToString().Contains(search) ||
                            s.Dealer.DealerId.Contains(search))
                .ToList();
            if (result == null || !result.Any())
            {
                throw new KeyNotFoundException($"No schedulings found for search term '{search}'.");
            }
            return result;
        }
    }
}
