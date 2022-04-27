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

        public EquipmentMoveWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            InitializeComboBox();
        }

        void DisplayRoomsFrom()
        {
            MoveFromView.Items.Clear();
            int index = 0;
            foreach (Room room in factory.RoomController.Rooms)
            {
                MoveFromView.Items.Add("Name: " + room.Name + ", Type: " + room.Type + ", Amount: " + room.EquipmentAmount[(Equipment)EquipmentCb.SelectedItem].ToString());
                roomsFrom[index] = room;
                index++;
            }
            MoveFromView.SelectedIndex = 0;
        }

        void DisplayRoomsTo()
        {
            MoveToView.Items.Clear();
            int index = 0;
            foreach (Room room in factory.RoomController.Rooms)
            {
                if (room != roomsFrom[MoveFromView.SelectedIndex]) 
                {
                    MoveToView.Items.Add("Name: " + room.Name + ", Type: " + room.Type + ", Amount: " + room.EquipmentAmount[(Equipment)EquipmentCb.SelectedItem].ToString());
                    roomsFrom[index] = room;
                    index++;
                }   
            }
            MoveToView.SelectedIndex = 0;
        }

        void InitializeComboBox()
        {
            foreach (Equipment equipment in factory.EquipmentController.Equipment)
                EquipmentCb.Items.Add(equipment);
            EquipmentCb.SelectedIndex = 0;
        }

        private void MoveFromView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MoveFromView.SelectedIndex != -1)
                DisplayRoomsTo();
        }

        private void EquipmentCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayRoomsFrom();
        }
    }
}
