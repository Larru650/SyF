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


        public async Task<string> EdamamAsync(string name)
        {
            string result = "no results found";

            var apiKey = _config["Keys:EdamamKey"]; //we add the property in config.json but NOT THE KEY as is going to go to Source Control
            var appId = _config["Keys:EdamamId"];
            var encodedName = WebUtility.UrlEncode(name);
            //var RandomFrom = from; //int, no need to encode
            ////var FromPlusOne = to;

            //testing other resources

            //
            var url = $"https://api.edamam.com/search?q={encodedName}&app_id=ae930de0&app_key={apiKey}";


            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


           //unauthorised? authorised if I use app_id and app_key in the url but it does not parse the object(yet to further investigate api documentation)

            var json = await client.GetStringAsync(url);

            JObject results = JObject.Parse(json);


            string recipeName = (string)results["hits"][0]["recipe"]["label"];
            result = recipeName;
                
                return result;

            
           
        }




        //EdamamAsync overload for newTemporaryRecipeSearches


        public async Task<RecipeListResult> EdamamAsyncGet(string name, string from, string to)
        {
            var result = new RecipeListResult()  //change, try to find recipe by ingredient first
            {
                Success = false,
                Message = "Failed to get Ingredients"

            };

            var apiKey = _config["Keys:EdamamKey"]; //we add the property in config.json but NOT THE KEY as is going to go to Source Control
            var appId = _config["Keys:EdamamId"];
            var encodedName = WebUtility.UrlEncode(name);
            var RandomFrom = Convert.ToInt32(from); //int, no need to encode
            var FromPlusOne = Convert.ToInt32(to);

            //testing other resources

            //
            var url = $"https://api.edamam.com/search?q={encodedName}&app_id=ae930de0&app_key={apiKey}&from={RandomFrom}&to={FromPlusOne}";



            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var json = await client.GetStringAsync(url);

            JObject results = JObject.Parse(json);


            string recipeName = (string)results["hits"][0]["recipe"]["label"]; //we parse the results and store the result "label" into string recipeName
            int calories = (int)results["hits"][0]["recipe"]["calories"]; //as json returns a very large double for "calories" we convert it to an int


            result.RecipeLabel = recipeName;
            result.Calories = calories;


            if (result != null)
            {

                result.Message = "It worked!";
                result.Success = true;
                return result;

            }
            else
            {
                return result;
            }
        }
        public async Task<RecipeListResult> EdamamAsync(string name, int Calories)
        {
            var result = new RecipeListResult()  //change, try to find recipe by ingredient first
            {
                Success = false,
                Message = "Failed to get Ingredients"

            };

            var apiKey = _config["Keys:EdamamKey"]; //we add the property in config.json but NOT THE KEY as is going to go to Source Control
            var appId = _config["Keys:EdamamId"];
            var encodedName = WebUtility.UrlEncode(name);
            

            //testing other resources

            //
            var url = $"https://api.edamam.com/search?q={encodedName}&app_id=ae930de0&app_key={apiKey}&calories={Calories}";
            

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var json = await client.GetStringAsync(url);

            JObject results = JObject.Parse(json);


            string recipeName = (string)results["hits"][0]["recipe"]["label"]; //we parse the results and store the result "label" into string recipeName
            int calories = (int)results["hits"][0]["recipe"]["calories"]; //as json returns a very large double for "calories" we convert it to an int


            result.RecipeLabel = recipeName;
            result.Calories = calories;


            if (result != null)
            {

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


