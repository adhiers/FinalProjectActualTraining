using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinalProject.BL.DTO;
using FinalProject.BO;
using FinalProject.DAL;

namespace FinalProject.BL
{
    public class DealerCarBL : IDealerCarBL
    {
        private readonly IMapper _mapper;
        private readonly IDealerCar _dealerCarDAL;
        public DealerCarBL(IDealerCar dealerCarDAL, IMapper mapper)
        {
            _dealerCarDAL = dealerCarDAL;
            _mapper = mapper;
        }
        public DealerCarDTO AddDealerCar(DealerCarInsertDTO dealerCarInsertDTO)
        {
            try
            {
                var newDealerCar = _mapper.Map<DealerCar>(dealerCarInsertDTO);
                var addedDealerCar = _dealerCarDAL.Create(newDealerCar);
                if (addedDealerCar == null)
                {
                    throw new Exception("Failed to add the dealer car.");
                }
                var dealerCarDTO = _mapper.Map<DealerCarDTO>(addedDealerCar);
                return dealerCarDTO;
            }
            catch (ArgumentException aEx)
            {
                throw new ArgumentException(aEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the dealer car.", ex);
            }
        }

        public void DeleteDealerCar(string dealerCarId)
        {
            try
            {
                _dealerCarDAL.Delete(dealerCarId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<DealerCarDTO> GetAllDealerCars()
        {
            var dealerCars = _dealerCarDAL.GetAll();
            var dealerCarDTOs = _mapper.Map<IEnumerable<DealerCarDTO>>(dealerCars);
            return dealerCarDTOs;
        }

        public DealerCarDTO GetDealerCarById(string dealerCarId)
        {
            var dealerCar = _dealerCarDAL.GetById(dealerCarId);
            if (dealerCar == null)
            {
                throw new ArgumentException($"DealerCar with ID {dealerCarId} not found.");
            }
            var dealerCarDTO = _mapper.Map<DealerCarDTO>(dealerCar);
            return dealerCarDTO;
        }

        public DealerCarDTO UpdateDealerCar(DealerCarUpdateDTO dealerCarUpdateDTO)
        {
            try
            {
                var updateDealerCar = _mapper.Map<DealerCar>(dealerCarUpdateDTO);
                var updatedDealerCar = _dealerCarDAL.Update(updateDealerCar);
                if (updatedDealerCar == null)
                {
                    throw new Exception("Failed to update the dealer car.");
                }
                var dealerCarDTO = _mapper.Map<DealerCarDTO>(updatedDealerCar);
                return dealerCarDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
