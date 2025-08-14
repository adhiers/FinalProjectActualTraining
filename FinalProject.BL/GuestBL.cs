using FinalProject.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BO;
using FinalProject.DAL;
using AutoMapper;

namespace FinalProject.BL
{
    public class GuestBL : IGuestBL
    {
        private readonly IGuest _guestDAL;
        private readonly IMapper _mapper;
        public GuestBL(IGuest guestDAL, IMapper mapper)
        {
            _guestDAL = guestDAL;
            _mapper = mapper;
        }
        public GuestDTO AddGuest(GuestInsertDTO guestInsertDTO)
        {
            try
            {
                var newGuest = _mapper.Map<Guest>(guestInsertDTO);
                var createdGuest = _guestDAL.Create(newGuest);
                return _mapper.Map<GuestDTO>(createdGuest);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the guest.", ex);
            }
        }

        public void DeleteGuest(int guestId)
        {
            try
            {
                _guestDAL.Delete(guestId);
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occurred while deleting the guest with ID {guestId}.", ex);
            }

        }

        public GuestDTO GetById(int guestId)
        {
            var guest = _guestDAL.GetById(guestId);
            if (guest == null)
            {
                throw new KeyNotFoundException($"Guest with ID {guestId} not found.");
            }
            return _mapper.Map<GuestDTO>(guest);
        }

        public IEnumerable<GuestDTO> GetGuests()
        {
            var guests = _guestDAL.GetAll();
            if (guests == null || !guests.Any())
            {
                return Enumerable.Empty<GuestDTO>();
            }
            return _mapper.Map<IEnumerable<GuestDTO>>(guests);

        }

        public IEnumerable<GuestDTO> SearchGuests(string search)
        {
            var guests = _guestDAL.GetAll()
                .Where(g => g.GuestName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            g.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            g.PhoneNumber.Contains(search, StringComparison.OrdinalIgnoreCase));
            if (guests == null || !guests.Any())
            {
                return Enumerable.Empty<GuestDTO>();
            }
            return _mapper.Map<IEnumerable<GuestDTO>>(guests);
        }

        public GuestDTO UpdateGuest(GuestUpdateDTO guestUpdateDTO)
        {
            try
            {
                var updatedGuest = _mapper.Map<Guest>(guestUpdateDTO);
                _guestDAL.Update(updatedGuest);
                var result = _mapper.Map<GuestDTO>(updatedGuest);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the guest.", ex);
            }
        }
    }
}
