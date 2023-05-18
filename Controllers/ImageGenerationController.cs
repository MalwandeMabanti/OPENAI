using Microsoft.AspNetCore.Mvc;
using OPENAI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OPENAI.Controllers
{
    public class ImageGenerationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImageGenerationUserInput model)
        {

            await ImageGenerationAsync(model.Input);
            return View(model);
        }

        public async Task ImageGenerationAsync(string input) 
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
            HttpResponseMessage response = await httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ImageGenerationRootobject>(responseJson);

                // Display the result
                Console.WriteLine(result.data[0].url);

            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
