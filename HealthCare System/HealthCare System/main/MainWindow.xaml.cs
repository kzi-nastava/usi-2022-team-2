using System.Windows;
using HealthCare_System.Model;
using HealthCare_System.factory;
using HealthCare_System.gui;
using HealthCare_System.Database;
using HealthCare_System.Services.UserServices;

namespace HealthCare_System
{
    public partial class MainWindow : Window
    {
        HealthCareFactory factory;
        HealthCareDatabase database;
        SecretaryWindow sc;

        public MainWindow(HealthCareFactory factory, HealthCareDatabase database)
        {
            this.factory = factory;
            this.database = database;
            database.PrintContnent();
            InitializeComponent();
            factory.TryToExecuteSimpleRenovations();
            factory.TryToExecuteMergingRenovations();
            factory.TryToExecuteSplittingRenovations();
            factory.TryToExecuteSupplyRequest();
        }
        public MainWindow()
        {
            factory = new();
            
            InitializeComponent();
            factory.TryToExecuteSimpleRenovations();
            factory.TryToExecuteMergingRenovations();
            factory.TryToExecuteSplittingRenovations();
            factory.TryToExecuteSupplyRequest();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;

            DoctorService doctorService = new(database.DoctorRepo, null);
            PatientService patientService = new(database.PatientRepo, null, null, null, null);
            ManagerService managerService = new(database.ManagerRepo);
            SecretaryService secretaryService = new(database.SecretaryRepo);

            UserService userService = new(patientService, doctorService, managerService, secretaryService, null);

            Person person = userService.Login(mail, password);
            if (person is null)
            {
                MessageBox.Show("Invalid mail or password!");
                passwordTb.Clear();
            }
            else if (person.GetType() == typeof(Doctor))
            {
                factory.User = person;
                Window doctorWindow = new DoctorWindow(factory,database);
                doctorWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Patient))
            {
                factory.User = person;
                if (!((Patient)person).Blocked)
                {
                    PatientWindow patientWindow = new PatientWindow(factory, database);
                    patientWindow.Show();
                    Close();
                }
                
            }
            else if (person.GetType() == typeof(Manager)) 
            {
                factory.User = person;
                Window managerWindow = new ManagerWindow(factory, database);
                managerWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Secretary))
            {
                factory.User = person;
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                SecretaryWindow secretaryWindow = new SecretaryWindow(factory, database);
                secretaryWindow.Show();
                Close();
            }
        }
    }

}
