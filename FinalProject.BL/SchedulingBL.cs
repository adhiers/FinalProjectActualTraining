using FinalProject.BL.DTO;
using FinalProject.BL;
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
    public class SchedulingBL : ISchedulingBL
    {
        private readonly IScheduling _schedulingDAL;
        private readonly IMapper _mapper;
        public SchedulingBL(IScheduling schedulingDAL, IMapper mapper)
        {
            _schedulingDAL = schedulingDAL;
            _mapper = mapper;
        }
        public SchedulingDTO AddScheduling(SchedulingInsertDTO schedulingInsertDTO)
        {
            try
            {
                var newScheduling = _mapper.Map<Scheduling>(schedulingInsertDTO);
                var createdScheduling = _schedulingDAL.Create(newScheduling);
                return _mapper.Map<SchedulingDTO>(createdScheduling);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the scheduling.", ex);
            }
        }

        public void DeleteScheduling(int id)
        {
            try
            {
                _schedulingDAL.Delete(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"An error occurred while deleting the scheduling with ID {id}.", ex);
            }
        }

        public SchedulingDTO GetById(int id)
        {
            var schedule = _schedulingDAL.GetById(id);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Scheduling with ID {id} not found.");
            }
            return _mapper.Map<SchedulingDTO>(schedule);
        }

        public IEnumerable<SchedulingDTO> GetSchedulings()
        {
            var schedules = _schedulingDAL.GetAll();
            if (schedules == null || !schedules.Any())
            {
                throw new KeyNotFoundException("No schedules found.");
            }
            return _mapper.Map<IEnumerable<SchedulingDTO>>(schedules);
        }

        public SchedulingDTO UpdateScheduling(SchedulingUpdateDTO schedulingUpdateDTO)
        {
            try
            {
                var existingScheduling = _schedulingDAL.GetById(schedulingUpdateDTO.ScheduleId);
                if (existingScheduling == null)
                {
                    throw new KeyNotFoundException($"Scheduling with ID {schedulingUpdateDTO.ScheduleId} not found.");
                }
                var updatedScheduling = _mapper.Map<Scheduling>(schedulingUpdateDTO);
                var result = _schedulingDAL.Update(updatedScheduling);
                return _mapper.Map<SchedulingDTO>(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the scheduling.", ex);
            }
        }

        public IEnumerable<SchedulingDTO> GetBySearch(string search)
        {
            var schedules = _schedulingDAL.GetBySearch(search);
            if (schedules == null || !schedules.Any())
            {
                throw new KeyNotFoundException($"No schedules found for Guest ID {search}.");
            }
            return _mapper.Map<IEnumerable<SchedulingDTO>>(schedules);
        }
    }
}
