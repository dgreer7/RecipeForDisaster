using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeForDisaster
{
    class Recipe
    {
        public string RecipeName { get; set; }
        public string Yield { get; set; }
        public string ServingSize { get; set; }
        public string Directions { get; set; }
        public string Comments { get; set; }
        public string RecipeType { get; set; }
        public string ListOfIngredients { get; set; }
    }
}