using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Model;
using System.ComponentModel;
using HealthCare_System.Database;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.EquipmentServices;
using HealthCare_System.Services.RenovationServices;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Services.DrugServices;
using HealthCare_System.Services.IngredientServices;
using HealthCare_System.Services.PrescriptionServices;

namespace HealthCare_System.gui
{
    public partial class ManagerWindow : Window
    {
        HealthCareDatabase database;
        Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
        Dictionary<int, Room> listedRooms = new Dictionary<int, Room>();
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();
        Dictionary<int, Ingredient> listedIngredients = new Dictionary<int, Ingredient>();

        RoomService roomService;
        EquipmentService equipmentService;
        MergingRenovationService mergingRenovationService;
        SimpleRenovationService simpleRenovationService;
        SplittingRenovationService splittingRenovationService;
        AppointmentService appointmentService;
        EquipmentTransferService equipmentTransferService;

        IngredientService ingredientService;
        DrugService drugService;
        PrescriptionService prescriptionService;


        public ManagerWindow(HealthCareDatabase database)
        {
            InitializeComponent();
            this.database  =  database;

            InitializeServices();
            InitializeComboBoxes();
            DisplayRooms(database.RoomRepo.Rooms);
            DisplayDrugs(database.DrugRepo.Drugs);
            DisplayIngredients(database.IngredientRepo.Ingredients);
            DisplayEquipment(equipmentService.GetEquipmentFromAllRooms());

            
        }

        void InitializeServices()
        {
            roomService = new RoomService(null, null, null, null, null, database.RoomRepo);
            equipmentService = new EquipmentService(database.EquipmentRepo, roomService);
            mergingRenovationService = new MergingRenovationService(database.MergingRenovationRepo, roomService,
                null, equipmentService);
            simpleRenovationService = new SimpleRenovationService(database.SimpleRenovationRepo, roomService,
                null, equipmentService);
            splittingRenovationService = new SplittingRenovationService(database.SplittingRenovationRepo, roomService,
                null, equipmentService);
            appointmentService = new AppointmentService(database.AppointmentRepo, null);
            equipmentTransferService = new EquipmentTransferService(database.EquipmentTransferRepo, roomService);
            roomService.MergingRenovationService = mergingRenovationService;
            roomService.SplittingRenovationService = splittingRenovationService;
            roomService.SimpleRenovationService = simpleRenovationService;
            roomService.AppointmentService = appointmentService;
            roomService.EquipmentTransferService = equipmentTransferService;

            prescriptionService = new PrescriptionService(database.PrescriptionRepo, null);
            drugService = new DrugService(database.DrugRepo, prescriptionService);
            ingredientService = new IngredientService(database.IngredientRepo, drugService);
        }

        #region EquipmentFiltering
    
        private void ApplyEveryEquipmentFilter()
        {
            
            if (roomTypeFilter.SelectedIndex != -1 && amountFilter.SelectedIndex != -1 && 
                equipementTypeFilter.SelectedIndex != -1)
            {
                equipmentAmount = equipmentService.GetEquipmentFromAllRooms();
                string roomType = roomTypeFilter.SelectedItem.ToString();
                string amount = amountFilter.SelectedItem.ToString();
                string equipmentType = equipementTypeFilter.SelectedItem.ToString();
                equipmentService.ApplyEquipmentFilters(roomType, amount, equipmentType, equipmentAmount);
                DisplayEquipment(equipmentAmount);
            }
        }

