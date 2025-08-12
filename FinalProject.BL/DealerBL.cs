using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;
using FinalProject.BO;
using FinalProject.DAL;
using AutoMapper;

namespace FinalProject.BL
{
    public class DealerBL : IDealerBL
    {
        private readonly IMapper _mapper;
        private readonly IDealer _dealerDAL;
        public DealerBL(IDealer dealerDAL, IMapper mapper)
        {
            _dealerDAL = dealerDAL;
            _mapper = mapper;
        }
        public DealerDTO AddDealer(DealerInsertDTO dealerInsertDTO)
        {
            try
            {
                var newDealer = _mapper.Map<Dealer>(dealerInsertDTO);
                _dealerDAL.Create(newDealer);
                var result = _mapper.Map<DealerDTO>(newDealer);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding the dealer.", ex);
            }
        }

        public void DeleteDealer(string dealerId)
        {
            try
            {
                _dealerDAL.Delete(dealerId);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the dealer.", ex);
            }
        }

        public DealerDTO GetById(string dealerId)
        {
            var dealer = _dealerDAL.GetById(dealerId);
            if (dealer == null)
            {
                throw new Exception("Dealer not found");
            }
            var dealerDTO = _mapper.Map<DealerDTO>(dealer);
            return dealerDTO;
        }

        public IEnumerable<DealerDTO> GetDealers()
        {
            var dealers = _dealerDAL.GetAll();
            var dealerDTOs = _mapper.Map<IEnumerable<DealerDTO>>(dealers);
            if (dealerDTOs == null || !dealerDTOs.Any())
            {
                throw new Exception("No dealers found");
            }
            return dealerDTOs;
        }

        //public IEnumerable<DealerDTO> SearchDealers(string search)
        //{
        //    var dealers = _dealerDAL.GetBySearch(search);
        //    if (dealers == null || !dealers.Any())
        //    {
        //        throw new Exception("No dealers found matching the search criteria");
        //    }
        //    var dealerDTOs = _mapper.Map<IEnumerable<DealerDTO>>(dealers);
        //    return dealerDTOs;
        //}

        public DealerDTO UpdateDealer(DealerUpdateDTO dealerUpdateDTO)
        {
            try
            {
                var editDealer = _mapper.Map<Dealer>(dealerUpdateDTO);
                _dealerDAL.Update(editDealer);
                var result = _mapper.Map<DealerDTO>(editDealer);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the dealer.", ex);
            }
        }
    }
}
