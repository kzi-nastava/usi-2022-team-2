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

namespace HealthCare_System.gui
{
    
    public partial class RejectedDrugsWindow : Window
    {
        HealthCareFactory factory;
        Dictionary<int, Drug> listedDrugs = new Dictionary<int, Drug>();
        
        public RejectedDrugsWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            InitializeComboBox();
            if (listedDrugs.Count == 0)
            {
                titleLbl.Content = "THere are no rejected drugs";
                correctBtn.IsEnabled = false;
            }
        }

        void InitializeComboBox()
        {
            rejectedDrugsCb.Items.Clear();
            int index = 0;
            foreach (Drug drug in factory.DrugController.Drugs)
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
            Window drugWindow = new DrugWindow(factory, false, listedDrugs[rejectedDrugsCb.SelectedIndex]);
            drugWindow.Show();
            Close();
        }
    }
}
