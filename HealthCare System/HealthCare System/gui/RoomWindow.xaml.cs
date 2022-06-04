using System;
using System.Collections.Generic;
using System.Windows;
using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using HealthCare_System.Database;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.EquipmentServices;

namespace HealthCare_System.gui
{
    public partial class RoomWindow : Window
    {
        bool createNewRoom;
        Room room;
        HealthCareDatabase database;

        RoomService roomService;
        EquipmentService equipmentService;

        public RoomWindow(bool createNewRoom, HealthCareDatabase database, Room room = null)
        {
            InitializeComponent();
            this.createNewRoom = createNewRoom;
            this.room = room;
            this.database = database;

            InitializeServices();
            InitializeTitle();
            InitializeFields();
        }

        void InitializeServices()
        {
            roomService = new RoomService(null, null, null, null, null, database.RoomRepo);
            equipmentService = new EquipmentService(database.EquipmentRepo, roomService);
        }

        void InitializeTitle()
        {
            if (createNewRoom)
                titleLbl.Content = "Create new room";
            else
                titleLbl.Content = "Update room";
        }

        void InitializeFields()
        {
            if (!createNewRoom)
                nameTb.Text = room.Name;
            else
                nameTb.Text = "";
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                if (roomType != TypeOfRoom.STORAGE)
                    typeCb.Items.Add(roomType);
            }
            if (!createNewRoom)
                typeCb.SelectedItem = room.Type;
            else
                typeCb.SelectedIndex = 0;

        }

        void TryCreation()
        {
            Dictionary<Equipment, int> equipmentAmount = equipmentService.InitalizeEquipment();
            try
            {
                RoomDTO roomDTO = new RoomDTO(database.RoomRepo.GenerateId(), nameTb.Text, 
                    (TypeOfRoom)typeCb.SelectedItem, equipmentAmount);
                roomService.Create(roomDTO);
                MessageBox.Show("Room created sucessfully!");
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
                RoomDTO roomDTO = new RoomDTO(-1, nameTb.Text,
                    (TypeOfRoom)typeCb.SelectedItem, new());
                roomService.Update(room, roomDTO);
                MessageBox.Show("Room updated sucessfully!");
                Close();
            }
            catch
            {
                MessageBox.Show("Name must be between 5 and 30 characters!");
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (createNewRoom)
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
