using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Renovations;
using HealthCare_System.GUI.Controller.Rooms;

namespace HealthCare_System.GUI.ManagerView
{
    
    public partial class RenovationWindow : Window
    {
        ServiceBuilder serviceBuilder;
        Dictionary<int, Room> listedRoomsSimple = new Dictionary<int, Room>();
        Dictionary<int, Room> listedRoomsSplitting = new Dictionary<int, Room>();
        Dictionary<int, Room> listedFirstRoomsMerging = new Dictionary<int, Room>();
        Dictionary<int, Room> listedSecondRoomsMerging= new Dictionary<int, Room>();

        RoomController roomController;
        SimpleRenovationController simpleRenovationController;
        MergingRenovationController mergingRenovationController;
        SplittingRenovationController splittingRenovationController;

        public RenovationWindow(ServiceBuilder serviceBuilder)
        {
            this.serviceBuilder = serviceBuilder;

            InitializeControllers();
            InitializeComponent();
            InitializeComboBoxes(roomController.Rooms());
            InitializeDatePickers();
        }

        void InitializeControllers()
        {
            roomController = new(serviceBuilder.RoomService);
            simpleRenovationController = new(serviceBuilder.SimpleRenovationService);
            mergingRenovationController = new(serviceBuilder.MergingRenovationService);
            splittingRenovationController = new(serviceBuilder.SplittingRenovationService);
        }

        void InitializeDatePickers()
        {
            simpleStartDp.SelectedDate = DateTime.Today;
            simpleEndDp.SelectedDate = DateTime.Today;
            mergingStartDp.SelectedDate = DateTime.Today;
            mergingEndDp.SelectedDate = DateTime.Today;
            splittingStartDp.SelectedDate = DateTime.Today;
            splittingEndDp.SelectedDate = DateTime.Today;
        }

        public void InitializeComboBoxes(List<Room> rooms)
        {
            InitializeRoomsSimpleCb(rooms);
            InitializeRoomsSplittingCb(rooms);
            InitializeFirstRoomsMergingCb(rooms);
            InitializeTypeComboBoxes();
        }

        public void DateValidation(DateTime start, DateTime end)
        {
            if (start <= DateTime.Now)
            {
                throw new Exception("Renovation cannot start in the past!");
            }

            if (end <= start)
            {
                throw new Exception("Renovaton cannot end befor it begins!");
            }
        }

        public void NameValidation(string name)
        {
            if (name.Length < 5 || name.Length > 30)
            {
                throw new Exception("Invalid name. Length of name must be between 5 and 30 characters");
            }
        }

        public void InitializeTypeComboBoxes()
        {
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                newRoomTypeSimpleCb.Items.Add(roomType);
                newRoomTypeMergingCb.Items.Add(roomType);
                firstNewRoomTypeSplittingCb.Items.Add(roomType);
                secondNewRoomTypeSplittingCb.Items.Add(roomType);
            }
            newRoomTypeSimpleCb.SelectedIndex = 0;
            newRoomTypeMergingCb.SelectedIndex = 0;
            firstNewRoomTypeSplittingCb.SelectedIndex = 0;
            secondNewRoomTypeSplittingCb.SelectedIndex = 0;
        }

        #region SimpleRenovations
        public void InitializeRoomsSimpleCb(List<Room> rooms)
        {
            roomsForSimpleCb.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                if (roomController.IsRoomAvailableForChange(room) &&
                    roomController.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
                {
                    roomsForSimpleCb.Items.Add(room.Id);
                    listedRoomsSimple[index] = room;
                    index++;
                }
            }
            roomsForSimpleCb.SelectedIndex = 0;
        }

        public void DisplayRoomSimpleData()
        {
            roomNameForSimpleTb.Text = listedRoomsSimple[roomsForSimpleCb.SelectedIndex].Name;
            roomTypeForSimpleTb.Text = listedRoomsSimple[roomsForSimpleCb.SelectedIndex].Type.ToString();
        }

