using System.Windows;
using HealthCare_System.entities;
using System;
using HealthCare_System.controllers;
using HealthCare_System.factory;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HealthCareFactory factory = new();
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;

            Person person = factory.Login(mail, password);

            //TODO add the rest of user types
            if (person is null)
            {
                MessageBox.Show("Invalid mail or password!");
                passwordTb.Clear();
            }
            else if (person.GetType() == typeof(Doctor))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
            }
            else if (person.GetType() == typeof(Patient))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
            }
        }

    }

}
