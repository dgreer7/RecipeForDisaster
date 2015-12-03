using System.Collections.Generic;
using System.Linq;

namespace RecipeForDisaster
{
    internal class Recipes
    {
        public List<Recipe> MasterRecipesList { get; set; }
        RecipeOrganizerEntities recipes = new RecipeOrganizerEntities();

        internal readonly int NumberOfRecipes;
        private int recipeCountInt = 0;

        private Recipe[] recipeArray;

        public Recipes()
        {
            CreateMasterList();
        }
        
        internal Recipes(int numberOfRecipes)
        {
            NumberOfRecipes = numberOfRecipes;
            recipeArray = new Recipe[NumberOfRecipes];
        }

        private void CreateMasterList()
        {
            MasterRecipesList = (from rec in recipes.Recipes
                select rec).ToList();
            CastCollections();
        }

        internal Recipe this[int index]
        {
            get
            {
                Recipe rec = null;

                if (index >= 0 && index < NumberOfRecipes)
                {
                    rec = recipeArray[index];
                }

                return rec;
            }
            private set { recipeArray[index] = value; }
        }

        private void CastCollections()
        {
            foreach (var recipe in MasterRecipesList)
            {
                switch (recipe.RecipeType.Trim())
                {
                    case "Meal Item":
                        MealItem meal = new MealItem();
                        meal.RecipeType = recipe.RecipeType;
                        meal.Comment = recipe.Comment;
                        meal.Directions = recipe.Directions;
                        meal.Ingredients = recipe.Ingredients;
                        meal.ServingSize = recipe.ServingSize;
                        meal.Title = recipe.Title;
                        meal.Yield = recipe.Yield;
                        MasterRecipesList.Remove(recipe);
                        MasterRecipesList.Add(meal);
                        break;

                    case "Desert":
                        DesertItem desert = new DesertItem();
                        desert.RecipeType = recipe.RecipeType;
                        desert.Comment = recipe.Comment;
                        desert.Directions = recipe.Directions;
                        desert.Ingredients = recipe.Ingredients;
                        desert.ServingSize = recipe.ServingSize;
                        desert.Title = recipe.Title;
                        desert.Yield = recipe.Yield;
                        MasterRecipesList.Remove(recipe);
                        MasterRecipesList.Add(desert);
                        break;
                }    
            }
            
        }
    }
}
