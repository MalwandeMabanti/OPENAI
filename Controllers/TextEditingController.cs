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

    public class TextEditingController : Controller
    {
        private readonly ITextEditingService _textEditingService;

        public TextEditingController(ITextEditingService textEditingService)
        {
            _textEditingService = textEditingService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TextEditingRootobject model)
        {
            HttpResponseMessage response = await TextEditingAsync(model.Input);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TextEditingRootobject>(responseJson);

                var textLog = new TextLog
                {
                    Input = model.Input,
                    Text = result.choices[0].text
                };

                _textEditingService.Add(textLog);

                // Display the result
                Console.WriteLine(result.choices[0].text);

                return View(result);

            }
            return View();
        }

        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var getChatLogs = _textEditingService.GetAll();

            return this.Json(DataSourceLoader.Load(getChatLogs, loadOptions));

        }


        public async Task<HttpResponseMessage> TextEditingAsync(string input)
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
            return await httpClient.PostAsync("https://api.openai.com/v1/edits", content);

        }
    }
}