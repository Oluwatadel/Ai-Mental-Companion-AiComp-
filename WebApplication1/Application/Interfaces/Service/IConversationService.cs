using AiComp.Application.DTOs;
using AiComp.Application.DTOs.ValueObjects;
using AiComp.Controllers;
using AiComp.Domain.Entities;

namespace AiComp.Application.Interfaces.Service
{
    public interface IConversationService
    {
        public Task<Conversation> AddConversation(User user, Conversation conversation);
        public Task<Conversation> AddChatToConversation(Conversation Conversation, ChatConverse chat);
        public Task<BaseResponse<List<ChatConverse>>> GetChatConverses(User user);
    }
}
