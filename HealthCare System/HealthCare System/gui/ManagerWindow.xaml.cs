using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.factory;
using HealthCare_System.entities;

namespace HealthCare_System.gui
{
    public partial class ManagerWindow : Window
    {
        HealthCareFactory factory;
        Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
        Dictionary<int, Room> listedRooms = new Dictionary<int, Room>();
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();
        Dictionary<int, Ingredient> listedIngredients = new Dictionary<int, Ingredient>();

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
                equipmentAmount = factory.RoomController.GetEquipmentFromAllRooms();
                string roomType = RoomTypeFilter.SelectedItem.ToString();
                string amount = AmountFilter.SelectedItem.ToString();
                string equipmentType = EquipementTypeFilter.SelectedItem.ToString();
                factory.ApplyEquipmentFilters(roomType, amount, equipmentType, equipmentAmount);
                DisplayEquipment(equipmentAmount);
            }
        }

        private void ExecuteEquipmentQuery(string value)
        {
            if (value.Length != 0)
            {
                factory.EquipmentController.EquipmentQuery(value, equipmentAmount);
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
            ExecuteEquipmentQuery(SearchTb.Text);
        }

        private void AmountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(SearchTb.Text);
        }

        private void EquipementTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(SearchTb.Text);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(SearchTb.Text);
        }
        #endregion


        #region Rooms
        private void DisplayRooms(List<Room> rooms)
        {
            RoomView.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                RoomView.Items.Add(room.Name);
                listedRooms[index] = room;
                index++;
            }
            RoomView.SelectedIndex = 0;
        }

        private void NewRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Window newRoomWindow = new RoomWindow(true, factory);
            newRoomWindow.Show();
        }

        private void DeleteRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void RenovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void UpdateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Window updateRoomWindow = new RoomWindow(false, factory, listedRooms[RoomView.SelectedIndex]);
            updateRoomWindow.Show();
        }

        private void MoveEquipementBtn_Click(object sender, RoutedEventArgs e)
        {
            Window moveEquipmentWindow = new EquipmentMoveWindow(factory);
            moveEquipmentWindow.Show();
        }
        #endregion


        #region Drugs
        private void DisplayDrugs(List<Drug> drugs)
        {
            DrugView.Items.Clear();
            int index = 0;
            foreach (Drug drug in drugs)
            {
                DrugView.Items.Add(drug.Name);
                listedDrugs[index] = drug;
                index++;
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
        #endregion


        #region Ingredients
        private void DisplayIngredients(List<Ingredient> ingredients)
        {
            IngredientsView.Items.Clear();
            int index = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                IngredientsView.Items.Add(ingredient.Name);
                listedIngredients[index] = ingredient;
                index++;
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
                            
        private void NewIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
      
    }
}
