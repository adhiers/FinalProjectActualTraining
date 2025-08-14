using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class TestDriveDAL : ITestDrive
    {
        private readonly FinalProjectDBContext _context;
        public TestDriveDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public TestDrive Create(TestDrive item)
        {
            try
            {
                _context.TestDrives.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while creating the test drive.", ex);
            }
        }

        public void Delete(string id)
        {
            try
            {
                var testDrive = GetById(id);
                if (testDrive == null)
                {
                    throw new KeyNotFoundException($"Test drive with ID {id} not found.");
                }
                _context.TestDrives.Remove(testDrive);
                _context.SaveChanges();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the test drive.", ex);
            }
        }

        public IEnumerable<TestDrive> GetAll()
        {
            var testDrives = _context.TestDrives
                .Include(td => td.Consult)
                .Include(td => td.DealerCar)
                .Include(td => td.Schedule)
                .Include(td => td.Sp)
                .ToList();
            if (testDrives == null || !testDrives.Any())
            {
                throw new KeyNotFoundException("No test drives found.");
            }
            return testDrives;
        }

        public TestDrive GetById(string id)
        {
           var testDrive = _context.TestDrives
                .Include(td => td.Consult)
                .Include(td => td.DealerCar)
                .Include(td => td.Schedule)
                .Include(td => td.Sp)
                .FirstOrDefault(td => td.Tdid == id);
            if (testDrive == null)
            {
                throw new KeyNotFoundException($"Test drive with ID {id} not found.");
            }
            return testDrive;
        }

        public TestDrive Update(TestDrive item)
        {
            try
            {
                var existingTestDrive = GetById(item.Tdid);
                if (existingTestDrive == null)
                {
                    throw new KeyNotFoundException($"Test drive with ID {item.Tdid} not found.");
                }
                _context.Entry(existingTestDrive).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return existingTestDrive;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the test drive.", ex);
            }
        }
    }
}
