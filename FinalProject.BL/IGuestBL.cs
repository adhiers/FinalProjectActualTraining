using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface IGuestBL
    {
        IEnumerable<GuestDTO> GetGuests();
        GuestDTO GetById(int guestId);
        GuestDTO AddGuest(GuestInsertDTO guestInsertDTO);
        GuestDTO UpdateGuest(GuestUpdateDTO guestUpdateDTO);
        void DeleteGuest(int guestId);
        IEnumerable<GuestDTO> SearchGuests(string search);
    }
}
