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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        HealthCareFactory factory;
        public ManagerWindow()
        {
            InitializeComponent();
            factory = new();
            foreach (Room room in factory.RoomController.Rooms)
            {
                RoomView.Items.Add(room);
            }
        }

        

        private void NewDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RejectedDrugsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteDrugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RoomEquipementBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RenovateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRoomBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteIngredientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
