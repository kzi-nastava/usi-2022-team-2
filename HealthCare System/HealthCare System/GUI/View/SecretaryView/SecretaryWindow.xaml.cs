using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.AppotinmentRequests;
using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.SupplyRequests;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.AppointmentRequests;
using HealthCare_System.GUI.Controller.Appointments;
using HealthCare_System.GUI.Controller.DaysOffRequests;
using HealthCare_System.GUI.Controller.Equipments;
using HealthCare_System.GUI.Controller.EquipmentTransfers;
using HealthCare_System.GUI.Controller.MedicalRecords;
using HealthCare_System.GUI.Controller.Notifications;
using HealthCare_System.GUI.Controller.Rooms;
using HealthCare_System.GUI.Controller.SupplyRequests;
using HealthCare_System.GUI.Controller.Users;
using HealthCare_System.GUI.Main;
using HealthCare_System.GUI.View.SecretaryView;
using HealthCare_System.Model.Core.Appointments.Model;

namespace HealthCare_System.GUI.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        ServiceBuilder serviceBuilder;
        List<Patient> blockedPatients = new List<Patient>();
        Dictionary<Appointment, DateTime> replaceableAppointments;
        AddPatientWindow addPatientWin;
        bool showingBlocked;
        bool isOperation;

        PatientController patientController;
        EquipmentController equipmentController;
        EquipmentTransferController equipmentTransferController;
        RoomController roomController;
        AppointmentRequestController appointmentRequestController;
        UrgentSchedulingController urgentSchedulingController;
        MedicalRecordController medicalRecordController;
        SchedulingController schedulingController;
        SupplyRequestController supplyRequestController;
        DoctorController doctorController;
        DelayedAppointmentNotificationController delayedAppointmentNotificationController;
        DaysOffRequestController daysOffRequestController;


        public SecretaryWindow(ServiceBuilder serviceBuilder)
        {
            this.serviceBuilder = serviceBuilder;
            this.showingBlocked = false;

            InitializeComponent();
            InitializeControllers();

            SetEmergencyAppTab();
            SetReferralsTab();
            SetEquipmentTab();
            SetRoomsTab();
            SetDaysOffRequestsTab();
        }

        void InitializeControllers()
        {
            patientController = new(serviceBuilder.PatientService);
            equipmentController = new(serviceBuilder.EquipmentService);
            equipmentTransferController = new(serviceBuilder.EquipmentTransferService);
            roomController = new(serviceBuilder.RoomService);
            appointmentRequestController = new(serviceBuilder.AppointmentRequestService);
            urgentSchedulingController = new(serviceBuilder.UrgentSchedulingService);
            medicalRecordController = new(serviceBuilder.MedicalRecordService);
            schedulingController = new(serviceBuilder.SchedulingService);
            supplyRequestController = new(serviceBuilder.SupplyRequestService);
            doctorController = new(serviceBuilder.DoctorService);
            delayedAppointmentNotificationController = new(serviceBuilder.DelayedAppointmentNotificationService);
            daysOffRequestController = new(serviceBuilder.DaysOffRequestService);

        }

        #region TabSetUp
        private void SetEmergencyAppTab()
        {
            this.isOperation = false;
            this.textBoxDuration.IsEnabled = false;

            foreach (Patient patient in patientController.Patients())
            {
                cmbPatient.Items.Add(patient);
            }

            foreach (int i in Enum.GetValues(typeof(Specialization)))
            {
                cmbSpecialization.Items.Add((Specialization)i);
            }

            FillListBoxPatients();
            FillListBoxRequests();
        }

        private void SetReferralsTab()
        {
            foreach (Patient patient in patientController.Patients())
            {
                cmbPatientInReferrals.Items.Add(patient);
            }

            FillListBoxReferrals();

        }

        private void SetEquipmentTab()
        {
            foreach (Equipment equipment in equipmentController.Equipment())
            {
                if (equipment.Dynamic) cmbEquipment.Items.Add(equipment);
            }

            FillListBoxEquipment();

            if (listBoxEquipment.Items.Count == 0)
            {
                listBoxEquipment.Items.Add("There is currently enough amount of dynamic equipment.");
            }
        }

        private void SetRoomsTab()
        {
            foreach (Room room in roomController.Rooms())
            {
                cmbRoom.Items.Add(room);
                cmbRoomFrom.Items.Add(room);
                cmbRoomTo.Items.Add(room);
            }

            foreach (Equipment equipment in equipmentController.Equipment())
            {
                if (equipment.Dynamic) cmbEquipmentType.Items.Add(equipment);
            }
        }

        private void SetDaysOffRequestsTab()
        {
            FillListBoxDaysOffRequests(); 
        }
        #endregion


        #region ListBoxFiller
        private void FillListBoxEquipment()
        {
            listBoxEquipment.Items.Clear();
            Dictionary<Equipment, int> dynamicEquipment = new Dictionary<Equipment, int>();

            foreach (Room room in roomController.Rooms())
            {
                foreach(Equipment currentEquipment in room.EquipmentAmount.Keys)
                {
                    if (currentEquipment.Dynamic)
                    {
                        if (dynamicEquipment.ContainsKey(currentEquipment))
                        {
                            dynamicEquipment[currentEquipment] += room.EquipmentAmount[currentEquipment];
                        }
                        else
                        {
                            dynamicEquipment[currentEquipment] = room.EquipmentAmount[currentEquipment];
                        }
                    }
                }
            }

            foreach (Equipment equipment in dynamicEquipment.Keys)
            {
                if (dynamicEquipment[equipment] == 0)
                {
                    listBoxEquipment.Items.Add(equipment);
                }
            }
        }
        private void FillListBoxRequests()
        {
            listBoxRequests.Items.Clear();
            foreach (AppointmentRequest appRequest in appointmentRequestController.AppointmentRequests())
            {
                if (appRequest.State == AppointmentState.WAITING)
                {
                    listBoxRequests.Items.Add(appRequest);
                }
            }
        }
        
        private void FillListBoxPatients()
        {
            listBoxPatients.Items.Clear();
            foreach (Patient patient in patientController.Patients())
            {
                if (patient.Blocked == showingBlocked)
                {
                    listBoxPatients.Items.Add(patient);
                }
                
            }
        }

        private void FillListBoxAppointments(List<Doctor> doctors, int duration)
        {
            listBoxAppointments.Items.Clear();
            replaceableAppointments = urgentSchedulingController.GetReplaceableAppointments(doctors, duration, (Patient)cmbPatient.SelectedItem);
            foreach (KeyValuePair<Appointment, DateTime> item in replaceableAppointments.OrderBy(key => key.Value))
            {
                listBoxAppointments.Items.Add(item.Key);
            }
        }

        private void FillListBoxReferrals()
        {
            listBoxReferrals.Items.Clear();
            Patient patient = (Patient)cmbPatientInReferrals.SelectedItem;
            if (patient is null)
            {
                listBoxReferrals.Items.Add("There isn't any unused referral.");
            }
            else
            {
                foreach (MedicalRecord medicalRecord in medicalRecordController.MedicalRecords())
                {
                    if (medicalRecord.Patient == patient)
                    {
                        foreach (Referral referral in medicalRecord.Referrals)
                        {
                            if(referral.Used == false)
                            {
                                listBoxReferrals.Items.Add(referral);
                            }
                        }
                        break;
                    }
                }

                if (listBoxReferrals.Items.Count == 0)
                {
                    listBoxReferrals.Items.Add("There isn't any unused referral.");
                }
            }
        }

        private void FillListBoxEquipmentEnd(Room room)
        {
            listBoxEquipmentEnd.Items.Clear();

            foreach (Equipment equipment in room.EquipmentAmount.Keys)
            {
                if (equipment.Dynamic && room.EquipmentAmount[equipment] == 0)
                {
                    listBoxEquipmentEnd.Items.Add(equipment);
                }
            }

            if (listBoxEquipmentEnd.Items.Count == 0)
            {
                listBoxEquipmentEnd.Items.Add("There is no missing equipment in this room.");
            }
        }

        private void FillListBoxEquipmentNearEnd(Room room)
        {
            listBoxEquipmentNearEnd.Items.Clear();

            foreach (Equipment equipment in room.EquipmentAmount.Keys)
            {
                if (equipment.Dynamic && room.EquipmentAmount[equipment] <= 5 && room.EquipmentAmount[equipment] > 0)
                {
                    listBoxEquipmentNearEnd.Items.Add(equipment);
                }
            }

            if (listBoxEquipmentNearEnd.Items.Count == 0)
            {
                listBoxEquipmentNearEnd.Items.Add("There are enough amount of every equipment in this room.");
            }
        }

        private void FillListBoxDaysOffRequests()
        {
            foreach (DaysOffRequest request in daysOffRequestController.DaysOffRequests())
            {
                if (request.State == DaysOffRequestState.WAITING)
                {
                    listBoxDaysOffRequests.Items.Add(request);
                }
            }
        }
        #endregion



        #region PatientsTab
        private void UpdatePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to update!");
                return;
            }
            addPatientWin = new AddPatientWindow(patientController, medicalRecordController, serviceBuilder.IngredientService, true, patient);
            addPatientWin.Show();
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            addPatientWin = new AddPatientWindow(patientController, medicalRecordController, serviceBuilder.IngredientService, false, null);
            addPatientWin.Show();
        }

        private void ShowBlockedBtn_Click(object sender, RoutedEventArgs e)
        {
            showingBlocked = !showingBlocked;
            FillListBoxPatients();
            if (showingBlocked)
            {
                showBlockedBtn.Content = "View Regular";
                blockBtn.Content = "Unblock";
            }else
            {
                showBlockedBtn.Content = "View Blocked";
                blockBtn.Content = "Block";
            }
        }

        private void DeletePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to delete!");

            }
            else
            {
                try
                {
                    patientController.DeletePatient(patient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                FillListBoxPatients();
            }
        }

        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to block/unblock!");
            }
            else
            {
                patientController.BlockPatient(patient);
                showBlockedBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            FillListBoxPatients();
        }

        private void RefreshReferralsBtn_Click(object sender, RoutedEventArgs e)
        {
            FillListBoxReferrals();
        }
        #endregion


        #region AppointmentRequestTab
        private void AcceptRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to accept!");
            }
            else
            {
                appointmentRequestController.AcceptRequest(request);
                FillListBoxRequests();
                MessageBox.Show("You succesefully accepted selected request.");
            }
        }

        private void RejectRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to reject!");
            }
            else
            {
                appointmentRequestController.RejectRequest(request);
                FillListBoxRequests();
                MessageBox.Show("You succesefully rejected selected request.");
            }
        }

        private void RequestDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to see!");
            }
            else
            {
                RequestDetailsWindow requestDetailsWindow = new RequestDetailsWindow(request);
                requestDetailsWindow.Show();
            }
        }
        #endregion


        #region RoomsTab
        private void ShowRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)cmbRoom.SelectedItem;
            if (room is null)
            {
                MessageBox.Show("Select room!");
            }
            else
            {
                FillListBoxEquipmentEnd(room);
                FillListBoxEquipmentNearEnd(room);
            }
        }

        private TransferDto ValidateTransfer()
        {
            Room fromRoom = (Room)cmbRoomFrom.SelectedItem;
            Room toRoom = (Room)cmbRoomTo.SelectedItem;
            Equipment equipment = (Equipment)cmbEquipmentType.SelectedItem;
            if (fromRoom is null || toRoom is null || equipment is null)
            {
                MessageBox.Show("Select rooms and equipment type!");
                throw new Exception();
            }

            int amount = Convert.ToInt32(textBoxEquipmentTransferQuantity.Text);
            if (fromRoom.EquipmentAmount[equipment] < amount)
            {
                MessageBox.Show("The room has less selected equipment in stock than entered.");
                throw new Exception();
            }

            return new TransferDto(0, DateTime.Now, amount, fromRoom, toRoom, equipment);
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {

            TransferDto transferDto = ValidateTransfer();
            equipmentTransferController.ExecuteTransfer(transferDto);

            MessageBox.Show("You have successfully transfered equipment.");
        }
        #endregion


        #region EmergencyAppointmentTab
        int getDuration()
        {
            int duration = 15;
            if (isOperation)
            {
                try
                {
                    duration = Convert.ToInt32(textBoxDuration.Text);
                }
                catch
                {
                    MessageBox.Show("Duration is in the wrong format. It is automatically set to 15 minutes.");
                }
            }
            return duration;
        }

        private UrgentAppointmentDto ValidateUrgentAppointment()
        {
            int duration = getDuration();
            Specialization specialization = (Specialization)cmbSpecialization.SelectedItem;
            List<Doctor> doctors = doctorController.FindBySpecialization((Specialization)cmbSpecialization.SelectedItem);
            Patient patient = (Patient)cmbPatient.SelectedItem;

            return new UrgentAppointmentDto(doctors, patient, duration);
        }

        private void BookClosestAppointment(object sender, RoutedEventArgs e)
        {
            UrgentAppointmentDto urgentAppointmentDto = ValidateUrgentAppointment();
            if (urgentAppointmentDto.Patient is null) return;

            try
            {
                urgentSchedulingController.BookClosestEmergancyAppointment(urgentAppointmentDto);
                MessageBox.Show("You succesefully booked emergency appointment.");
            }
            catch(Exception ex)
            {
                FillListBoxAppointments(urgentAppointmentDto.Doctors, urgentAppointmentDto.Duration);
                MessageBox.Show(ex.Message);
            }
        }

        private void rbOperation_Checked(object sender, RoutedEventArgs e)
        {
            this.isOperation = true;
            this.textBoxDuration.IsEnabled = true;
        }

        private void rbExamination_Checked(object sender, RoutedEventArgs e)
        {
            this.isOperation = false;
            this.textBoxDuration.IsEnabled = false;
        }

        private void bookAndReplaceBtn_Click(object sender, RoutedEventArgs e)
        {
            Appointment toReplaceAppointment = (Appointment)listBoxAppointments.SelectedItem;
            if (toReplaceAppointment is null)
            {
                MessageBox.Show("Select appointment You want to replace!");
            }
            else
            {
                UrgentAppointmentDto urgentAppointmentDto = new UrgentAppointmentDto(toReplaceAppointment.Doctor, (Patient)cmbPatient.SelectedItem, getDuration());

                urgentAppointmentDto.DelayedStart = replaceableAppointments[toReplaceAppointment];
                urgentAppointmentDto.DelayedEnd = replaceableAppointments[toReplaceAppointment].AddMinutes(getDuration());

                Appointment newAppointment = urgentSchedulingController.ReplaceAppointment(toReplaceAppointment, urgentAppointmentDto);
                delayedAppointmentNotificationController.AddNotification(toReplaceAppointment, newAppointment.Start);

                MessageBox.Show("Doctor and patient are informed about appointment delay.");
            }
        }
        #endregion


        #region ReferralsTab
        private void bookByReferralBtn_Click(object sender, RoutedEventArgs e)
        {
            Referral referral = (Referral)listBoxReferrals.SelectedItem;
            if (referral is null)
            {
                MessageBox.Show("Select referral You want to use for new appointment!");
            }
            else
            {
                Appointment appointment = schedulingController.BookAppointmentByReferral(referral);
                MessageBox.Show("You successfully booked new appointment using selected referral.\nAppointment start: " + appointment.Start);
            }
        }
        #endregion


        #region EquipmentTab
        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)cmbEquipment.SelectedItem;
            if (equipment is null)
            {
                MessageBox.Show("Select equipment You want to order!");
            }
            else
            {
                int quantity;
                if (textBoxEquipmentQuantity.Text == "")
                {
                    quantity = 1;
                }
                else
                {
                    quantity = Convert.ToInt32(textBoxEquipmentQuantity.Text);
                }

                supplyRequestController.AddSupplyRequest(equipment, quantity);

                MessageBox.Show("You have succesefully orderd new equipment.");
            }
        }
        #endregion


        #region daysOffRequests

        private void acceptDaysOffRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            DaysOffRequest daysOffRequest = (DaysOffRequest)listBoxDaysOffRequests.SelectedItem;
            if (daysOffRequest is null)
            {
                MessageBox.Show("Select request!");
            }
            else
            {
                daysOffRequestController.AcceptDaysOffRequest(daysOffRequest);
                MessageBox.Show("Doctor is informed about request acceptance!");
            }
        }

        private void rejectDaysOffRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            DaysOffRequest daysOffRequest = (DaysOffRequest)listBoxDaysOffRequests.SelectedItem;
            if (daysOffRequest is null)
            {
                MessageBox.Show("Select request!");
            }
            else
            {
                string message = "";
                DaysOffRejectionMessage daysOffRejectionMessage = new DaysOffRejectionMessage(daysOffRequestController, daysOffRequest, message);
                daysOffRejectionMessage.Show();
            }
        }
        #endregion


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(serviceBuilder);
                main.Show();
            }
            else e.Cancel = true;
        }
    }
}
