using OPENAI.Data;
using OPENAI.Interfaces;
using OPENAI.Models;

namespace OPENAI.Services
{
    public class ChatBotService : GenericService<ChatLog>, IChatBotService
    {
        public ChatBotService(ApplicationDbContext context) 
            : base(context) 
        { 
        }
    }
}
