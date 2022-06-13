using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using HealthCare_System.Database;
using HealthCare_System.Services.EquipmentServices;

namespace HealthCare_System.gui
{
    
    public partial class DynamicEquipmentWindow : Window
    {
        Room room;
        HealthCareDatabase database;
        Dictionary<string, KeyValuePair<Equipment, int>> dynamicEquipmentDisplay;
        EquipmentTransferService equpmentTransferService;

        public DynamicEquipmentWindow(Room room, HealthCareDatabase database)
        {
            this.room = room;
            this.database =  database;

            InitializeComponent();

            InitializeEquipment();

            equpmentTransferService = new(database.EquipmentTransferRepo, null);

            roomNameLbl.Content = room.Name;
            nameTb.IsReadOnly = true;
        }

        void InitializeEquipment()
        {
            dynamicEquipmentView.Items.Clear();
            dynamicEquipmentDisplay = new Dictionary<string, KeyValuePair<Equipment, int>>();
            Dictionary<Equipment, int> dynamicEquipment = room.FilterDynamicEquipment();
            Dictionary<Equipment, int> sortedDynamicEquipment = dynamicEquipment.OrderBy(x => x.Key.Name).
                ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<Equipment, int> entry in sortedDynamicEquipment)
            {
                string key = entry.Key.Name + ": " + entry.Value;
                dynamicEquipmentDisplay.Add(key, entry);
                dynamicEquipmentView.Items.Add(key);
            }
        }

        private void DynamicEquipmentView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dynamicEquipmentView.SelectedIndex != -1)
            {
                KeyValuePair<Equipment, int> equipment = dynamicEquipmentDisplay[dynamicEquipmentView.SelectedItem.ToString()];
                nameTb.Text = equipment.Key.Name;
                amountTb.Text = "0";
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<Equipment, int> equipment = dynamicEquipmentDisplay[dynamicEquipmentView.SelectedItem.ToString()];

            int spentAmount = Convert.ToInt32(amountTb.Text);
            if (spentAmount > equipment.Value)
            {
                MessageBox.Show("The spent amount of equipment can't be greater then the current amount!");
                return;
            }

            equpmentTransferService.MoveFromRoom(room, equipment.Key, spentAmount);
            database.RoomRepo.Serialize();
            InitializeEquipment();
            MessageBox.Show("Equipment updated!");
        }

        private void amauntTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
