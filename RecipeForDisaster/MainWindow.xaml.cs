using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Finder;

namespace RecipeForDisaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Recipe> MasterRecipesList = new List<Recipe>();

        RecipeOrganizerEntities Recipes = new RecipeOrganizerEntities();
       
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                CreateMasterList();
                PopulateRecipeListBox();
            }
            catch (Exception ex)
            {
                MessageLabel.Visibility = Visibility.Visible;
                messageTextBox.Visibility = Visibility.Visible;
                messageTextBox.Text = ex.Message;
            }

        }

        private void PopulateRecipeListBox()
        {
            List<string> recipeTitles = (from rec in MasterRecipesList
                select rec.Title).ToList();
            recipeListBox.DataContext = recipeTitles;
        }

        private void CreateMasterList()
        {
            MasterRecipesList = (from rec in Recipes.Recipes
                select rec).ToList();
        }

        private void DisplayRecipe(string title)
        {
            Recipe recipe = (from rec in MasterRecipesList
                             where rec.Title == title
                                    select rec).First();
            
            StringBuilder ingredients = new StringBuilder();
            foreach (Ingredient ingred in recipe.Ingredients)
            {
                ingredients.AppendLine(ingred.Ingredient1);
            }
           
            RecipeTitleTextBox.Text = recipe.Title;
            YieldValueTextBox.Text = recipe.Yield;
            CommentTextBox.Text = recipe.Comment;
            DirectionsTextBox.Text = recipe.Directions;
            IngredientsTextBox.Text = ingredients.ToString();
            RecipeTypeTextBox.Text = recipe.RecipeType;
            ServingSizeTextBox.Text = recipe.ServingSize;
        }

        private void ClearTextBoxes(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox)
                {
                    TextBox tb = (TextBox)visualChild;
                    tb.Clear();
                }
                ClearTextBoxes(visualChild);
            }
            messageTextBox.Text = string.Empty;
            messageTextBox.Visibility = Visibility.Hidden;
            MessageLabel.Visibility = Visibility.Hidden;
         }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(Recipe4DisasterForm);
            PopulateRecipeListBox();
        }

        private void recipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recipeListBox.SelectedItem != null) DisplayRecipe(recipeListBox.SelectedItem.ToString());
        }

        private void Recipe4DisasterForm_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ExceptionalButton_Click(object sender, RoutedEventArgs e)
        {
            messageTextBox.Text = "Bam! An Exception has occured.";
            messageTextBox.Visibility = Visibility.Visible;
            MessageLabel.Visibility = Visibility.Visible;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Find findObj = new Find();
            SearchWindow searchWindow = new SearchWindow();
            if (searchWindow.ShowDialog() == true)
            {
                List<Recipe> refindRecipes = new List<Recipe>();

                foreach (var recipe in MasterRecipesList)
                {
                    string recipeString;
                    List<string> keywords = new List<string>();
                    string rawtext = searchWindow.RequestKeywords;
                    keywords = rawtext.Split(',').ToList();
                    RecipeToString recipeToString = new RecipeToString();
                    recipeString = recipeToString.Convert(recipe);
                    
                    if (findObj.KeywordMatcher(keywords, recipeString))
                    {
                        refindRecipes.Add(recipe);
                    }
                }

                List<string> refinedRecipeTitles = (from rec in refindRecipes
                    select rec.Title).ToList();
                recipeListBox.DataContext = refinedRecipeTitles;
            }
         }
     }
}
