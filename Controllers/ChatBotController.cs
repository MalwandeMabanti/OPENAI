﻿using Microsoft.AspNetCore.Mvc;
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
using OPENAI.Interfaces;

namespace OPENAI.Controllers
{
    
    public class ChatBotController : Controller
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
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

                _chatBotService.Add(chatLog);

                Console.WriteLine(result.choices[0].message.content.ToString());

                return View(result);
            }

            return View();
        }


        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var getChatLogs = _chatBotService.GetAll();

            return this.Json(DataSourceLoader.Load(getChatLogs, loadOptions));

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
