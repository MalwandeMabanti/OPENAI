using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENAI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OPENAI.Controllers
{
    public class ChatBotController : Controller
    {
        public IActionResult Index()
        {

            //await ChatBotAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ChatBotUserInput model)
        {

            await ChatBotAsync(model.Input);
            return View(model);
        }



        public async Task ChatBotAsync(string input) 
        {
            HttpClient httpClient = new HttpClient();

            // Set up the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            // Set up the request body
            var data = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = input },
                }
            };

            // Convert the request body to JSON
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the OpenAI API
            HttpResponseMessage response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ChatBotRootObject>(responseJson);

                // Display the result
                Console.WriteLine(result.choices[0].message.content.ToString());
                
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
