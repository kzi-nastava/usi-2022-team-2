using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HealthCare_System.factory;
using HealthCare_System.entities;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for EquipmentMoveWindow.xaml
    /// </summary>
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
        }

        void DisplayRoomsFrom()
        {
            moveFromView.Items.Clear();
            int index = 0;
            foreach (Room room in factory.RoomController.Rooms)
            {
                moveFromView.Items.Add("Id: " + room.Id + ", Name: " + room.Name + ", Amount: " + room.EquipmentAmount[equipment[equipmentCb.SelectedIndex]].ToString());
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

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime momentOfTransfer;
            DateTime date = datePick.SelectedDate.Value;

            try
            {
                int hour = Convert.ToInt32(timeTb.Text.Split(':')[0]);
                int minute = Convert.ToInt32(timeTb.Text.Split(':')[1]);
                momentOfTransfer = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                if (momentOfTransfer < DateTime.Now)
                {
                    MessageBox.Show("You cannot enter date from the past.");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("Invalid Time Format.");
                return;
            }

            int amount;
            try
            {
                amount = Convert.ToInt32(amountTb.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Amount Format.");
                return;
            }

            if (amount < 0)
            {
                MessageBox.Show("Entered amount must be larger than 0.");
                return;
            }

            if (amount > roomsFrom[moveFromView.SelectedIndex].EquipmentAmount[equipment[equipmentCb.SelectedIndex]])
            {
                MessageBox.Show("Entered amount to be moved is larger than current amount available in the room.");
                return;
            }

            try
            {
                factory.TransferController.AddTransfer(momentOfTransfer, roomsFrom[moveFromView.SelectedIndex],
                roomsTo[moveToView.SelectedIndex], equipment[equipmentCb.SelectedIndex], amount);
                MessageBox.Show("Transfer succsessfully added.");
            }
            catch
            {
                MessageBox.Show("Entered amount to be moved is larger than amount availabel " +
                    "in the room after all the transfers are finished.");
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayRoomsFrom();
        }

    }
}
