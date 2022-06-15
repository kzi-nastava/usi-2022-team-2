using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using HealthCare_System.Database;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.GUI.Main;
using HealthCare_System.GUI.Controller.Ingredients;
using HealthCare_System.GUI.Controller.Drugs;
using HealthCare_System.GUI.Controller.Rooms;
using HealthCare_System.GUI.Controller.Equipments;
using HealthCare_System.GUI.Controller.Users;
using HealthCare_System.GUI.Controller.DoctorSurveys;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.GUI.Controller.HospitalServices;

namespace HealthCare_System.GUI.ManagerView
{
    public partial class ManagerWindow : Window
    {
        ServiceBuilder serviceBuilder;
        Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
        Dictionary<int, Room> listedRooms = new Dictionary<int, Room>();
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();
        Dictionary<int, Ingredient> listedIngredients = new Dictionary<int, Ingredient>();
        Dictionary<int, Doctor> listedDoctors = new Dictionary<int, Doctor>();

        EquipmentController equipmentController;
        RoomController roomController;
        IngredientController ingredientController;
        DrugController drugController;
        DoctorController doctorController;
        DoctorSurveyController doctorSurveyController;
        HospitalSurveyController hospitalSurveyController;

        public ManagerWindow(ServiceBuilder serviceBuilder)
        {
            InitializeComponent();
            this.serviceBuilder = serviceBuilder;

            InitializeControllers();
            InitializeComboBoxes();
            DisplayRooms(roomController.Rooms());
            DisplayDrugs(drugController.Drugs());
            DisplayIngredients(ingredientController.Ingredients());
            DisplayEquipment(equipmentController.GetEquipmentFromAllRooms());
            InitializeDoctors();
            DisplayHospitalRatings();

            
        }

        void InitializeControllers()
        {
            equipmentController = new(serviceBuilder.EquipmentService);
            roomController = new(serviceBuilder.RoomService);
            ingredientController = new(serviceBuilder.IngredientService);
            drugController = new(serviceBuilder.DrugService);
            doctorController = new(serviceBuilder.DoctorService);
            doctorSurveyController = new(serviceBuilder.DoctorSurveyService);
            hospitalSurveyController = new(serviceBuilder.HospitalSurveyService);
        }

        #region EquipmentFiltering
    
        private void ApplyEveryEquipmentFilter()
        {
            
            if (roomTypeFilter.SelectedIndex != -1 && amountFilter.SelectedIndex != -1 && 
                equipementTypeFilter.SelectedIndex != -1)
            {
                equipmentAmount = equipmentController.GetEquipmentFromAllRooms();
                string roomType = roomTypeFilter.SelectedItem.ToString();
                string amount = amountFilter.SelectedItem.ToString();
                string equipmentType = equipementTypeFilter.SelectedItem.ToString();
                equipmentController.ApplyEquipmentFilters(roomType, amount, equipmentType, equipmentAmount);
                DisplayEquipment(equipmentAmount);
            }
        }

        private void ExecuteEquipmentQuery(string value)
        {
            if (value.Length != 0)
            {
                equipmentController.EquipmentQuery(value, equipmentAmount);
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
            Window newRoomWindow = new RoomWindow(true, serviceBuilder);
            newRoomWindow.Show();
        }

        void ValidateForDelete()
        {
            if (listedRooms[roomView.SelectedIndex].Type == TypeOfRoom.STORAGE)
            {
                throw new Exception("Cannot delete storage!");
            }

            if (!roomController.IsRoomAvailableRenovationsAtAll(listedRooms[roomView.SelectedIndex]))
            {
                throw new Exception("Room is in process of renovation so it is not able to be deleted!");
            }

            if (!roomController.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
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

            if (!roomController.IsRoomAvailableRenovationsAtTime(listedRooms[roomView.SelectedIndex], DateTime.Now))
            {
                throw new Exception("Room is in process of renovation so it is not able to be updated!");
            }

            if (!roomController.IsRoomAvailableForChange(listedRooms[roomView.SelectedIndex]))
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
                    roomController.RemoveRoom(listedRooms[roomView.SelectedIndex]);
                    MessageBox.Show("Room deleted sucessfully!");
                    DisplayRooms(roomController.Rooms());
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
            Window renovationWindow = new RenovationWindow(serviceBuilder);
            renovationWindow.Show();
        }

        private void updateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateForUpdate();
                Window updateRoomWindow = new RoomWindow(false, serviceBuilder, listedRooms[roomView.SelectedIndex]);
                updateRoomWindow.Show();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
            
        }

        private void moveEquipementBtn_Click(object sender, RoutedEventArgs e)
        {
            Window moveEquipmentWindow = new EquipmentMoveWindow(serviceBuilder);
            moveEquipmentWindow.Show();
        }

        private void refreshRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayRooms(roomController.Rooms());
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
            if (!drugController.IsDrugAvailableForChange(listedDrugs[drugView.SelectedIndex]))
            {
                throw new Exception("Drug exists in a prescription so it is not available for change");
            }
        }

