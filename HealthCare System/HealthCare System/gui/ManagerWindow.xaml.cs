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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        HealthCareFactory factory;
        public ManagerWindow()
        {
            InitializeComponent();
            factory = new();
            DisplayRooms(factory.RoomController.Rooms);
            DisplayDrugs(factory.DrugController.Drugs);
            DisplayIngredients(factory.IngredientController.Ingredients);
            DisplayEquipment(factory.RoomController.GetEquipmentFromAllRooms());
            InitializeComboBoxes();
        }

        private void DisplayRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                RoomView.Items.Add(room);
            }
        }

        private void DisplayDrugs(List<Drug> drugs)
        {
            foreach (Drug drug in drugs)
            {
                DrugView.Items.Add(drug);
            }
        }

        private void DisplayIngredients(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                IngredientsView.Items.Add(ingredient);
            }
        }

        private void DisplayEquipment(Dictionary<Equipment, int> equipmentAmount)
        {
            foreach (KeyValuePair<Equipment, int> entry in equipmentAmount)
            {
                EquipementView.Items.Add(entry.Key.ToString() + ",  Amount: " + entry.Value);
            }
        }

        private void InitializeComboBoxes()
        {
            RoomTypeFilter.Items.Add("All");
            RoomTypeFilter.SelectedIndex = 0;
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                RoomTypeFilter.Items.Add(roomType);
            }

            EquipementTypeFilter.Items.Add("All");
            EquipementTypeFilter.SelectedIndex = 0;
            foreach (TypeOfEquipment equipmentType in Enum.GetValues(typeof(TypeOfEquipment)))
            {
                EquipementTypeFilter.Items.Add(equipmentType);
            }

            AmountFilter.Items.Add("All");
            AmountFilter.SelectedIndex = 0;
            AmountFilter.Items.Add("Nema na stanju");
            AmountFilter.Items.Add("0-10");
            AmountFilter.Items.Add("10+");   
            
            
        }
        

        private void NewDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RejectedDrugsBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RoomEquipementBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RenovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
