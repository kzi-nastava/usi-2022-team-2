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
        }

        #region SimpleRenovations
       
        #endregion

        #region MergingRenovations
       
        #endregion

        #region SplittingRenovations
        
        #endregion
    }
}
