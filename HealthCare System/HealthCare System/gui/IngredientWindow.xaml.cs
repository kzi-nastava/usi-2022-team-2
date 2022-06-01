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
using HealthCare_System.entities;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for IngredientWindow.xaml
    /// </summary>
    public partial class IngredientWindow : Window
    {
        HealthCareFactory factory;
        bool create;
        Ingredient ingredient;
        public IngredientWindow(bool create, HealthCareFactory factory, Ingredient ingredient = null)
        {
            InitializeComponent();
            this.create = create;
            this.factory = factory;
            this.ingredient = ingredient;
            InitializeTitle();
            InitializeFields();
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
                factory.IngredientController.CreateNewIngredient(nameTb.Text);
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
                factory.IngredientController.UpdateIngredient(nameTb.Text, ingredient);
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
