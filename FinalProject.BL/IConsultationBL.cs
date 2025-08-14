using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;

namespace FinalProject.BL
{
    public interface IConsultationBL
    {
        IEnumerable<ConsultationDTO> GetConsultations();
        ConsultationDTO GetById(string id);
        ConsultationDTO AddConsultation(ConsultationInsertDTO consultationInsertDTO);
        ConsultationDTO UpdateConsultation(ConsultationUpdateDTO consultationUpdateDTO);
        void DeleteConsultation(string id);
    }
}
