using System;
using System.Collections.Generic;
using System.Windows;
using HealthCare_System.entities;
using HealthCare_System.factory;

namespace HealthCare_System.gui
{
    public partial class RoomWindow : Window
    {
        bool createNewRoom;
        Room room;
        HealthCareFactory factory;

        public RoomWindow(bool createNewRoom, HealthCareFactory factory, Room room = null)
        {
            InitializeComponent();
            this.createNewRoom = createNewRoom;
            this.room = room;
            this.factory = factory;
            InitializeTitle();
            InitializeFields();
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

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (createNewRoom)
            {
                Dictionary<Equipment, int> equipmentAmount = factory.InitalizeEquipment();
                try
                {
                    factory.RoomController.CreateNewRoom(nameTb.Text, (TypeOfRoom)typeCb.SelectedItem, equipmentAmount);
                    MessageBox.Show("Room created sucessfully!");
                    Close();
                }
                catch
                {
                    MessageBox.Show("Name must be between 5 and 30 characters!");
                }
            }
            else
            {
                try
                {
                    factory.RoomController.UpdateRoom(room, nameTb.Text, (TypeOfRoom)typeCb.SelectedItem);
                    MessageBox.Show("Room updated sucessfully!");
                    Close();
                }
                catch
                {
                    MessageBox.Show("Name must be between 5 and 30 characters!");
                }
            }
        }
    }
}
