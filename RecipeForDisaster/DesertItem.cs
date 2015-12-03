namespace RecipeForDisaster
{
    class DesertItem : Recipe
{
        public string RecipeTypeTitle { get; set; }

        public DesertItem(): base()
        {
           Title = "D-" + base.Title;
        }
    }
}
