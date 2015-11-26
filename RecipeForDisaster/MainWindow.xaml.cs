using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeForDisaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecipeOrganizerEntities Recipes = new RecipeOrganizerEntities();
       
        

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Recipe recipe1 = new Recipe()
        //{
        //    RecipeName = "Lasagna",
        //    Yield = "4 People",
        //    Comments = "Something goes here",
        //    Directions = "Don't burn it",
        //    ListOfIngredients = "Pasta things",
        //    RecipeType = "Italian",
        //    ServingSize = "4oz"
        //};

        //Recipe recipe2 = new Recipe()
        //{
        //    RecipeName = "Pasta",
        //    Yield = "3 Dragons",
        //    Comments = "Nothing",
        //    Directions = "Go Left",
        //    ListOfIngredients = "Noodles",
        //    RecipeType = "Greek",
        //    ServingSize = "50oz"
        //};

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                

                //recipeListBox.Items.Add(recipe1.RecipeName);
                //recipeListBox.Items.Add(recipe2.RecipeName);
            }
            catch (Exception ex)
            {
                MessageLabel.Visibility = Visibility.Visible;
                messageTextBox.Visibility = Visibility.Visible;
                messageTextBox.Text = ex.Message;
            }
            
        }

        private void DisplayRecipe()
        {
            //Recipe recipeToDisplay;
            //if (recipeListBox.SelectedItem == recipe1.RecipeName)
            //{
            //    recipeToDisplay = recipe1;
            //}
            //else
            //recipeToDisplay = recipe2;

            //RecipeTitleTextBox.Text = recipeToDisplay.RecipeName;
            //YieldValueTextBox.Text = recipeToDisplay.Yield;
            //CommentTextBox.Text = recipeToDisplay.Comments;
            //DirectionsTextBox.Text = recipeToDisplay.Directions;
            //IngredientsTextBox.Text = recipeToDisplay.ListOfIngredients;
            //RecipeTypeTextBox.Text = recipeToDisplay.RecipeType;
            //ServingSizeTextBox.Text = recipeToDisplay.ServingSize;
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
        }

        private void recipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayRecipe();
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


    }
}
