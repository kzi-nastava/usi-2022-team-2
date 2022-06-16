using System;
using System.Collections.Generic;
using System.Windows;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Equipments;
using HealthCare_System.GUI.Controller.Rooms;

namespace HealthCare_System.GUI.ManagerView
{
    public partial class RoomWindow : Window
    {
        bool createNewRoom;
        Room room;
        ServiceBuilder serviceBuilder;

        RoomController roomController;
        EquipmentController equipmentController;

        public RoomWindow(bool createNewRoom, ServiceBuilder serviceBuilder, Room room = null)
        {
            InitializeComponent();
            this.createNewRoom = createNewRoom;
            this.room = room;
            this.serviceBuilder = serviceBuilder;

            InitializeControllers();
            InitializeTitle();
            InitializeFields();
        }
        void InitializeControllers()
        {
            roomController = new(serviceBuilder.RoomService);
            equipmentController = new(serviceBuilder.EquipmentService);
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
            Dictionary<Equipment, int> equipmentAmount = equipmentController.InitalizeEquipment();
            try
            {
                RoomDto roomDto = new RoomDto(roomController.GenerateId(), nameTb.Text, 
                    (TypeOfRoom)typeCb.SelectedItem, equipmentAmount);
                roomController.Create(roomDto);
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
                RoomDto roomDto = new RoomDto(-1, nameTb.Text,
                    (TypeOfRoom)typeCb.SelectedItem, new());
                roomController.Update(room, roomDto);
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
