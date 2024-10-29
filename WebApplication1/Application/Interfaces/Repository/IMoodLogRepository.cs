using AiComp.Domain.Entities;

namespace AiComp.Application.Interfaces.Repository
{
    public interface IMoodLogRepository
    {
        public Task<MoodLog> AddMoodLog(MoodLog moodLog);
        public Task<MoodLog> UpdateMoodLog(MoodLog moodLog);
    }
}
