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
using HealthCare_System.entities;
using HealthCare_System.factory;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
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
                Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
                foreach (Equipment equipment in factory.EquipmentController.Equipment)
                {
                    equipmentAmount[equipment] = 0;
                }
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