        private void roomsForSimpleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomsForSimpleCb.SelectedIndex != -1)
            {
                DisplayRoomSimpleData();
            }
        }

        private void submitSimpleBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime beginningDate = simpleStartDp.SelectedDate.Value;
            DateTime endingDate = simpleEndDp.SelectedDate.Value;
            string newRoomName = newRoomNameSimpleTb.Text;
            try
            {
                DateValidation(beginningDate, endingDate);
                NameValidation(newRoomName);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
                return;
            }
            SimpleRenovationDto simpleRenovationDto = new SimpleRenovationDto(simpleRenovationController.GenerateId(),
                beginningDate, endingDate, RenovationStatus.BOOKED,
                listedRoomsSimple[roomsForSimpleCb.SelectedIndex], newRoomName,
                (TypeOfRoom)newRoomTypeSimpleCb.SelectedItem);
            simpleRenovationController.BookRenovation(simpleRenovationDto);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(roomController.Rooms());
        }
        #endregion

        #region MergingRenovations
        public void InitializeFirstRoomsMergingCb(List<Room> rooms)
        {
            firstRoomsForMergingCb.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                if (roomController.IsRoomAvailableForChange(room) &&
                    roomController.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
                {
                    firstRoomsForMergingCb.Items.Add(room.Id);
                    listedFirstRoomsMerging[index] = room;
                    index++;
                }
            }
            firstRoomsForMergingCb.SelectedIndex = 0;
        }

        public void InitializeSecondRoomsMergingCb()
        {
            secondRoomsForMergingCb.Items.Clear();
            int index = 0;
            foreach (KeyValuePair<int, Room> roomEntry in listedFirstRoomsMerging)
            {
                if (roomEntry.Value != listedFirstRoomsMerging[firstRoomsForMergingCb.SelectedIndex])
                {
                    secondRoomsForMergingCb.Items.Add(roomEntry.Value.Id);
                    listedSecondRoomsMerging[index] = roomEntry.Value;
                    index++;
                }
            }
            secondRoomsForMergingCb.SelectedIndex = 0;
        }

        public void DisplayFirstRoomMergingData()
        {
            firstRoomNameForMergingTb.Text = listedFirstRoomsMerging[firstRoomsForMergingCb.SelectedIndex].Name;
            firstRoomTypeForMergingTb.Text = listedFirstRoomsMerging[firstRoomsForMergingCb.SelectedIndex].Type.ToString();
        }

        public void DisplaySecondRoomMergingData()
        {
            secondRoomNameForMergingTb.Text = listedSecondRoomsMerging[secondRoomsForMergingCb.SelectedIndex].Name;
            secondRoomTypeForMergingTb.Text = listedSecondRoomsMerging[secondRoomsForMergingCb.SelectedIndex].Type.ToString();
        }

        private void firstRoomsForMergingCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstRoomsForMergingCb.SelectedIndex != -1)
            {
                DisplayFirstRoomMergingData();
                InitializeSecondRoomsMergingCb();
            }
        }

        private void secondRoomsForMergingCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (secondRoomsForMergingCb.SelectedIndex != -1)
            {
                DisplaySecondRoomMergingData();
            }
        }

        private void submitMergingBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime beginningDate = mergingStartDp.SelectedDate.Value;
            DateTime endingDate = mergingEndDp.SelectedDate.Value;
            string newRoomName = newRoomNameMergingTb.Text;
            try
            {
                DateValidation(beginningDate, endingDate);
                NameValidation(newRoomName);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
                return;
            }
            List<Room> rooms = new List<Room> { listedFirstRoomsMerging[firstRoomsForMergingCb.SelectedIndex],
                listedSecondRoomsMerging[secondRoomsForMergingCb.SelectedIndex] };
            MergingRenovationDto mergingRenovationDto = new MergingRenovationDto(mergingRenovationController.GenerateId(),
                beginningDate, endingDate, rooms, RenovationStatus.BOOKED, newRoomName,
                (TypeOfRoom)newRoomTypeMergingCb.SelectedItem);
            mergingRenovationController.BookRenovation(mergingRenovationDto);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(roomController.Rooms());
        }
        #endregion

        #region SplittingRenovations
        public void InitializeRoomsSplittingCb(List<Room> rooms)
        {
            roomsForSplittingCb.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                if (roomController.IsRoomAvailableForChange(room) &&
                    roomController.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
                {
                    roomsForSplittingCb.Items.Add(room.Id);
                    listedRoomsSplitting[index] = room;
                    index++;
                }
            }
            roomsForSplittingCb.SelectedIndex = 0;
        }

        public void DisplayRoomSplittingData()
        {
            roomNameForSplittingTb.Text = listedRoomsSplitting[roomsForSplittingCb.SelectedIndex].Name;
            roomTypeForSplittingTb.Text = listedRoomsSplitting[roomsForSplittingCb.SelectedIndex].Type.ToString();
        }

        private void roomsForSplittingCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomsForSplittingCb.SelectedIndex != -1)
            {
                DisplayRoomSplittingData();
            }
        }

        private void submitSplittingBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime beginningDate = splittingStartDp.SelectedDate.Value;
            DateTime endingDate = splittingEndDp.SelectedDate.Value;
            string firstNewRoomName = firstNewRoomNameSplittingTb.Text;
            string secondNewRoomName = secondNewRoomNameSplittingTb.Text;
            try
            {
                DateValidation(beginningDate, endingDate);
                NameValidation(firstNewRoomName);
                NameValidation(secondNewRoomName);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
                return;
            }
            SplittingRenovationDto splittingRenovationDto = new SplittingRenovationDto(splittingRenovationController.GenerateId(),
                beginningDate, endingDate, RenovationStatus.BOOKED,
                listedRoomsSplitting[roomsForSplittingCb.SelectedIndex], firstNewRoomName,
                (TypeOfRoom)firstNewRoomTypeSplittingCb.SelectedItem, secondNewRoomName,
                (TypeOfRoom)secondNewRoomTypeSplittingCb.SelectedItem);
            splittingRenovationController.BookRenovation(splittingRenovationDto);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(roomController.Rooms());
        }

        #endregion

        
    }
}
