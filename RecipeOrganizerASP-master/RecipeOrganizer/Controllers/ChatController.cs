using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RecipeOrganizer.Controllers
{
	public class ChatController : Controller
	{
		private readonly IConfiguration configuration;
		List<string> conversationHistory;
		private readonly HttpClient httpClient;

		public ChatController(IConfiguration configuration)
		{
			this.configuration = configuration;
			conversationHistory = new List<string>();
			httpClient = new HttpClient();
		}

		public async Task<IActionResult> SendMessage(string userMessage)
		{
			// Get the API key from configuration
			var apiKey = configuration["OpenAI:ApiKey"];

			// Construct the request payload with the conversation history
			var requestBody = new
			{
				messages = new List<object>
		{
			new { role = "system", content = "You" },
			new { role = "user", content = userMessage }
		},
				max_tokens = 100,
				temperature = 0.6,
				// Additional parameters based on OpenAI GPT-3 API documentation
			};

			// Serialize the request payload to JSON
			var jsonPayload = JsonConvert.SerializeObject(requestBody);

			// Set the API endpoint URL
			var apiUrl = "https://api.openai.com/v1/completions";

			try
			{
				// Set the request headers
				httpClient.DefaultRequestHeaders.Clear();
				httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// Send the HTTP POST request to the OpenAI GPT-3 API
				var response = await httpClient.PostAsync(apiUrl, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

				// Check if the request was successful
				if (response.IsSuccessStatusCode)
				{
					// Read and parse the API response
					var responseContent = await response.Content.ReadAsStringAsync();
					var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

					// Extract the generated response from the API response
					var generatedResponse = responseObject.choices[0].text;

					// Add the generated response to the conversation history
					conversationHistory.Add(generatedResponse);

					return View("Index", conversationHistory);
				}
				else
				{
					// Handle error response from the API
					return View();
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex.ToString());
				// Handle any exceptions that occur during the API call
				return View();
			}
		}
	}
}
