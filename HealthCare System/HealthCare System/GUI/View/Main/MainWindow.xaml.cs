using System.Windows;
using HealthCare_System.gui;
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

namespace HealthCare_System.GUI.Main
{
    public partial class MainWindow : Window
    {
        HealthCareDatabase database;
        SecretaryWindow sc;

        RoomService roomService;
        EquipmentService equipmentService;
        MergingRenovationService mergingRenovationService;
        SimpleRenovationService simpleRenovationService;
        SplittingRenovationService splittingRenovationService;
        AppointmentService appointmentService;
        EquipmentTransferService equipmentTransferService;
        SupplyRequestService supplyRequestService;

        public MainWindow(HealthCareDatabase database)
        {
            this.database = database;
            InitializeServices();

            InitializeComponent();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
            supplyRequestService.TryToExecuteSupplyRequest();
        }
        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
            supplyRequestService.TryToExecuteSupplyRequest();
        }

        private void InitializeServices()
        {
            roomService = new RoomService(null, null, null, null, null, database.RoomRepo);
            equipmentService = new EquipmentService(database.EquipmentRepo, roomService);
            equipmentTransferService = new EquipmentTransferService(database.EquipmentTransferRepo, roomService);
            mergingRenovationService = new MergingRenovationService(database.MergingRenovationRepo, roomService,
                equipmentTransferService, equipmentService);
            simpleRenovationService = new SimpleRenovationService(database.SimpleRenovationRepo, roomService,
                equipmentTransferService, equipmentService);
            splittingRenovationService = new SplittingRenovationService(database.SplittingRenovationRepo, roomService,
                equipmentTransferService, equipmentService);
            appointmentService = new AppointmentService(database.AppointmentRepo, null);
            roomService.MergingRenovationService = mergingRenovationService;
            roomService.SplittingRenovationService = splittingRenovationService;
            roomService.SimpleRenovationService = simpleRenovationService;
            roomService.AppointmentService = appointmentService;
            roomService.EquipmentTransferService = equipmentTransferService;
            supplyRequestService = new(database.SupplyRequestRepo, roomService, equipmentTransferService);
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passwordTb.Password;

            DoctorService doctorService = new(database.DoctorRepo, null);
            PatientService patientService = new(database.PatientRepo, null, null, null, null);
            ManagerService managerService = new(database.ManagerRepo);
            SecretaryService secretaryService = new(database.SecretaryRepo);
            AppointmentRequestService appointmentRequestService = new(database.AppointmentRequestRepo, null);
            UserService userService = new(patientService, doctorService, managerService, secretaryService, appointmentRequestService);

            Person person = userService.Login(mail, password);
            if (person is null)
            {
                MessageBox.Show("Invalid mail or password!");
                passwordTb.Clear();
            }
            else if (person.GetType() == typeof(Doctor))
            {
                Window doctorWindow = new DoctorWindow((Doctor)person, database);
                doctorWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Patient))
            {
                if (!((Patient)person).Blocked)
                {
                    PatientWindow patientWindow = new PatientWindow(person, database);
                    patientWindow.Show();
                    Close();
                }
                
            }
            else if (person.GetType() == typeof(Manager)) 
            {
                Window managerWindow = new ManagerWindow(database);
                managerWindow.Show();
                Close();
            }
            else if (person.GetType() == typeof(Secretary))
            {
                MessageBox.Show("Logged in as " + person.FirstName + " " + person.LastName);
                SecretaryWindow secretaryWindow = new SecretaryWindow(database);
                secretaryWindow.Show();
                Close();
            }
        }
    }

}
