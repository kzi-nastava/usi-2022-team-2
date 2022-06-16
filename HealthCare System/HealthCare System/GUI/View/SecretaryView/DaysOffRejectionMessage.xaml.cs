using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.GUI.Controller.DaysOffRequests;
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

namespace HealthCare_System.GUI.View.SecretaryView
{
    /// <summary>
    /// Interaction logic for DaysOffRejectionMessage.xaml
    /// </summary>
    public partial class DaysOffRejectionMessage : Window
    {
        DaysOffRequest daysOffRequest;
        DaysOffRequestController daysOffRequestController;
        public DaysOffRejectionMessage(DaysOffRequestController daysOffRequestController, DaysOffRequest daysOffRequest)
        {
            this.daysOffRequest = daysOffRequest;
            this.daysOffRequestController = daysOffRequestController;
            InitializeComponent();
        }

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string GetMessage()
        {
            string message = textBoxMessage.Text;
            if (message == "")
            {
                throw new Exception("Input rejection reason.");
            }
            return message;
        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = textBoxMessage.Text;
                daysOffRequestController.RejectDaysOffRequest(daysOffRequest, message);
                MessageBox.Show("Doctor is informed about request rejection!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
