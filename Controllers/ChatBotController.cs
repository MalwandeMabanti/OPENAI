using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPENAI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using OPENAI.Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;

namespace OPENAI.Controllers
{
    
    public class ChatBotController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ChatBotController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(ChatBotRootObject model)
        {
            HttpResponseMessage response = await ChatBotAsync(model.Input);

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ChatBotRootObject>(responseJson);

                var chatLog = new ChatLog
                {
                    Input = model.Input,
                    Content = result.choices[0].message.content
                };

                this.Add(chatLog);

                Console.WriteLine(result.choices[0].message.content.ToString());

                return View(result);
            }

            return View();
        }


        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var getChatLogs = this.GetAll();

            return this.Json(DataSourceLoader.Load(getChatLogs, loadOptions));

        }




        public List<ChatLog> GetAll()
        {
            return _context.Set<ChatLog>().ToList();
        }

        public void Add(ChatLog entity)
        {
            _context.Set<ChatLog>().Add(entity);
            _context.SaveChanges();
        }

        public async Task<HttpResponseMessage> ChatBotAsync(string input) 
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

            return await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        }
    }
}
