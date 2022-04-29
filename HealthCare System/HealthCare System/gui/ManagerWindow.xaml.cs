using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.factory;
using HealthCare_System.entities;
using System.ComponentModel;

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
            if (roomTypeFilter.SelectedIndex != -1 && amountFilter.SelectedIndex != -1 && equipementTypeFilter.SelectedIndex != -1)
            {
                equipmentAmount = factory.RoomController.GetEquipmentFromAllRooms();
                string roomType = roomTypeFilter.SelectedItem.ToString();
                string amount = amountFilter.SelectedItem.ToString();
                string equipmentType = equipementTypeFilter.SelectedItem.ToString();
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
            equipementView.Items.Clear();
            if (equipmentAmount.Count == 0)
            {
                equipementView.Items.Add("No data");
            }
            else
            {
                foreach (KeyValuePair<Equipment, int> entry in equipmentAmount)
                {
                    equipementView.Items.Add(entry.Key.ToString() + ",  Amount: " + entry.Value);
                }
            }            
        }

        private void roomTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(searchTb.Text);
        }

        private void amountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(searchTb.Text);
        }

        private void equipementTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(searchTb.Text);
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyEveryEquipmentFilter();
            ExecuteEquipmentQuery(searchTb.Text);
        }

        private void InitializeComboBoxes()
        {
            roomTypeFilter.Items.Add("All");
            roomTypeFilter.SelectedIndex = 0;
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                roomTypeFilter.Items.Add(roomType);
            }

            amountFilter.Items.Add("All");
            amountFilter.SelectedIndex = 0;
            amountFilter.Items.Add("Nema na stanju");
            amountFilter.Items.Add("0-10");
            amountFilter.Items.Add("10+");

            equipementTypeFilter.Items.Add("All");
            equipementTypeFilter.SelectedIndex = 0;
            foreach (TypeOfEquipment equipmentType in Enum.GetValues(typeof(TypeOfEquipment)))
            {
                equipementTypeFilter.Items.Add(equipmentType);
            }



        }
        #endregion


        #region Rooms
        private void DisplayRooms(List<Room> rooms)
        {
            roomView.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                roomView.Items.Add("Id: "+ room.Id + ", Name: " + room.Name + ", Type: " + room.Type);
                listedRooms[index] = room;
                index++;
            }
            roomView.SelectedIndex = 0;
        }

        private void newRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Window newRoomWindow = new RoomWindow(true, factory);
            newRoomWindow.Show();
        }

        private void deleteRoomBtn_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Delete Room?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (listedRooms[roomView.SelectedIndex].Type == TypeOfRoom.STORAGE)
                {
                    MessageBox.Show("Cannot delete storage!");
                    return;
                }

                if (factory.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
                {
                    factory.RemoveRoom(listedRooms[roomView.SelectedIndex]);
                    MessageBox.Show("Room deleted sucessfully!");
                    DisplayRooms(this.factory.RoomController.Rooms);
                }
                else
                {
                    MessageBox.Show("Room is already taken by an appointmet or transfer so it is not able to be deleted!");
                }
            }
            else
            {
                return;
            }
        }

        private void renovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listedRooms[roomView.SelectedIndex].Type == TypeOfRoom.STORAGE)
            {
                MessageBox.Show("Cannot update storage!");
                return;
            }

            if (factory.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
            {
                Window updateRoomWindow = new RoomWindow(false, factory, listedRooms[roomView.SelectedIndex]);
                updateRoomWindow.Show();
            }
            else
            {
                MessageBox.Show("Room is already taken by an appointmet or transfer so it is not able to be updated!");
            }
            
        }

        private void moveEquipementBtn_Click(object sender, RoutedEventArgs e)
        {
            Window moveEquipmentWindow = new EquipmentMoveWindow(factory);
            moveEquipmentWindow.Show();
        }

        private void refreshRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayRooms(this.factory.RoomController.Rooms);
        }
        #endregion


        #region Drugs
        private void DisplayDrugs(List<Drug> drugs)
        {
            drugView.Items.Clear();
            int index = 0;
            foreach (Drug drug in drugs)
            {
                drugView.Items.Add(drug.Name);
                listedDrugs[index] = drug;
                index++;
            }
        }

        private void newDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rejectedDrugsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region Ingredients
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
        }

        
                            
        private void newIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }




        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            factory.User = null;
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(factory);
                main.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
