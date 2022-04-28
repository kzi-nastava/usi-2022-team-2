using System.Windows;
using HealthCare_System.entities;
using System;
using HealthCare_System.controllers;
using HealthCare_System.factory;
using HealthCare_System.gui;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HealthCareFactory factory = new();
        SecretaryWindow sc;


        public MainWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            InitializeComponent();
            StartTransfers();
        }
        public MainWindow()
        {
            factory = new();
            InitializeComponent();
            StartTransfers();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;
            factory.PrintContnent();
            Person person = factory.Login(mail, password);
            Console.WriteLine( factory.DoctorController.FindByJmbg("1001").IsAvailable(new DateTime(2022, 4, 25, 13, 01, 0), new DateTime(2022, 4, 25, 13, 16, 0)));

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
                factory.User = person;
                PatientWindow patientWindow = new PatientWindow(factory);
                patientWindow.Show();
                this.Close();
            }
            else if (person.GetType() == typeof(Manager)) {
                Window managerWindow = new ManagerWindow(factory);
                managerWindow.Show();
            }
            else if (person.GetType() == typeof(Secretary))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                sc = new SecretaryWindow(factory);
                sc.Show();
            }
        }

        private async void StartTransfers()
        {
            await Task.Run(() => DoTransfers());
        }

        private void DoTransfers()
        {
            Thread.Sleep(300000);
            if (factory.TransferController.Transfers.Count > 0)
            {
                List<Transfer> copyTransfers = new List<Transfer>();
                foreach (Transfer copyTransfer in factory.TransferController.Transfers)
                {
                    copyTransfers.Add(copyTransfer);
                }

                foreach (Transfer transfer in copyTransfers)
                {
                    if (transfer.MomentOfTransfer < DateTime.Now)
                        factory.ExecuteTransfer(transfer);
                }
            }
            

        }

    }

}