        private void newDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            Window drugWindow = new DrugWindow(true, serviceBuilder);
            drugWindow.Show();
        }

        private void rejectedDrugsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window rejectedDrugsWindow = new RejectedDrugsWindow(serviceBuilder);
            rejectedDrugsWindow.Show();
        }

        private void updateDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateDrugForChange();
                Window drugWindow = new DrugWindow(false,  serviceBuilder, listedDrugs[drugView.SelectedIndex]);
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
                    drugController.Delete(listedDrugs[drugView.SelectedIndex]);
                    MessageBox.Show("Drug deleted sucessfully!");
                    DisplayDrugs(drugController.Drugs());
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
            DisplayDrugs(drugController.Drugs());
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
            if (!ingredientController.IsIngredientAvailableForChange(listedIngredients[ingredientsView.SelectedIndex]))
            {
                throw new Exception("Ingredient exists in a drug so it is not available for change");
            }
        }
                            
        private void newIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            Window ingredientWindow = new IngredientWindow(true, serviceBuilder);
            ingredientWindow.Show();
        }

        private void updateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateIngredientForChange();
                Window ingredientWindow = new IngredientWindow(false, serviceBuilder, 
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
                    ingredientController.Delete(listedIngredients[ingredientsView.SelectedIndex]);
                    MessageBox.Show("Ingredient deleted sucessfully!");
                    DisplayIngredients(ingredientController.Ingredients());
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
            DisplayIngredients(ingredientController.Ingredients());
        }
        #endregion


        #region DoctorSurveys

        void InitializeDoctors()
        {
            int index = 0;
            foreach (Doctor doctor in doctorController.Doctors())
            {
                doctorIdCb.Items.Add(doctor.Jmbg);
                listedDoctors[index] = doctor;
                index++;
            }
            doctorIdCb.SelectedIndex = 0;
            DisplayBestDoctors(1);
            DisplayWorstDoctors(1);
        }

        private void doctorIdCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorIdCb.SelectedIndex != -1)
            {
                doctorNameLbl.Content = listedDoctors[doctorIdCb.SelectedIndex].FirstName;
                doctorLastNameLbl.Content = listedDoctors[doctorIdCb.SelectedIndex].LastName;
                DisplayDoctorRatings();
                DisplayDoctorComments();
            }
            
        }

        void DisplayDoctorRatings()
        {
            Dictionary<string, decimal> averageRatings = doctorSurveyController.FindAverageRatingForDoctorByCategory(
                listedDoctors[doctorIdCb.SelectedIndex]);
            Dictionary<int, int> serviceQualityRatings = doctorSurveyController.FindRatingAppearancesForServiceQuality(
                listedDoctors[doctorIdCb.SelectedIndex]);
            Dictionary<int, int> recommendationRatings = doctorSurveyController.FindRatingAppearancesForRecommendation(
                listedDoctors[doctorIdCb.SelectedIndex]);
            DisplayDoctorServiceQuality(serviceQualityRatings, averageRatings["Service Quality"]);
            DisplayDoctorRecommendation(recommendationRatings, averageRatings["Recommendation"]);
        }

        void DisplayDoctorServiceQuality(Dictionary<int, int> serviceQualityRatings, decimal averageRating)
        {
            doctorServiceQualityLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in serviceQualityRatings)
            {
                doctorServiceQualityLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            doctorServiceQualityLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayDoctorRecommendation(Dictionary<int, int> recommendationRatings, decimal averageRating)
        {
            doctorRecommendationLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in recommendationRatings)
            {
                doctorRecommendationLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            doctorRecommendationLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayDoctorComments()
        {
            commentsLb.Items.Clear();
            List<string> comments = doctorSurveyController.GetAllComments(listedDoctors[doctorIdCb.SelectedIndex]);
            foreach (string comment in comments)
            {
                commentsLb.Items.Add(comment);
            }
        }

        void DisplayBestDoctors(int numberOfDoctors)
        {
            List<Doctor> sortedDoctors = doctorSurveyController.SortDoctorsByRatings(doctorController.Doctors(), SortDirection.DESCENDING);
            for (int i = 0; i < numberOfDoctors; i++)
            {
                bestDoctorsLb.Items.Add(sortedDoctors[i].Jmbg + " - " + sortedDoctors[i].FirstName + " " + sortedDoctors[i].LastName);
            }
        }

        void DisplayWorstDoctors(int numberOfDoctors)
        {
            List<Doctor> sortedDoctors = doctorSurveyController.SortDoctorsByRatings(doctorController.Doctors(), SortDirection.ASCENDING);
            for (int i = 0; i < numberOfDoctors; i++)
            {
                worstDoctorsLb.Items.Add(sortedDoctors[i].Jmbg + " - " + sortedDoctors[i].FirstName + " " + sortedDoctors[i].LastName);
            }
        }

        #endregion


        #region HospitalSurveys

        void DisplayHospitalRatings()
        {
            Dictionary<string, decimal> averageRatings = hospitalSurveyController.FindAverageRatingByCategory();
            Dictionary<int, int> serviceQualityRatings = hospitalSurveyController.FindRatingAppearancesForServiceQuality();
            Dictionary<int, int> hygieneRatings = hospitalSurveyController.FindRatingAppearancesForHygiene();
            Dictionary<int, int> satisfactionRatings = hospitalSurveyController.FindRatingAppearancesForSatisfaction();
            Dictionary<int, int> recommendationRatings = hospitalSurveyController.FindRatingAppearancesForRecommendation();

            DisplayHospitalServiceQuality(serviceQualityRatings, averageRatings["serviceQuality"]);
            DisplayHospitalHygiene(hygieneRatings, averageRatings["hygiene"]);
            DisplayHospitalSatisfaction(satisfactionRatings, averageRatings["satisfaction"]);
            DisplayHospitalRecommendation(recommendationRatings, averageRatings["recommendation"]);

            DisplayHospitalComments();
        }

        void DisplayHospitalServiceQuality(Dictionary<int, int> serviceQualityRatings, decimal averageRating)
        {
            hospitalServiceQualityLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in serviceQualityRatings)
            {
                hospitalServiceQualityLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            hospitalServiceQualityLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayHospitalHygiene(Dictionary<int, int> hygieneRatings, decimal averageRating)
        {
            hospitalHyigieneLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in hygieneRatings)
            {
                hospitalHyigieneLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            hospitalHyigieneLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayHospitalSatisfaction(Dictionary<int, int> satisfactionRatings, decimal averageRating)
        {
            hospitalSatisfactionLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in satisfactionRatings)
            {
                hospitalSatisfactionLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            hospitalSatisfactionLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayHospitalRecommendation(Dictionary<int, int> recommendationRatings, decimal averageRating)
        {
            hospitalRecommendationLb.Items.Clear();
            foreach (KeyValuePair<int, int> rating in recommendationRatings)
            {
                hospitalRecommendationLb.Items.Add(rating.Key.ToString() + "'s : " + rating.Value.ToString());
            }
            hospitalRecommendationLb.Items.Add("Average : " + averageRating.ToString());
        }

        void DisplayHospitalComments()
        {
            List<string> comments = hospitalSurveyController.GetAllComments();
            foreach (string comment in comments)
            {
                hospitalCommentsLb.Items.Add(comment);
            }
        }

        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(serviceBuilder);
                main.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }

        
    }
}
