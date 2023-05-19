using OPENAI.Data;
using OPENAI.Interfaces;
using OPENAI.Models;

namespace OPENAI.Services
{
    public class TextEditingService : GenericService<TextLog>, ITextEditingService
    {
        public TextEditingService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
