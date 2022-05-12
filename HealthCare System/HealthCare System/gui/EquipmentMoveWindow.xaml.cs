using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.factory;
using HealthCare_System.entities;

namespace HealthCare_System.gui
{
    public partial class EquipmentMoveWindow : Window
    {
        HealthCareFactory factory;
        Dictionary<int, Room> roomsFrom = new Dictionary<int, Room>();
        Dictionary<int, Room> roomsTo = new Dictionary<int, Room>();
        Dictionary<int, Equipment> equipment = new Dictionary<int, Equipment>();

        public EquipmentMoveWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            InitializeComboBox();
            InitializeDatePicker();
        }

        private void InitializeDatePicker()
        {
            datePick.SelectedDate = DateTime.Today;
        }

        void DisplayRoomsFrom()
        {
            moveFromView.Items.Clear();
            int index = 0;
            foreach (Room room in factory.RoomController.Rooms)
            {
                moveFromView.Items.Add("Id: " + room.Id + ", Name: " + room.Name + ", Amount: "+ room.EquipmentAmount[equipment[equipmentCb.SelectedIndex]].ToString());
                roomsFrom[index] = room;
                index++;
            }
            moveFromView.SelectedIndex = 0;
        }

        void DisplayRoomsTo()
        {
            moveToView.Items.Clear();
            int index = 0;
            foreach (Room room in factory.RoomController.Rooms)
            {
                if (room != roomsFrom[moveFromView.SelectedIndex]) 
                {
                    moveToView.Items.Add("Id: " + room.Id + ", Name: " + room.Name + ", Amount: " + room.EquipmentAmount[equipment[equipmentCb.SelectedIndex]].ToString());
                    roomsTo[index] = room;
                    index++;
                }   
            }
            moveToView.SelectedIndex = 0;
        }

        void InitializeComboBox()
        {
            int index = 0;
            foreach (Equipment equipment in factory.EquipmentController.Equipment) 
            {
                equipmentCb.Items.Add(equipment.Name);
                this.equipment[index] = equipment;
                index++;
            }
                
            equipmentCb.SelectedIndex = 0;
        }

        private void moveFromView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (moveFromView.SelectedIndex != -1)
                DisplayRoomsTo();
        }

        private void equipmentCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayRoomsFrom();
        }

        int ValidateAmountFormat()
        {
            try
            {
                int amount = Convert.ToInt32(amountTb.Text);
                return amount;
            }
            catch
            {
                return - 1;
            }
        }

        void ValidateAmountNumber(int amount)
        {
            if (amount < 0)
            {
                throw new Exception("Entered amount must be larger than 0.");
            }

            if (amount > roomsFrom[moveFromView.SelectedIndex].EquipmentAmount[equipment[equipmentCb.SelectedIndex]])
            {
                throw new Exception("Entered amount to be moved is larger than current amount available in the room.");
            }
        }

        void TryTransfer(DateTime momentOfTransfer, int amount)
        {
            try
            {
                factory.TransferController.Add(momentOfTransfer, roomsFrom[moveFromView.SelectedIndex],
                roomsTo[moveToView.SelectedIndex], equipment[equipmentCb.SelectedIndex], amount);
                MessageBox.Show("Transfer succsessfully added.");
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime momentOfTransfer;
            DateTime selectedDate = datePick.SelectedDate.Value;
            try
            {
                int hour = Convert.ToInt32(timeTb.Text.Split(':')[0]);
                int minute = Convert.ToInt32(timeTb.Text.Split(':')[1]);
                momentOfTransfer = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);
                if (momentOfTransfer < DateTime.Now)
                {
                    MessageBox.Show("You cannot enter date from the past.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Invalid time format!");
                return;
            }

            int amount = ValidateAmountFormat();
            if (amount == -1)
            {
                MessageBox.Show("Invalid Amount Format.");
                return;
            }
            try
            {
                ValidateAmountNumber(amount);
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
                return;
            }

            TryTransfer(momentOfTransfer, amount);
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayRoomsFrom();
        }

    }
}
