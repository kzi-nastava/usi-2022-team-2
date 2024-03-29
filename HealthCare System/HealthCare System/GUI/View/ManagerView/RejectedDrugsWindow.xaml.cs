﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Drugs;

namespace HealthCare_System.GUI.ManagerView
{
    
    public partial class RejectedDrugsWindow : Window
    {
        ServiceBuilder serviceBuilder;
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();

        DrugController drugController;
        
        public RejectedDrugsWindow(ServiceBuilder serviceBuilder)
        {
            InitializeComponent();
            this.serviceBuilder  =  serviceBuilder;

            InitializeControllers();
            InitializeComboBox();
            if (listedDrugs.Count == 0)
            {
                titleLbl.Content = "THere are no rejected drugs";
                correctBtn.IsEnabled = false;
            }
        }

        void InitializeControllers()
        {
            drugController = new(serviceBuilder.DrugService);
        }

        void InitializeComboBox()
        {
            rejectedDrugsCb.Items.Clear();
            int index = 0;
            foreach (Drug drug in drugController.Drugs())
            {
                if (drug.Status == DrugStatus.REJECTED)
                {
                    rejectedDrugsCb.Items.Add(drug.Id);
                    listedDrugs[index] = drug;
                    index++;
                }
            }
            rejectedDrugsCb.SelectedIndex = 0;
        }

        public void DisplayDrugData()
        {
            nameTb.Text = listedDrugs[rejectedDrugsCb.SelectedIndex].Name;
            messageTbl.Text = listedDrugs[rejectedDrugsCb.SelectedIndex].Message;
        }

        private void rejectedDrugsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rejectedDrugsCb.SelectedIndex != -1)
            {
                DisplayDrugData();
            }
        }

        private void correctBtn_Click(object sender, RoutedEventArgs e)
        {
            Window drugWindow = new DrugWindow(false, serviceBuilder, 
                listedDrugs[rejectedDrugsCb.SelectedIndex]);
            drugWindow.Show();
            Close();
        }
    }
}
