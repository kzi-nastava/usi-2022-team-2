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
                TitleLbl.Content = "Create new room";
            else
                TitleLbl.Content = "Update room";
        }

        void InitializeFields()
        {
            if (!createNewRoom)
                NameTb.Text = room.Name;
            else
                NameTb.Text = "";
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                if (roomType != TypeOfRoom.STORAGE)
                    TypeCb.Items.Add(roomType);
            }
            if (!createNewRoom)
                TypeCb.SelectedItem = room.Type;
            else
                TypeCb.SelectedIndex = 0;

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
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
                    factory.RoomController.CreateNewRoom(NameTb.Text, (TypeOfRoom)TypeCb.SelectedItem, equipmentAmount);
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
                    factory.RoomController.UpdateRoom(room, NameTb.Text, (TypeOfRoom)TypeCb.SelectedItem);
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
