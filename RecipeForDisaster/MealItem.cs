using System.Collections.Generic;

namespace RecipeForDisaster
{
    class MealItem : Recipe
    {
        public string RecipeTypeTitle { get; set; }

        public MealItem(): base()
        {
           Title = "M-" + base.Title;
        }
    }
}
