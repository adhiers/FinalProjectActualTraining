using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BO;
using FinalProject.BL.DTO;
using FinalProject.DAL;
using AutoMapper;

namespace FinalProject.BL
{
    public class TestDriveBL : ITestDriveBL
    {
        private readonly ITestDrive _testDriveDAL;
        private readonly IMapper _mapper;
        public TestDriveBL(ITestDrive testDriveDAL, IMapper mapper)
        {
            _testDriveDAL = testDriveDAL;
            _mapper = mapper;
        }
        public TestDriveDTO AddTestDrive(TestDriveInsertDTO testDriveInsertDTO)
        {
            try
            {
                if (testDriveInsertDTO == null)
                {
                    throw new ArgumentNullException(nameof(testDriveInsertDTO), "Test drive insert DTO cannot be null.");
                }
                var testDrive = _mapper.Map<TestDrive>(testDriveInsertDTO);
                var addedTestDrive = _testDriveDAL.Create(testDrive);
                return _mapper.Map<TestDriveDTO>(addedTestDrive);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the test drive.", ex);
            }
        }

        public void DeleteTestDrive(string id)
        {
            try
            {
                _testDriveDAL.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while deleting the test drive.", ex);
            }
        }

        public TestDriveDTO GetById(string id)
        {
            try
            {
                var testDrive = _testDriveDAL.GetById(id);
                if (testDrive == null)
                {
                    throw new KeyNotFoundException($"Test drive with ID {id} not found.");
                }
                return _mapper.Map<TestDriveDTO>(testDrive);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while retrieving the test drive.", ex);
            }
        }

        public IEnumerable<TestDriveDTO> GetTestDrives()
        {
            var testDrives = _testDriveDAL.GetAll();
            if (testDrives == null || !testDrives.Any())
            {
                return Enumerable.Empty<TestDriveDTO>();
            }
            return _mapper.Map<IEnumerable<TestDriveDTO>>(testDrives);
        }

        public TestDriveDTO UpdateTestDrive(TestDriveUpdateDTO testDriveUpdateDTO)
        {
            try
            {
                if (testDriveUpdateDTO == null)
                {
                    throw new ArgumentNullException(nameof(testDriveUpdateDTO), "Test drive DTO cannot be null.");
                }
                var testDrive = _mapper.Map<TestDrive>(testDriveUpdateDTO);
                var updatedTestDrive = _testDriveDAL.Update(testDrive);
                return _mapper.Map<TestDriveDTO>(updatedTestDrive);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the test drive.", ex);
            }
        }
    }
}
