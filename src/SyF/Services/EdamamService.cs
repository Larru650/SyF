using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Services
{
    public class EdamamService
    {
        private IConfigurationRoot _config;
        private ILogger<EdamamService> _logger;

        public EdamamService(ILogger<EdamamService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;

        }


        public async Task<RecipeListResult> EdamamAsync(string name)
        {
            var result = new RecipeListResult()  //change, try to find recipe by ingredient first
            {
                Success = false,
                Message = "Failed to get Ingredients"

            };

            var apiKey = _config["Keys:EdamamKey"]; //we add the property in config.json but NOT THE KEY as is going to go to Source Control
            var appId = _config["Keys:EdamamId"];
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"https://api.edamam.com/search?q={encodedName}&app_id=ae930de0&app_key={apiKey}";


            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var json = await client.GetStringAsync(url);

            //unauthorised? authorised if I use app_id and app_key in the url but it does not parse the object(yet to further investigate api documentation)

            JObject results = JObject.Parse(json);
           
            string ingredientQueried = (string)results["q"]; //we parse the results and store the result "q" into string ingredientQueried

            result.RecipeLabel = ingredientQueried;
            
            if(result != null) {

            result.Message = "It worked!";
            result.Success = true; 
            return result;

            }
            else
            {
                return result;
            }
        }
    }
}


//TODO fetch EDAMAM API : extract recipe name
//using (var client = new HttpClient())
//{
//    try
//    {
//        client.BaseAddress = new Uri("http://api.openweathermap.org");
//        var response = await client.GetAsync($"https://api.edamam.com/search?q={encodedName}&app_id=${appId}&app_key=${apiKey}");
//        response.EnsureSuccessStatusCode();

//        var stringResult = await response.Content.ReadAsStringAsync();
//        var rawWeather = JsonConvert.DeserializeObject<IngredientListResult>(stringResult);
//        return Ok(new
//        {
//            Recipe = recipe.title
//        });
//    }
//    catch (HttpRequestException httpRequestException)
//    {
//        return BadRequest($"Error getting recipes from Edamam: {httpRequestException.Message}");
//    }
//}

//}


