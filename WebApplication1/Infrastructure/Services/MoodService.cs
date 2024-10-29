using AiComp.Application.DTOs;
using AiComp.Application.DTOs.ValueObjects;
using AiComp.Application.Interfaces.Repository;
using AiComp.Application.Interfaces.Service;
using AiComp.Domain.Entities;

namespace AiComp.Infrastructure.Services
{
    public class MoodService : IMoodService
    {
        private readonly IMoodLogRepository _moodLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MoodService(IMoodLogRepository moodLogRepository, IUnitOfWork unitOfWork)
        {
            _moodLogRepository = moodLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<MoodLog>> AddMoodLog(User user, SentimentPrediction predict)
        {
            var moodLog = new MoodLog(user.Id);
            moodLog.SetUserObject(user);
            moodLog.SetMood(predict);
            var response = new BaseResponse<MoodLog>();
            var changes = await _unitOfWork.SaveChanges();
            if (changes == 0)
            {
                response.SetValues("Something went wrong", "Unsuccessful", null);
                return response;
            }
            await _moodLogRepository.AddMoodLog(moodLog);
            response.SetValues("MoodLog added successfully", "Successful", moodLog);
            return response;
        }

        public async Task<BaseResponse<MoodLog>> UpdateMoodLog(User user, Guid moodLogId, DateTime today, SentimentPrediction prediction)
        {
            var moodLog = user.MoodLogs.FirstOrDefault(a => a.Id == moodLogId && a.Timestamp.Date == today.Date);
            var response = new BaseResponse<MoodLog>();
            if (moodLog == null)
            {
                response.SetValues("Something went wrong", "Unsuccessful", null);
                return response;
            }

            moodLog.UpdateModeOfTheSameDay(prediction);
            await _unitOfWork.SaveChanges();
            response.SetValues("MoodLog added successfully", "Successful", moodLog);
            return response;
        }

        public async Task<ICollection<MoodLog>> ViewMoodLogs(User user)
        {
            var logs = user.MoodLogs;
            if (logs.Count() > 0) return null;
            return logs.ToList();
        }

        public async Task<MoodLog> ViewMoodLogsByTime(User user, DateTime date)
        {
            var logs = user.MoodLogs.FirstOrDefault(a => a.Timestamp.Date == date.Date);
            return await Task.FromResult(logs);
        }
    }
}
