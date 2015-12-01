using System.Collections.Generic;

namespace RecipeForDisaster
{
    class Results
    {
        public List<Recipe> RefinedRecipes  { get; set; }

        public void Add(Recipe recipe)
        {
            RefinedRecipes.Add(recipe);
        }
     }
}
