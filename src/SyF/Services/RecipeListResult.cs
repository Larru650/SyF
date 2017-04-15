namespace SyF.Services
{
    public class RecipeListResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string app_id { get; set; }
        public string app_key { get; set; }
        public string RecipeLabel { get; set; }
        public int RandomFrom { get; set; } //we generate a random number to retrieve a random recipe within the calories and ingredients specified. Created in the loop
        public int FromPlusOne { get; set; } // we add one to the generated random so we only get one recipe i.e: from=6&to=7  from random is 6 so to will be 7
        public int CaloriesLower { get; set; } //calories is a range in edamam, gte = Lower bound and lte upper bound. Example gte300 lte 600 - between 300 and 600 calories
        public int CaloriesUpper { get; set; }
        public int Calories { get; set; } //we create calories prop as I will need to pass it onto the vm anyway
    }
}

//I will want to use an URI like so: "https://api.edamam.com/search?q=chicken&app_id=${YOUR_APP_ID}&app_key=${YOUR_APP_KEY}&from=RandomFrom&to=FromPlusOnecalories=gte%20591,%20lte%20722&health=alcohol-free"

    //will retrieve a random recipe that has between 591 and 722 calories (remember to add %20 after calories=)