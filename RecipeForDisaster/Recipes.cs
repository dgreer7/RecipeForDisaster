namespace RecipeForDisaster
{
    internal class Recipes
    {
        internal readonly int NumberOfRecipes;
        private int recipeCountInt = 0;

        private Recipe[] recipeArray;

        internal Recipes(int numberOfRecipes)
        {
            NumberOfRecipes = numberOfRecipes;
            recipeArray = new Recipe[NumberOfRecipes];
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
    }
}
