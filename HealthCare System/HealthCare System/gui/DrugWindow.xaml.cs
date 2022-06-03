using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HealthCare_System.factory;
using HealthCare_System.Model;
using HealthCare_System.Database;

namespace HealthCare_System.gui
{
    
    public partial class DrugWindow : Window
    {
        HealthCareFactory factory;
        HealthCareDatabase database;
        bool create;
        Drug drug;
        Dictionary<int, Ingredient> listedIngredients = new Dictionary<int, Ingredient>();
        List<int> selectedIndencies = new List<int>();
        public DrugWindow(HealthCareFactory factory, bool create, HealthCareDatabase database, Drug drug = null)
        {
            InitializeComponent();
            this.factory = factory;
            this.database = database;
            this.create = create;
            this.drug = drug;
            InitializeTitle();
            InitializeFields();
        }

        void InitializeTitle()
        {
            if (create)
                titleLbl.Content = "Create new drug";
            else
                titleLbl.Content = "Update drug";
        }

        void InitializeFields()
        {
            DisplayIngredients(factory.IngredientController.Ingredients);
            if (!create)
            {
                nameTb.Text = drug.Name;
                SetSelectedIngredients(drug.Ingredients);
            }
            else
            {
                nameTb.Text = "";
            }
                
        }

        void SetSelectedIngredients(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredientsView.SelectedItems.Add(ingredient);
            }
        }

        void SetSelectedIndecies()
        {
            selectedIndencies.Clear();
            foreach (object o in ingredientsView.SelectedItems)
            {
                selectedIndencies.Add(ingredientsView.Items.IndexOf(o));
            }
        }

        List<Ingredient> GetSelectedIngredients()
        {
            SetSelectedIndecies();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (int i in selectedIndencies)
            {
                ingredients.Add(listedIngredients[i]);
            }
            return ingredients;
        }

        private void DisplayIngredients(List<Ingredient> ingredients)
        {
            ingredientsView.Items.Clear();
            int index = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                ingredientsView.Items.Add(ingredient.Name);
                listedIngredients[index] = ingredient;
                index++;
            }
            ingredientsView.SelectedIndex = 0;
        }

        void TryCreation()
        {
            try
            {
                factory.DrugController.CreateNewDrug(nameTb.Text, GetSelectedIngredients());
                MessageBox.Show("Drug created sucessfully!");
                Close();
            }
            catch
            {
                MessageBox.Show("Name must be between 5 and 30 characters!");
            }
        }

        void TryUpdate()
        {
            try
            {
                factory.DrugController.UpdateDrug(nameTb.Text, GetSelectedIngredients() ,drug);
                MessageBox.Show("Drug updated sucessfully!");
                Close();
            }
            catch
            {
                MessageBox.Show("Name must be between 5 and 30 characters!");
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectedIngredients().Count > 0)
            {
                if (create)
                {
                    TryCreation();
                }
                else
                {
                    TryUpdate();
                }
            }
            else
            {
                MessageBox.Show("You must select atleast one ingredient!");
                return;
            }
        }
    }
}
