﻿using AiComp.Application.Interfaces.Repository;
using AiComp.Domain.Entities;
using AiComp.Infrastructure.Persistence.Context;

namespace AiComp.Infrastructure.Persistence.Repositories
{
    public class MoodLogRepository : IMoodLogRepository
    {
        private readonly AiCompDBContext _aiCompDBContext;

        public MoodLogRepository(AiCompDBContext aiCompDBContext)
        {
            _aiCompDBContext = aiCompDBContext;
        }

        public async Task<MoodLog> AddMoodLog(MoodLog moodLog)
        {
            await _aiCompDBContext.MoodLogs.AddAsync(moodLog);
            return moodLog;
        }

        public async Task<MoodLog> UpdateMoodLog(MoodLog moodLog)
        {
            await Task.FromResult(_aiCompDBContext.MoodLogs.Update(moodLog));
            return moodLog;
        }
    }
}