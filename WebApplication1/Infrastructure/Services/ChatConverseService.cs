using AiComp.Application.DTOs.ValueObjects;
using AiComp.Application.Interfaces.Service;
using AiComp.Domain.Entities;

namespace AiComp.Infrastructure.Services
{
    public class ChatConverseService : IChatConverseService
    {
        private readonly IAiServices _aiService;

        public ChatConverseService(IAiServices aiService)
        {
            _aiService = aiService;
        }

        public async Task<ChatConverse> CreateChatConverse(Prompt prompt, Response response)
        {
            var chatConverse = new ChatConverse(prompt, response);
            return await Task.FromResult(chatConverse);
        }
    }
}
