using AiComp.Application.DTOs.ValueObjects;

namespace AiComp.Application.Interfaces.Service
{
    public interface IChatConverseService
    {
        public Task<ChatConverse> CreateChatConverse(Prompt prompt, Response response);
    }
}
