using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using OPENAI.Interfaces;
using OPENAI.Models;
using OPENAI.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OPENAI.Controllers
{
    public class ImageGenerationController : Controller
    {
        private readonly IImageGenerationService _imageGenerationService;

        public ImageGenerationController(IImageGenerationService imageGenerationService) 
        {
            _imageGenerationService = imageGenerationService;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ImageGenerationRootobject model) 
        {
            HttpResponseMessage response = await ImageGenerationAsync(model.Input);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ImageGenerationRootobject>(responseJson);

                var imageLog = new ImageLog
                {
                    Input = model.Input,
                    Link = result.data[0].url
                };

                _imageGenerationService.Add(imageLog);

                // Display the result
                Console.WriteLine(result.data[0].url);

                return View(result);

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var getImageLogs = _imageGenerationService.GetAll();

            return this.Json(DataSourceLoader.Load(getImageLogs, loadOptions));

        }

        public async Task<HttpResponseMessage> ImageGenerationAsync(string input) 
        {
            HttpClient httpClient = new HttpClient();

            // Set up the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            // Set up the request body
            var data = new
            {
                prompt = input,
                n= 1,
                size = "1024x1024"
            };

            // Convert the request body to JSON
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the OpenAI API
            return await httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);

        }
    }
}
