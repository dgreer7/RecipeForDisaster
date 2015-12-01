using System.Text;

namespace RecipeForDisaster
{
    class RecipeToString
    {
        public string Convert(Recipe recipe)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(recipe.Title + " ");
            foreach (Ingredient ingre in recipe.Ingredients)
            {
                sb.Append(ingre + " ");
            }
            sb.Append(recipe.Comment);
            sb.Append(recipe.Directions);
            sb.Append(recipe.RecipeType);
            sb.Append(recipe.ServingSize);
            sb.Append(recipe.Yield);

            return sb.ToString();
        }
    }
}
