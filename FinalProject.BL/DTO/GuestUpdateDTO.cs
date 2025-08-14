using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL.DTO
{
    public class GuestUpdateDTO
    {
        public int GuestId { get; set; }

        public string GuestName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
