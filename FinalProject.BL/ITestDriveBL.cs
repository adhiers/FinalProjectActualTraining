using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface ITestDriveBL
    {
        IEnumerable<TestDriveDTO> GetTestDrives();
        TestDriveDTO GetById(string id);
        TestDriveDTO AddTestDrive(TestDriveInsertDTO testDriveInsertDTO);
        TestDriveDTO UpdateTestDrive(TestDriveDTO testDriveDTO);
        void DeleteTestDrive(string id);
    }
}
