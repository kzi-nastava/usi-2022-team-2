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
        public ManagerWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            InitializeComboBoxes();
            DisplayRooms(this.factory.RoomController.Rooms);
            DisplayDrugs(this.factory.DrugController.Drugs);
            DisplayIngredients(this.factory.IngredientController.Ingredients);
            DisplayEquipment(this.factory.RoomController.GetEquipmentFromAllRooms());
            
        }

        #region EquipmentFiltering

        private void ApplyEveryEquipmentFilter()
        {
            if (RoomTypeFilter.SelectedIndex != -1 && AmountFilter.SelectedIndex != -1 && EquipementTypeFilter.SelectedIndex != -1)
            {
                Dictionary<Equipment, int> equipmentAmount = factory.RoomController.GetEquipmentFromAllRooms();
                string roomType = RoomTypeFilter.SelectedItem.ToString();
                string amount = AmountFilter.SelectedItem.ToString();
                string equipmentType = EquipementTypeFilter.SelectedItem.ToString();
                factory.ApplyEquipmentFilters(roomType, amount, equipmentType, equipmentAmount);
                DisplayEquipment(equipmentAmount);
            }
        }

        private void DisplayEquipment(Dictionary<Equipment, int> equipmentAmount)
        {
            EquipementView.Items.Clear();
            if (equipmentAmount.Count == 0)
            {
                EquipementView.Items.Add("No data");
            }
            else
            {
                foreach (KeyValuePair<Equipment, int> entry in equipmentAmount)
                {
                    EquipementView.Items.Add(entry.Key.ToString() + ",  Amount: " + entry.Value);
                }
            }            
        }

        private void RoomTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
        }

        private void AmountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
        }

        private void EquipementTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
        }

        #endregion

        private void DisplayRooms(List<Room> rooms)
        {
            RoomView.Items.Clear();
            foreach (Room room in rooms)
            {
                RoomView.Items.Add(room);
            }
        }

        private void DisplayDrugs(List<Drug> drugs)
        {
            DrugView.Items.Clear();
            foreach (Drug drug in drugs)
            {
                DrugView.Items.Add(drug);
            }
        }

        private void DisplayIngredients(List<Ingredient> ingredients)
        {
            IngredientsView.Items.Clear();
            foreach (Ingredient ingredient in ingredients)
            {
                IngredientsView.Items.Add(ingredient);
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

            AmountFilter.Items.Add("All");
            AmountFilter.SelectedIndex = 0;
            AmountFilter.Items.Add("Nema na stanju");
            AmountFilter.Items.Add("0-10");
            AmountFilter.Items.Add("10+");

            EquipementTypeFilter.Items.Add("All");
            EquipementTypeFilter.SelectedIndex = 0;
            foreach (TypeOfEquipment equipmentType in Enum.GetValues(typeof(TypeOfEquipment)))
            {
                EquipementTypeFilter.Items.Add(equipmentType);
            }

             
            
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
