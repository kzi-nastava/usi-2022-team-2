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
            if (!this.createNewRoom)
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
            NameTb.Text = room.Name;
            foreach (TypeOfRoom roomType in Enum.GetValues(typeof(TypeOfRoom)))
            {
                TypeCb.Items.Add(roomType);
            }
            TypeCb.SelectedItem = room.Type;
            
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
