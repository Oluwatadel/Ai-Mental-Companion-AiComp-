
namespace AiComp.Application.DTOs.ValueObjects
{
    public class ChatConverse
    {
        public static int Id { get; private set; } = 0;
        public Response Response { get; private set; }
        public Prompt Prompt { get; private set; }

        public ChatConverse(Prompt prompt, Response response) 
        {
            Id += 1;
            Prompt = prompt;
            Response = response;
        }
    }
}