        private void ExecuteEquipmentQuery(string value)
        {
            if (value.Length != 0)
            {
                equipmentService.EquipmentQuery(value, equipmentAmount);
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

        void InitializeRoomTypeFilterCb()
        {
            roomTypeFilter.Items.Add("All");
            roomTypeFilter.SelectedIndex = 0;
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                roomTypeFilter.Items.Add(roomType);
            }
        }

        void InitializeAmountFilterCb()
        {
            amountFilter.Items.Add("All");
            amountFilter.SelectedIndex = 0;
            amountFilter.Items.Add("Nema na stanju");
            amountFilter.Items.Add("0-10");
            amountFilter.Items.Add("10+");
        }

        void InitializeEquipmentTypeFilterCb()
        {
            equipementTypeFilter.Items.Add("All");
            equipementTypeFilter.SelectedIndex = 0;
            foreach (TypeOfEquipment equipmentType in Enum.GetValues(typeof(TypeOfEquipment)))
            {
                equipementTypeFilter.Items.Add(equipmentType);
            }
        }

        private void InitializeComboBoxes()
        {
            InitializeRoomTypeFilterCb();
            InitializeAmountFilterCb();
            InitializeEquipmentTypeFilterCb();
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
            Window newRoomWindow = new RoomWindow(true, database);
            newRoomWindow.Show();
        }

        void ValidateForDelete()
        {
            if (listedRooms[roomView.SelectedIndex].Type == TypeOfRoom.STORAGE)
            {
                throw new Exception("Cannot delete storage!");
            }

            if (!roomService.IsRoomAvailableRenovationsAtAll(listedRooms[roomView.SelectedIndex]))
            {
                throw new Exception("Room is in process of renovation so it is not able to be deleted!");
            }

            if (!roomService.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
            {
                throw new Exception("Room is already taken by an appointmet or transfer so it is not able to be deleted!");
            }
        }

        void ValidateForUpdate()
        {
            if (listedRooms[roomView.SelectedIndex].Type == TypeOfRoom.STORAGE)
            {
                throw new Exception("Cannot update storage!");
            }

            if (!roomService.IsRoomAvailableRenovationsAtTime(listedRooms[roomView.SelectedIndex], DateTime.Now))
            {
                throw new Exception("Room is in process of renovation so it is not able to be updated!");
            }

            if (!roomService.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
            {
                throw new Exception("Room is already taken by an appointmet or transfer so it is not able to be updated!");
            }
        }

        private void deleteRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Room?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ValidateForDelete();
                    roomService.RemoveRoom(listedRooms[roomView.SelectedIndex]);
                    MessageBox.Show("Room deleted sucessfully!");
                    DisplayRooms(database.RoomRepo.Rooms);
                }
                catch (Exception excp)
                {
                    MessageBox.Show(excp.Message);
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void renovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Window renovationWindow = new RenovationWindow(database);
            renovationWindow.Show();
        }

        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateForUpdate();
                Window updateRoomWindow = new RoomWindow(false, database, listedRooms[roomView.SelectedIndex]);
                updateRoomWindow.Show();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
            
        }

        private void moveEquipementBtn_Click(object sender, RoutedEventArgs e)
        {
            Window moveEquipmentWindow = new EquipmentMoveWindow(database);
            moveEquipmentWindow.Show();
        }

        private void refreshRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayRooms(database.RoomRepo.Rooms);
        }
        #endregion


        #region Drugs
        private void DisplayDrugs(List<Drug> drugs)
        {
            drugView.Items.Clear();
            int index = 0;
            foreach (Drug drug in drugs)
            {
                if (drug.Status == DrugStatus.ACCEPTED) {
                    drugView.Items.Add(drug.Name);
                    listedDrugs[index] = drug;
                    index++;
                }
            }
            drugView.SelectedIndex = 0;
        }

        private void ValidateDrugForChange()
        {
            if (!drugService.IsDrugAvailableForChange(listedDrugs[drugView.SelectedIndex]))
            {
                throw new Exception("Drug exists in a prescription so it is not available for change");
            }
        }

        private void newDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            Window drugWindow = new DrugWindow(true, database);
            drugWindow.Show();
        }

        private void rejectedDrugsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window rejectedDrugsWindow = new RejectedDrugsWindow(database);
            rejectedDrugsWindow.Show();
        }

        private void updateDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateDrugForChange();
                Window drugWindow = new DrugWindow(false,  database, listedDrugs[drugView.SelectedIndex]);
                drugWindow.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void deleteDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Drug?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ValidateDrugForChange();
                    drugService.Delete(listedDrugs[drugView.SelectedIndex]);
                    MessageBox.Show("Drug deleted sucessfully!");
                    DisplayDrugs(database.DrugRepo.Drugs);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void refreshDrugsBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayDrugs(database.DrugRepo.Drugs);
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
            ingredientsView.SelectedIndex = 0;
        }

        private void ValidateIngredientForChange()
        {
            if (!ingredientService.IsIngredientAvailableForChange(listedIngredients[ingredientsView.SelectedIndex]))
            {
                throw new Exception("Ingredient exists in a drug so it is not available for change");
            }
        }
                            
        private void newIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            Window ingredientWindow = new IngredientWindow(true, database);
            ingredientWindow.Show();
        }

        private void updateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateIngredientForChange();
                Window ingredientWindow = new IngredientWindow(false, database, 
                    listedIngredients[ingredientsView.SelectedIndex]);
                ingredientWindow.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void deleteIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Ingredient?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ValidateIngredientForChange();
                    ingredientService.Delete(listedIngredients[ingredientsView.SelectedIndex]);
                    MessageBox.Show("Ingredient deleted sucessfully!");
                    DisplayIngredients(database.IngredientRepo.Ingredients);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void refreshIngredientsBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayIngredients(database.IngredientRepo.Ingredients);
        }
        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(database);
                main.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }

        
    }
}
