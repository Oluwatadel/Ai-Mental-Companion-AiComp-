using AiComp.Domain.Entities;
using AiComp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AiComp.Application.Interfaces.Repository
{
    public interface IMoodMessageRepository
    {
        public Task<MoodMessage> AddMoodMessages(MoodMessage message);

        public Task<List<MoodMessage>> GetMoodMessages(Guid userId);
        public void Delete(MoodMessage message);
    }
}
