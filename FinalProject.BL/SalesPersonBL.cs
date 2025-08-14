using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL;
using FinalProject.BO;
using FinalProject.BL.DTO;
using AutoMapper;

namespace FinalProject.BL
{
    public class SalesPersonBL : ISalesPersonBL
    {
        private readonly ISalesPerson _salesPersonDAL;
        private readonly IMapper _mapper;
        public SalesPersonBL(ISalesPerson salesPersonDAL, IMapper mapper)
        {
            _salesPersonDAL = salesPersonDAL;
            _mapper = mapper;
        }
        public SalesPersonDTO CreateSalesPerson(SalesPersonInsertDTO salesPersonInsertDTO)
        {
            try
            {
                if (salesPersonInsertDTO == null)
                {
                    throw new ArgumentNullException(nameof(salesPersonInsertDTO), "SalesPersonInsertDTO cannot be null");
                }
                var newSalesPerson = _mapper.Map<SalesPerson>(salesPersonInsertDTO);
                var createdSalesPerson = _salesPersonDAL.Create(newSalesPerson);
                return _mapper.Map<SalesPersonDTO>(createdSalesPerson);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while creating the sales person.", ex);
            }
        }

        public void DeleteSalesPerson(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("SalesPerson ID cannot be null or empty", nameof(id));
                }
                _salesPersonDAL.Delete(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"An error occurred while deleting the sales person with ID {id}.", ex);
            }
        }

        public IEnumerable<SalesPersonDTO> GetAllSalesPeople()
        {
            var salesPeople = _salesPersonDAL.GetAll();
            var salesPersonDTOs = _mapper.Map<IEnumerable<SalesPersonDTO>>(salesPeople);
            if (salesPeople == null || !salesPeople.Any())
            {
                return Enumerable.Empty<SalesPersonDTO>();
            }
            return salesPersonDTOs;
        }

        public SalesPersonDTO GetById(string id)
        {
            var salesPerson = _salesPersonDAL.GetById(id);
            if (salesPerson == null)
            {
                throw new Exception($"SalesPerson with ID {id} not found.");
            }
            return _mapper.Map<SalesPersonDTO>(salesPerson);
        }

        public SalesPersonDTO UpdateSalesPerson(SalesPersonUpdateDTO salesPersonUpdateDTO)
        {
            try
            {
                if (salesPersonUpdateDTO == null)
                {
                    throw new ArgumentNullException(nameof(salesPersonUpdateDTO), "SalesPersonUpdateDTO cannot be null");
                }
                var existingSalesPerson = _salesPersonDAL.GetById(salesPersonUpdateDTO.SPId);
                if (existingSalesPerson == null)
                {
                    throw new Exception($"SalesPerson with ID {salesPersonUpdateDTO.SPId} not found.");
                }
                var updatedSalesPerson = _mapper.Map<SalesPerson>(salesPersonUpdateDTO);
                var result = _salesPersonDAL.Update(updatedSalesPerson);
                return _mapper.Map<SalesPersonDTO>(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the sales person.", ex);
            }
        }
    }
}
