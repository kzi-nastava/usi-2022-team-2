using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using HealthCare_System.factory;
using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using HealthCare_System.Database;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.RenovationServices;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Services.EquipmentServices;

namespace HealthCare_System.gui
{
    
    public partial class RenovationWindow : Window
    {
        HealthCareDatabase database;
        Dictionary<int, Room> listedRoomsSimple = new Dictionary<int, Room>();
        Dictionary<int, Room> listedRoomsSplitting = new Dictionary<int, Room>();
        Dictionary<int, Room> listedFirstRoomsMerging = new Dictionary<int, Room>();
        Dictionary<int, Room> listedSecondRoomsMerging= new Dictionary<int, Room>();

        RoomService roomService;
        SimpleRenovationService simpleRenovationService;
        MergingRenovationService mergingRenovationService;
        SplittingRenovationService splittingRenovationService;
        AppointmentService appointmentService;
        EquipmentTransferService equipmentTransferService;

        public RenovationWindow(HealthCareDatabase database)
        {
            this.database  =  database;

            InitializeServices();
            InitializeComponent();
            InitializeComboBoxes(database.RoomRepo.Rooms);
            simpleStartDp.SelectedDate = DateTime.Today;
            simpleEndDp.SelectedDate = DateTime.Today;
            mergingStartDp.SelectedDate = DateTime.Today;
            mergingEndDp.SelectedDate = DateTime.Today;
            splittingStartDp.SelectedDate = DateTime.Today;
            splittingEndDp.SelectedDate = DateTime.Today;
        }

        void InitializeServices()
        {
            roomService = new RoomService(null, null, null, null, null, database.RoomRepo);
            mergingRenovationService = new MergingRenovationService(database.MergingRenovationRepo, roomService,
                null, null);
            simpleRenovationService = new SimpleRenovationService(database.SimpleRenovationRepo, roomService,
                null, null);
            splittingRenovationService = new SplittingRenovationService(database.SplittingRenovationRepo, roomService,
                null, null);
            appointmentService = new AppointmentService(database.AppointmentRepo, null);
            equipmentTransferService = new EquipmentTransferService(database.EquipmentTransferRepo, roomService);
            roomService.MergingRenovationService = mergingRenovationService;
            roomService.SplittingRenovationService = splittingRenovationService;
            roomService.SimpleRenovationService = simpleRenovationService;
            roomService.AppointmentService = appointmentService;
            roomService.EquipmentTransferService = equipmentTransferService;
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
                if (roomService.IsRoomAvailableForChange(room) &&
                    roomService.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
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
            SimpleRenovationDTO simpleRenovationDTO = new SimpleRenovationDTO(database.SimpleRenovationRepo.GenerateId(),
                beginningDate, endingDate, RenovationStatus.BOOKED,
                listedRoomsSimple[roomsForSimpleCb.SelectedIndex], newRoomName,
                (TypeOfRoom)newRoomTypeSimpleCb.SelectedItem);
            simpleRenovationService.BookRenovation(simpleRenovationDTO);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(database.RoomRepo.Rooms);
        }
        #endregion

        #region MergingRenovations
        public void InitializeFirstRoomsMergingCb(List<Room> rooms)
        {
            firstRoomsForMergingCb.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                if (roomService.IsRoomAvailableForChange(room) &&
                    roomService.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
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
            MergingRenovationDTO mergingRenovationDTO = new MergingRenovationDTO(database.MergingRenovationRepo.GenerateId(),
                beginningDate, endingDate, rooms, RenovationStatus.BOOKED, newRoomName,
                (TypeOfRoom)newRoomTypeMergingCb.SelectedItem);
            mergingRenovationService.BookRenovation(mergingRenovationDTO);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(database.RoomRepo.Rooms);
        }
        #endregion

        #region SplittingRenovations
        public void InitializeRoomsSplittingCb(List<Room> rooms)
        {
            roomsForSplittingCb.Items.Clear();
            int index = 0;
            foreach (Room room in rooms)
            {
                if (roomService.IsRoomAvailableForChange(room) &&
                    roomService.IsRoomAvailableRenovationsAtAll(room) && room.Type != TypeOfRoom.STORAGE)
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
            SplittingRenovationDTO splittingRenovationDTO = new SplittingRenovationDTO(database.SplittingRenovationRepo.GenerateId(),
                beginningDate, endingDate, RenovationStatus.BOOKED,
                listedRoomsSplitting[roomsForSplittingCb.SelectedIndex], firstNewRoomName,
                (TypeOfRoom)firstNewRoomTypeSplittingCb.SelectedItem, secondNewRoomName,
                (TypeOfRoom)secondNewRoomTypeSplittingCb.SelectedItem);
            splittingRenovationService.BookRenovation(splittingRenovationDTO);
            MessageBox.Show("Renovation booked sucessfully!");
            InitializeComboBoxes(database.RoomRepo.Rooms);
        }

        #endregion

        
    }
}
