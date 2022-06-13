using System.Windows;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Database;

namespace HealthCare_System.GUI.ManagerView
{
    
    public partial class IngredientWindow : Window
    {
        HealthCareDatabase database;
        bool create;
        Ingredient ingredient;

        IngredientService ingredientService;

        public IngredientWindow(bool create, HealthCareDatabase database, Ingredient ingredient = null)
        {
            InitializeComponent();
            this.create = create;
            this.database = database;
            this.ingredient = ingredient;

            InitializeServices();
            InitializeTitle();
            InitializeFields();
        }

        void InitializeServices()
        {
            ingredientService = new IngredientService(database.IngredientRepo, null);
        }

        void InitializeTitle()
        {
            if (create)
                titleLbl.Content = "Create new ingredient";
            else
                titleLbl.Content = "Update ingredient";
        }

        void InitializeFields()
        {
            if (!create)
                nameTb.Text = ingredient.Name;
            else
                nameTb.Text = "";
        }

        void TryCreation()
        {
            try
            {
                IngredientDto ingredientDto = new IngredientDto(database.IngredientRepo.GenerateId(), nameTb.Text);
                ingredientService.Create(ingredientDto);
                MessageBox.Show("Ingredient created sucessfully!");
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
                IngredientDto ingredientDto = new IngredientDto(-1, nameTb.Text);
                ingredientService.Update(ingredientDto, ingredient);
                MessageBox.Show("Ingredient updated sucessfully!");
                Close();
            }
            catch
            {
                MessageBox.Show("Name must be between 5 and 30 characters!");
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
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
    }
}
