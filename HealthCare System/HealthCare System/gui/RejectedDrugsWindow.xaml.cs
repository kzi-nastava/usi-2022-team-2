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
using HealthCare_System.Model;
using HealthCare_System.Database;
using HealthCare_System.Services.DrugServices;

namespace HealthCare_System.gui
{
    
    public partial class RejectedDrugsWindow : Window
    {
        HealthCareDatabase database;
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();

        DrugService drugService;
        
        public RejectedDrugsWindow(HealthCareDatabase database)
        {
            InitializeComponent();
            this.database  =  database;

            InitializeServices();
            InitializeComboBox();
            if (listedDrugs.Count == 0)
            {
                titleLbl.Content = "THere are no rejected drugs";
                correctBtn.IsEnabled = false;
            }
        }

        void InitializeServices()
        {
            drugService = new DrugService(database.DrugRepo, null);
        }

        void InitializeComboBox()
        {
            rejectedDrugsCb.Items.Clear();
            int index = 0;
            foreach (Drug drug in database.DrugRepo.Drugs)
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
            Window drugWindow = new DrugWindow(false, database, 
                listedDrugs[rejectedDrugsCb.SelectedIndex]);
            drugWindow.Show();
            Close();
        }
    }
}
