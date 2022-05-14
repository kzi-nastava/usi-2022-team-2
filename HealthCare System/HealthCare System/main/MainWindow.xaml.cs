using System.Windows;
using HealthCare_System.entities;
using HealthCare_System.factory;
using HealthCare_System.gui;

namespace HealthCare_System
{
    public partial class MainWindow : Window
    {
        HealthCareFactory factory;
        SecretaryWindow sc;


        public MainWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            InitializeComponent();
            factory.TryToExecuteSimpleRenovations();
            factory.TryToExecuteMergingRenovations();
            factory.TryToExecuteSplittingRenovations();
        }
        public MainWindow()
        {
            factory = new();
            InitializeComponent();
            factory.TryToExecuteSimpleRenovations();
            factory.TryToExecuteMergingRenovations();
            factory.TryToExecuteSplittingRenovations();
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
                    Close();
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
                factory.User = person;
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                SecretaryWindow secretaryWindow = new SecretaryWindow(factory);
                secretaryWindow.Show();
                Close();
            }
        }
    }

}
