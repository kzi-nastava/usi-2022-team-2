using System.Windows;
using HealthCare_System.entities;
using System;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passTb.Password;

            Doctor doctor = new Doctor("firstName", "lastName", new DateTime(2022, 12, 31), "mail", "password");
            mailTb.Text = doctor.ToString();
        }

    }

}
