﻿using System.Windows;
using HealthCare_System.entities;
using System;
using HealthCare_System.controllers;
using HealthCare_System.factory;
using HealthCare_System.gui;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HealthCareFactory factory = new();
        
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;
            factory.PrintContnent();
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
                factory.User = person;
                PatientWindow patientWindow = new PatientWindow(factory);
                patientWindow.Show();
            }
            else if (person.GetType() == typeof(Manager)) {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
            }
            else if (person.GetType() == typeof(Secretary))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
            }
        }

    }

}
