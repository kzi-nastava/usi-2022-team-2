using System.Windows;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Ingredients;

namespace HealthCare_System.GUI.ManagerView
{
    
    public partial class IngredientWindow : Window
    {
        ServiceBuilder serviceBuilder;
        bool create;
        Ingredient ingredient;

        IngredientController ingredientController;

        public IngredientWindow(bool create, ServiceBuilder serviceBuilder, Ingredient ingredient = null)
        {
            InitializeComponent();
            this.create = create;
            this.serviceBuilder = serviceBuilder;
            this.ingredient = ingredient;

            InitializeControllers();
            InitializeTitle();
            InitializeFields();
        }

        void InitializeControllers()
        {
            ingredientController = new(serviceBuilder.IngredientService);
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
                IngredientDto ingredientDto = new IngredientDto(ingredientController.GenerateId(), nameTb.Text);
                ingredientController.Create(ingredientDto);
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
                ingredientController.Update(ingredientDto, ingredient);
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
