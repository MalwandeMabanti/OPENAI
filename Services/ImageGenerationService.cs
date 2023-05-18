using OPENAI.Data;
using OPENAI.Interfaces;
using OPENAI.Models;

namespace OPENAI.Services
{
    public class ImageGenerationService : GenericService<ImageLog>, IImageGenerationService
    {
        public ImageGenerationService(ApplicationDbContext context) 
            : base(context) 
        { 
        }
    }
}
