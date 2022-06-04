using System.Windows;
using HealthCare_System.Model;
using HealthCare_System.factory;
using HealthCare_System.gui;
using HealthCare_System.Database;
using HealthCare_System.Services.UserServices;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.RenovationServices;
using HealthCare_System.Services.EquipmentServices;
using HealthCare_System.Services.AppointmentServices;

namespace HealthCare_System
{
    public partial class MainWindow : Window
    {
        HealthCareFactory factory;
        HealthCareDatabase database;
        SecretaryWindow sc;

        RoomService roomService;
        EquipmentService equipmentService;
        MergingRenovationService mergingRenovationService;
        SimpleRenovationService simpleRenovationService;
        SplittingRenovationService splittingRenovationService;
        AppointmentService appointmentService;
        EquipmentTransferService equipmentTransferService;

        public MainWindow(HealthCareFactory factory, HealthCareDatabase database)
        {
            this.factory = factory;
            this.database = database;

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

            database.PrintContnent();
            InitializeComponent();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
            factory.TryToExecuteSupplyRequest();
        }
        public MainWindow()
        {
            factory = new();

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

            InitializeComponent();
            simpleRenovationService.TryToExecuteSimpleRenovations();
            mergingRenovationService.TryToExecuteMergingRenovations();
            splittingRenovationService.TryToExecuteSplittingRenovations();
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
                factory.User = person;
                Window doctorWindow = new DoctorWindow((Doctor)person, database);
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
                Window managerWindow = new ManagerWindow(database);
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
