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

        public EquipmentMoveWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            DisplayRoomsFrom();
            InitializeComboBox();
        }

        void DisplayRoomsFrom()
        {
            MoveFromView.Items.Clear();
            foreach (Room room in factory.RoomController.Rooms)
            {
                MoveFromView.Items.Add(room);
            }
            MoveFromView.SelectedIndex = 0;
        }

        void DisplayRoomsTo()
        {
            MoveToView.Items.Clear();
            foreach (Room room in factory.RoomController.Rooms)
            {
                if (room != (Room)MoveFromView.SelectedItem)
                    MoveToView.Items.Add(room);
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
            DisplayRoomsTo();
        }
    }
}
