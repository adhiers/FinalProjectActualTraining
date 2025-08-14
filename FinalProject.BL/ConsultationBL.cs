using FinalProject.BL.DTO;
using FinalProject.BO;
using FinalProject.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BL
{
    public class ConsultationBL : IConsultationBL
    {
        private readonly IConsultation _consultationDAL;
        private readonly IMapper _mapper;
        public ConsultationBL(IConsultation consultationDAL, IMapper mapper)
        {
            _consultationDAL = consultationDAL;
            _mapper = mapper;
        }
        public ConsultationDTO AddConsultation(ConsultationInsertDTO consultationInsertDTO)
        {
            try
            {
                var consultation = _mapper.Map<Consultation>(consultationInsertDTO);
                var createdConsultation = _consultationDAL.Create(consultation);
                if (createdConsultation == null)
                {
                    throw new Exception("Failed to create consultation.");
                }
                return _mapper.Map<ConsultationDTO>(createdConsultation);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the consultation.", ex);
            }
        }

        public void DeleteConsultation(string id)
        {
            try
            {
                _consultationDAL.Delete(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"An error occurred while deleting the consultation with ID {id}.", ex);
            }
        }

        public ConsultationDTO GetById(string id)
        {
            try
            {
                var consultation = _consultationDAL.GetById(id);
                if (consultation == null)
                {
                    throw new KeyNotFoundException($"Consultation with ID {id} not found.");
                }
                return _mapper.Map<ConsultationDTO>(consultation);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"An error occurred while retrieving the consultation with ID {id}.", ex);
            }
        }

        public IEnumerable<ConsultationDTO> GetConsultations()
        {
            var consultations = _consultationDAL.GetAll();
            if (consultations == null || !consultations.Any())
            {
                throw new KeyNotFoundException("No consultations found.");
            }
            return _mapper.Map<IEnumerable<ConsultationDTO>>(consultations);
        }

        public ConsultationDTO UpdateConsultation(ConsultationUpdateDTO consultationUpdateDTO)
        {
            try
            {
                var updatedConsultation = _mapper.Map<Consultation>(consultationUpdateDTO);
                var consultation = _consultationDAL.Update(updatedConsultation);
                if (consultation == null)
                {
                    throw new KeyNotFoundException($"Consultation with ID {consultationUpdateDTO.ConsultId} not found.");
                }
                return _mapper.Map<ConsultationDTO>(consultation);
            }
        }
    }
}
