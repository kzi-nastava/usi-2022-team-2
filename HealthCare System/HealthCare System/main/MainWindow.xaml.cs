using System.Windows;
using HealthCare_System.entities;
using System;
using HealthCare_System.controllers;
using HealthCare_System.factory;
using HealthCare_System.gui;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Threading;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HealthCareFactory factory;
        SecretaryWindow sc;


        public MainWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            InitializeComponent();
        }
        public MainWindow()
        {
            factory = new();
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;
            Person person = factory.Login(mail, password);

            if (person is null)
            {
                MessageBox.Show("Invalid mail or password!");
                passwordTb.Clear();
            }
            else if (person.GetType() == typeof(Doctor))
            {
                factory.User = person;
                Window doctorWindow = new DoctorWindow(factory);
                doctorWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Patient))
            {
                factory.User = person;
                if (!((Patient)person).Blocked)
                {
                    PatientWindow patientWindow = new PatientWindow(factory);
                    patientWindow.Show();
                    this.Close();
                }
                
            }
            else if (person.GetType() == typeof(Manager)) 
            {
                factory.User = person;
                Window managerWindow = new ManagerWindow(factory);
                managerWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Secretary))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                SecretaryWindow secretaryWindow = new SecretaryWindow(factory);
                secretaryWindow.Show();
            }
        }
    }

}
