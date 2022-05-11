using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using HealthCare_System.factory;
using HealthCare_System.entities;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for RenovationWindow.xaml
    /// </summary>
    public partial class RenovationWindow : Window
    {
        HealthCareFactory factory;
        public RenovationWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            InitializeComponent();
            DisplaySimpleRenovations(factory.SimpleRenovationController.SimpleRenovations);
            DisplayMergingRenovations(factory.MergingRenovationController.MergingRenovations);
            DisplaySplittingRenovations(factory.SplittingRenovationController.SplittingRenovations);
        }

        #region SimpleRenovations
        public void DisplaySimpleRenovations(List<SimpleRenovation> simpleRenovations)
        {
            if (simpleRenovations.Count != 0) {
                foreach (SimpleRenovation simpleRenovation in simpleRenovations)
                {
                    simpleRenovationsView.Items.Add(simpleRenovation);
                }
            }
            else
            {
                simpleRenovationsView.Items.Add("No data");
            }
            
        }
        #endregion

        #region MergingRenovations
        public void DisplayMergingRenovations(List<MergingRenovation> mergingRenovations)
        {
            if (mergingRenovations.Count != 0) 
            {
                foreach (MergingRenovation mergingRenovation in mergingRenovations)
                {
                    mergingRenovationsView.Items.Add(mergingRenovation);
                }
            }
            else
            {
                mergingRenovationsView.Items.Add("No data");
            }
            
        }
        #endregion

        #region SplittingRenovations
        public void DisplaySplittingRenovations(List<SplittingRenovation> splittingRenovations)
        {
            if (splittingRenovations.Count != 0)
            {
                foreach (SplittingRenovation splittingRenovation in splittingRenovations)
                {
                    splittingRenovationsView.Items.Add(splittingRenovation);
                }
            }
            else
            {
                splittingRenovationsView.Items.Add("No data");
            }
        }
        #endregion
    }
}
