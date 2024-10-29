using AiComp.Application.DTOs;
using AiComp.Application.DTOs.ValueObjects;
using AiComp.Application.Interfaces.Repository;
using AiComp.Application.Interfaces.Service;
using AiComp.Controllers;
using AiComp.Domain.Entities;

namespace AiComp.Infrastructure.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IChatConverseRepository _chatConverseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConversationService(IConversationRepository conversationRepository, IUnitOfWork unitOfWork, IChatConverseRepository chatConverseRepository)
        {
            _conversationRepository = conversationRepository;
            _unitOfWork = unitOfWork;
            _chatConverseRepository = chatConverseRepository;
        }

        public async Task<Conversation> AddChatToConversation(Conversation conversation, ChatConverse chat)
        {
            conversation.AddPromptAndResponse(chat);
            var changes = await _unitOfWork.SaveChanges();
            if(changes == 0)
            {
                return null;
            }

            return conversation;
        }

        public async Task<Conversation> AddConversation(User user, Conversation conversation)
        {
            user.AddConversation(conversation);
            await _conversationRepository.AddConversationAsync(conversation);
            var changes = await _unitOfWork.SaveChanges();
            if (changes == 0)
            {
                return null;
            }

            return conversation;
        }

        public async Task<BaseResponse<List<ChatConverse>>> GetChatConverses(User user)
        {
            var response = new BaseResponse<List<ChatConverse>>();
            var conversations = await _chatConverseRepository.GetChatConverse(user.Conversation!.Id);
            if(conversations.Count() == 0)
            {
                response.SetValues("No COnversation Yet!!!", "Conversation Empty", null);
                return response;
            }

            response.SetValues($"Chat Found", "Successful", conversations);
            return response;            
        }
    }
}
