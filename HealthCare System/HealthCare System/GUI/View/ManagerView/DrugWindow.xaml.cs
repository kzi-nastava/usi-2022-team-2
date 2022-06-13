using System.Collections.Generic;
using System.Windows;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Database;

namespace HealthCare_System.GUI.ManagerView
{
    
    public partial class DrugWindow : Window
    {
        HealthCareDatabase database;
        bool create;
        Drug drug;
        Dictionary<int, Ingredient> listedIngredients = new Dictionary<int, Ingredient>();
        List<int> selectedIndencies = new List<int>();

        DrugService drugService;

        public DrugWindow(bool create, HealthCareDatabase database, Drug drug = null)
        {
            InitializeComponent();
            this.database = database;
            this.create = create;
            this.drug = drug;
            InitializeServices();
            InitializeTitle();
            InitializeFields();
        }

        void InitializeServices()
        {
            drugService = new DrugService(database.DrugRepo, null);
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
            DisplayIngredients(database.IngredientRepo.Ingredients);
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
                DrugDto drugDto = new DrugDto(database.DrugRepo.GenerateId(), nameTb.Text, 
                    GetSelectedIngredients(), DrugStatus.ON_HOLD, "");
                drugService.CreateNew(drugDto);
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
                DrugDto drugDto = new DrugDto(-1, nameTb.Text,
                   GetSelectedIngredients(), DrugStatus.ON_HOLD, "");
                drugService.Update(drugDto ,drug);
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
