using Microsoft.AspNetCore.Mvc;
using OPENAI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OPENAI.Controllers
{
    public class TextEditingController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //await TextEditingAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TextEditingUserInput model)
        {

            await TextEditingAsync(model.Input);
            return View(model);
        }

        public async Task TextEditingAsync(string input)
        {
            HttpClient httpClient = new HttpClient();

            // Set up the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            // Set up the request body
            var data = new
            {
                model = "text-davinci-edit-001",
                input = input,
                instruction = "Fix the spelling mistakes",
            };

            // Convert the request body to JSON
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the OpenAI API
            HttpResponseMessage response = await httpClient.PostAsync("https://api.openai.com/v1/edits", content);

            // Ensure the request was successful
             if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TextEditingRootobject>(responseJson);

                // Display the result
                Console.WriteLine(result.choices[0].text);

            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
