using System.Windows;
using HealthCare_System.Database;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.SupplyRequests;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.AppotinmentRequests;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.GUI.DoctorView;
using HealthCare_System.GUI.SecretaryView;
using HealthCare_System.GUI.PatientView;
using HealthCare_System.GUI.ManagerView;

namespace HealthCare_System.GUI.Main
{
    public partial class MainWindow : Window
    {
        ServiceBuilder serviceBuilder;
        SecretaryWindow sc;

        RoomService roomService;
        EquipmentService equipmentService;
        MergingRenovationService mergingRenovationService;
        SimpleRenovationService simpleRenovationService;
        SplittingRenovationService splittingRenovationService;
        AppointmentService appointmentService;
        EquipmentTransferService equipmentTransferService;
        SupplyRequestService supplyRequestService;

        public MainWindow(ServiceBuilder serviceBuilder)
        {
            this.serviceBuilder = serviceBuilder;
            InitializeComponent();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
            supplyRequestService.TryToExecuteSupplyRequest();
        }
        public MainWindow()
        {
            InitializeComponent();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
            supplyRequestService.TryToExecuteSupplyRequest();
        }

        

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;

            

            Person person = serviceBuilder.UserService.Login(mail, password);
            if (person is null)
            {
                MessageBox.Show("Invalid mail or password!");
                passwordTb.Clear();
            }
            else if (person.GetType() == typeof(Doctor))
            {
                Window doctorWindow = new DoctorWindow((Doctor)person, serviceBuilder);
                doctorWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Patient))
            {
                if (!((Patient)person).Blocked)
                {
                    PatientWindow patientWindow = new PatientWindow(person, serviceBuilder);
                    patientWindow.Show();
                    Close();
                }
                
            }
            else if (person.GetType() == typeof(Manager)) 
            {
                Window managerWindow = new ManagerWindow(serviceBuilder);
                managerWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Secretary))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                SecretaryWindow secretaryWindow = new SecretaryWindow(serviceBuilder);
                secretaryWindow.Show();
                Close();
            }
        }
    }

}
