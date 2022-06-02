using HealthCare_System.controllers;
using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;

namespace HealthCare_System.factory
{
    

    public class HealthCareFactory
    {
        AnamnesisController anamnesisController;
        AppointmentController appointmentController;
        AppointmentRequestController appointmentRequestController;
        DaysOffNotificationController daysOffNotificationController;
        DaysOffRequestController daysOffRequestController;
        DelayedAppointmentNotificationController delayedAppointmentNotificationController;
        DoctorController doctorController;
        DoctorSurveyController doctorSurveyController;
        DrugController drugController;
        DrugNotificationController drugNotificationController;
        EquipmentController equipmentController;
        HospitalSurveyController hospitalSurveyController;
        IngredientController ingredientController;
        ManagerController managerController;
        MedicalRecordController medicalRecordController;
        MergingRenovationController mergingRenovationController;
        PatientController patientController;
        PrescriptionController prescriptionController;
        ReferralController referralController;
        RoomController roomController;
        SimpleRenovationController simpleRenovationController;
        SplittingRenovationController splittingRenovationController;
        SupplyRequestController supplyRequestController;
        Person user;
        SecretaryController secretaryController;
        TransferController transferController;

        #region Property
        internal AnamnesisController AnamnesisController
        { get => anamnesisController; set => anamnesisController = value; }

        internal AppointmentController AppointmentController
        { get => appointmentController; set => appointmentController = value; }

        internal AppointmentRequestController AppointmentRequestController
        { get => appointmentRequestController; set => appointmentRequestController = value; }

        internal DaysOffNotificationController DaysOffNotificationController
        { get => daysOffNotificationController; set => daysOffNotificationController = value; }

        internal DaysOffRequestController DaysOffRequestController
        { get => daysOffRequestController; set => daysOffRequestController = value; }

        internal DelayedAppointmentNotificationController DelayedAppointmentNotificationController
        { get => delayedAppointmentNotificationController; set => delayedAppointmentNotificationController = value; }

        internal DoctorController DoctorController
        { get => doctorController; set => doctorController = value; }

        internal DoctorSurveyController DoctorSurveyController
        { get => doctorSurveyController; set => doctorSurveyController = value; }

        internal DrugController DrugController
        { get => drugController; set => drugController = value; }

        internal DrugNotificationController DrugNotificationController
        { get => drugNotificationController; set => drugNotificationController = value; }

        internal EquipmentController EquipmentController
        { get => equipmentController; set => equipmentController = value; }

        internal HospitalSurveyController HospitalSurveyController
        { get => hospitalSurveyController; set => hospitalSurveyController = value; }

        internal IngredientController IngredientController
        { get => ingredientController; set => ingredientController = value; }

        internal ManagerController ManagerController
        { get => managerController; set => managerController = value; }

        internal MedicalRecordController MedicalRecordController
        { get => medicalRecordController; set => medicalRecordController = value; }

        internal MergingRenovationController MergingRenovationController
        { get => mergingRenovationController; set => mergingRenovationController = value; }

        internal PatientController PatientController
        { get => patientController; set => patientController = value; }

        internal PrescriptionController PrescriptionController
        { get => prescriptionController; set => prescriptionController = value; }

        internal ReferralController ReferralController
        { get => referralController; set => referralController = value; }

        internal RoomController RoomController
        { get => roomController; set => roomController = value; }

        internal SimpleRenovationController SimpleRenovationController
        { get => simpleRenovationController; set => simpleRenovationController = value; }

        internal SplittingRenovationController SplittingRenovationController
        { get => splittingRenovationController; set => splittingRenovationController = value; }

        internal SupplyRequestController SupplyRequestController
        { get => supplyRequestController; set => supplyRequestController = value; }

        public Person User { get => user; set => user = value; }

        internal SecretaryController SecretaryController
        { get => secretaryController; set => secretaryController = value; }

        internal TransferController TransferController
        { get => transferController; set => transferController = value; }
        #endregion

        public HealthCareFactory()
        {
            anamnesisController = new();
            appointmentController = new();
            appointmentRequestController = new();
            daysOffNotificationController = new();
            daysOffRequestController = new();
            delayedAppointmentNotificationController = new();
            doctorController = new();
            doctorSurveyController = new();
            drugController = new();
            drugNotificationController = new();
            equipmentController = new();
            hospitalSurveyController = new();
            ingredientController = new();
            managerController = new();
            medicalRecordController = new();
            mergingRenovationController = new();
            patientController = new();
            prescriptionController = new();
            referralController = new();
            roomController = new();
            simpleRenovationController = new();
            splittingRenovationController = new();
            supplyRequestController = new();
            secretaryController = new();
            transferController = new();


            LinkDrugIngredient();
            LinkAppointmentRequest();
            LinkDaysOffNotification();
            LinkDaysOffRequest();
            LinkDelayedAppointmentNotification();

            LinkMedicalRecordPatient();
            LinkMedicalRecordIngrediant();
            LinkAppointment();
            LinkDoctorSurvey();
            LinkPrescription();
            LinkReferral();

            LinkRoomEquipment();
            LinkSimpleRenovationRoom();
            LinkSplittingRenovationRoom();
            LinkMergingRenovationRoom();
            LinkSupplyRequestEquipment();
            LinkTransfers();
        }
        #region Link
        public Person Login(string mail, string password)
        {
            foreach (Doctor doctor in doctorController.Doctors)
                if (doctor.Mail == mail && doctor.Password == password)
                    return doctor;

            foreach (Patient patient in patientController.Patients)
                if (patient.Mail == mail && patient.Password == password)
                {
                    appointmentRequestController.RunAntiTrollCheck(patient);
                    if (patient.Blocked)
                    {
                        MessageBox.Show("Account blocked. Contact secretary for more informations!");
                        return patient;
                    }
                    else
                    {

                        return patient;
                    }

                }
            foreach (Manager manager in managerController.Managers)
                if (manager.Mail == mail && manager.Password == password)
                    return manager;

            foreach (Secretary secretary in secretaryController.Secretaries)
                if (secretary.Mail == mail && secretary.Password == password)
                    return secretary;

            return null;
        }

        void LinkDrugIngredient(string path = "../../../data/links/Drug_Ingredient.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int drugId = Convert.ToInt32(line.Split(";")[0]);
                int ingredientId = Convert.ToInt32(line.Split(";")[1].Trim());

                Drug drug = drugController.FindById(drugId);
                Ingredient ingredient = ingredientController.FindById(ingredientId);

                drug.Ingredients.Add(ingredient);
            }

            file.Close();
        }

        void LinkAppointmentRequest(string path = "../../../data/links/AppointmentRequestLinker.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int requestId = Convert.ToInt32(line.Split(";")[0]);
                string patientId = line.Split(";")[1];
                int oldAppointmentId = Convert.ToInt32(line.Split(";")[2]);
                int newAppointmentId = Convert.ToInt32(line.Split(";")[3].Trim());


                AppointmentRequest request = appointmentRequestController.FindById(requestId);
                Patient patient = patientController.FindByJmbg(patientId);
                Appointment oldAppointment = appointmentController.FindById(oldAppointmentId);
                Appointment newAppointment = null;
                if (newAppointmentId != -1)
                {
                    appointmentController.FindById(newAppointmentId);
                }
                request.Patient = patient;
                request.OldAppointment = oldAppointment;
                request.NewAppointment = newAppointment;
            }

            file.Close();
        }

        void LinkDaysOffNotification(string path = "../../../data/links/Notification_Doctor.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationId = Convert.ToInt32(line.Split(";")[0]);
                string doctorId = line.Split(";")[1].Trim();

                DaysOffNotification notification = daysOffNotificationController.FindById(notificationId);
                Doctor doctor = doctorController.FindByJmbg(doctorId);

                notification.Doctor = doctor;
            }

            file.Close();
        }

        void LinkDaysOffRequest(string path = "../../../data/links/DaysOffRequest_Doctor.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int requestId = Convert.ToInt32(line.Split(";")[0]);
                string doctorId = line.Split(";")[1].Trim();

                DaysOffRequest request = daysOffRequestController.FindById(requestId);
                Doctor doctor = doctorController.FindByJmbg(doctorId);

                request.Doctor = doctor;
            }

            file.Close();
        }

        void LinkDelayedAppointmentNotification(string path = "../../../data/links/DelayedAppointmentNotificationLinker.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationtId = Convert.ToInt32(line.Split(";")[0]);
                int appointmentId = Convert.ToInt32(line.Split(";")[1].Trim());

                DelayedAppointmentNotification notification = delayedAppointmentNotificationController.FindById(notificationtId);
                Appointment appointment = appointmentController.FindById(appointmentId);

                notification.Appointment = appointment;
            }

            file.Close();
        }

        void LinkMedicalRecordPatient(string path = "../../../data/links/MedicalRecord_Patient.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int medicalRecordId = Convert.ToInt32(line.Split(";")[0]);
                string patientJmbg = line.Split(";")[1].Trim();

                MedicalRecord medicalRecord = medicalRecordController.FindById(medicalRecordId);
                Patient patient = patientController.FindByJmbg(patientJmbg);

                medicalRecord.Patient = patient;
                patient.MedicalRecord = medicalRecord;
            }

            file.Close();
        }

        void LinkAppointment(string path = "../../../data/links/AppointmentLinker.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int appointmentId = Convert.ToInt32(line.Split(";")[0]);
                string doctorJmbg = line.Split(";")[1];
                string patientJmbg = line.Split(";")[2];
                int roomId = Convert.ToInt32(line.Split(";")[3]);
                int anamnesisId = Convert.ToInt32(line.Split(";")[4].Trim());

                Appointment appointment = appointmentController.FindById(appointmentId);
                Doctor doctor = doctorController.FindByJmbg(doctorJmbg);
                Patient patient = patientController.FindByJmbg(patientJmbg);
                Room room;
                if (roomId == -1)
                {
                    room = null;
                }
                else
                {
                    room = roomController.FindById(roomId);
                }

                Anamnesis anamnesis = anamnesisController.FindById(anamnesisId);

                appointment.Doctor = doctor;
                appointment.Patient = patient;
                appointment.Room = room;
                appointment.Anamnesis = anamnesis;

                doctor.Appointments.Add(appointment);

                patient.MedicalRecord.Appointments.Add(appointment);
            }

            file.Close();
        }

        void LinkMedicalRecordIngrediant(string path = "../../../data/links/MedicalRecord_Ingredient.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int medicalRecordId = Convert.ToInt32(line.Split(";")[0]);
                int ingrediantId = Convert.ToInt32(line.Split(";")[1].Trim());

                MedicalRecord medicalRecord = medicalRecordController.FindById(medicalRecordId);
                Ingredient ingredient = ingredientController.FindById(ingrediantId);

                medicalRecord.Allergens.Add(ingredient);
            }

            file.Close();
        }

        void LinkDoctorSurvey(string path = "../../../data/links/Doctor_DoctorSurvey.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                string doctorJmbg = line.Split(";")[0];
                int surveyId = Convert.ToInt32(line.Split(";")[1].Trim());

                Doctor doctor = doctorController.FindByJmbg(doctorJmbg);
                DoctorSurvey doctorSurvey = doctorSurveyController.FindById(surveyId);

                doctorSurvey.Doctor = doctor;
            }

            file.Close();
        }

        void LinkPrescription(string path = "../../../data/links/PrescriptionLinker.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int prescriptionId = Convert.ToInt32(line.Split(";")[0]);
                int medicalRecordId = Convert.ToInt32(line.Split(";")[1]);
                int drugId = Convert.ToInt32(line.Split(";")[2].Trim());

                Prescription prescription = prescriptionController.FindById(prescriptionId);
                MedicalRecord medicalRecord = medicalRecordController.FindById(medicalRecordId);
                Drug drug = drugController.FindById(drugId);

                prescription.MedicalRecord = medicalRecord;
                prescription.Drug = drug;
                medicalRecord.Prescriptions.Add(prescription);

            }

            file.Close();
        }

        void LinkReferral(string path = "../../../data/links/ReferralLinker.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int referralId = Convert.ToInt32(line.Split(";")[0]);
                string doctorJmbg = line.Split(";")[1];
                int medicalRecordId = Convert.ToInt32(line.Split(";")[2].Trim());

                Referral referral = referralController.FindById(referralId);
                Doctor doctor = doctorController.FindByJmbg(doctorJmbg);
                MedicalRecord medicalRecord = medicalRecordController.FindById(medicalRecordId);

                referral.Doctor = doctor;
                referral.MedicalRecord = medicalRecord;
                medicalRecord.Referrals.Add(referral);
            }

            file.Close();
        }

        void LinkRoomEquipment(string path = "../../../data/links/Room_Equipment.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int roomId = Convert.ToInt32(line.Split(";")[0]);
                int equipmentId = Convert.ToInt32(line.Split(";")[1].Trim());
                int amount = Convert.ToInt32(line.Split(";")[2].Trim());

                Room room = this.roomController.FindById(roomId);
                Equipment equipment = equipmentController.FindById(equipmentId);

                room.EquipmentAmount[equipment] = amount;
            }

            file.Close();
        }

        void LinkSimpleRenovationRoom(string path = "../../../data/links/SimpleRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int simpleRenovationId = Convert.ToInt32(line.Split(";")[0].Trim());
                int roomId = Convert.ToInt32(line.Split(";")[1]);

                SimpleRenovation simpleRenovation = simpleRenovationController.FindById(simpleRenovationId);
                Room room = roomController.FindById(roomId);

                simpleRenovation.Room = room;
            }

            file.Close();
        }

        void LinkSplittingRenovationRoom(string path = "../../../data/links/SplittingRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int splittingRenovationId = Convert.ToInt32(line.Split(";")[0].Trim());
                int roomId = Convert.ToInt32(line.Split(";")[1]);

                SplittingRenovation splittingRenovation = splittingRenovationController.FindById(splittingRenovationId);
                Room room = roomController.FindById(roomId);

                splittingRenovation.Room = room;
            }

            file.Close();
        }

        void LinkMergingRenovationRoom(string path = "../../../data/links/MergingRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int mergingRenovationId = Convert.ToInt32(line.Split(";")[0]);
                int firstRoomId = Convert.ToInt32(line.Split(";")[1]);
                int secondRoomId = Convert.ToInt32(line.Split(";")[2].Trim());

                MergingRenovation mergingRenovation = mergingRenovationController.FindById(mergingRenovationId);
                Room firstRoom = roomController.FindById(firstRoomId);
                Room secondRoom = roomController.FindById(secondRoomId);

                mergingRenovation.Rooms.Add(firstRoom);
                mergingRenovation.Rooms.Add(secondRoom);
            }

            file.Close();
        }

        void LinkSupplyRequestEquipment(string path = "../../../data/links/SupplyRequest_Equipment.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int supplyRequestId = Convert.ToInt32(line.Split(";")[0]);
                int equipmentId = Convert.ToInt32(line.Split(";")[1].Trim());
                int amount = Convert.ToInt32(line.Split(";")[2].Trim());

                SupplyRequest supplyRequest = supplyRequestController.FindById(supplyRequestId);
                Equipment equipment = equipmentController.FindById(equipmentId);

                supplyRequest.OrderDetails[equipment] = amount;
            }

            file.Close();
        }

        void LinkTransfers(string path = "../../../data/links/TransferLinker.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int transferId = Convert.ToInt32(line.Split(";")[0]);
                int fromRoomId = Convert.ToInt32(line.Split(";")[1].Trim());
                int toRoomId = Convert.ToInt32(line.Split(";")[2].Trim());
                int equipmentId = Convert.ToInt32(line.Split(";")[3].Trim());

                Transfer transfer = transferController.FindById(transferId);
                Room fromRoom = roomController.FindById(fromRoomId);
                Room toRoom = roomController.FindById(toRoomId);
                Equipment equipment = equipmentController.FindById(equipmentId);

                transfer.FromRoom = fromRoom;
                transfer.ToRoom = toRoom;
                transfer.Equipment = equipment;
            }

            file.Close();
        }
        #endregion
        //+
        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {

            List<Room> rooms = new List<Room>();


            foreach (Room room in roomController.GetRoomsByType(type))
                if (IsRoomAvailableRenovationsAtTime(room, start))
                    rooms.Add(room);

            foreach (Appointment appointment in appointmentController.Appointments)
            {

                if (rooms.Contains(appointment.Room) && ((appointment.Start <= start && appointment.End >= start) ||
                    (appointment.Start <= end && appointment.End >= end) ||
                    (start <= appointment.Start && end >= appointment.End)))
                {
                    rooms.Remove(appointment.Room);
                }
            }

            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms[0];
        }

        //+
        public Appointment AddAppointment(Appointment appointment)
        {
            int anamnesisId = anamnesisController.GenerateId();
            Anamnesis anamnesis = new(anamnesisId, "");
            appointment.Anamnesis = anamnesis;

            Room room = AvailableRoom(appointment.Type, appointment.Start, appointment.End);
            appointment.Room = room;

            appointment.Validate();

            appointmentController.Appointments.Add(appointment);
            appointment.Doctor.Appointments.Add(appointment);
            appointment.Patient.MedicalRecord.Appointments.Add(appointment);
            anamnesisController.Anamneses.Add(appointment.Anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();

            return appointment;

        }
        //+
        public void UpdateAppointment(Appointment newAppointment)
        {
            newAppointment.Validate();
            Appointment appointment = appointmentController.FindById(newAppointment.Id);

            appointment.Start = newAppointment.Start;
            appointment.End = newAppointment.End;
            appointment.Doctor = newAppointment.Doctor;
            appointment.Patient = newAppointment.Patient;
            appointment.Status = newAppointment.Status;
            appointmentController.Serialize();

        }
        //+
        public void DeleteAppointment(int id)
        {
            Appointment appointment = appointmentController.FindById(id);
            if (appointment is null)
            {
                throw new Exception("Appointment is not found!");
            }
            appointmentController.Appointments.Remove(appointment);
            appointment.Doctor.Appointments.Remove(appointment);
            appointment.Patient.MedicalRecord.Appointments.Remove(appointment);
            anamnesisController.Anamneses.Remove(appointment.Anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();
        }
        //+
        public Appointment BookClosestEmergancyAppointment(List<Doctor> doctors, Patient patient, int duration)
        {
            DateTime limitTime = DateTime.Now.AddHours(2);
            DateTime start = limitTime;
            DateTime closestTimeForDoctor;
            Doctor doctor = doctors[0];
            foreach (Doctor doc in doctors)
            {
                closestTimeForDoctor = doc.getClosestFreeAppointment(duration, patient);
                if (closestTimeForDoctor < start)
                {
                    start = closestTimeForDoctor;
                    doctor = doc;
                }
            }

            if (limitTime == start)
            {
                return null;
            }
            AppointmentType type = Appointment.getTypeByDuration(duration);

            int id = appointmentController.GenerateId();
            Appointment appointment = new Appointment(id, start, start.AddMinutes(duration), type, AppointmentStatus.BOOKED, false, true);
            appointment.Doctor = doctor;
            return appointment;
        }
        //+
        public Appointment BookAppointmentByReferral(Referral referral)
        {
            Doctor doctor = referral.Doctor;
            if (doctor is null)
            {
                doctor = doctorController.FindBySpecialization(referral.Specialization)[0];
            }

            DateTime closestTimeForDoctor = doctor.getClosestFreeAppointment(15, referral.MedicalRecord.Patient);

            referral.Used = true;
            referralController.Serialize();

            int id = appointmentController.GenerateId();
            Appointment appointment = new(id, closestTimeForDoctor, closestTimeForDoctor.AddMinutes(15), doctor,
                referral.MedicalRecord.Patient, null, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
            return AddAppointment(appointment);

        }
        //+
        public void AddNotification(Appointment appointment, DateTime oldStart)
        {
            string text = "Your appointment booked for " + oldStart + " is delayed. New start is on: " + appointment.Start + ".";
            DelayedAppointmentNotification newNotification = delayedAppointmentNotificationController.Add(appointment, text);
            delayedAppointmentNotificationController.Serialize();
        }
        //+
        public void AcceptRequest(AppointmentRequest request)
        {
            if (request.Type == RequestType.DELETE)
            {
                appointmentController.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
            }
            appointmentController.Appointments.Remove(request.OldAppointment);
            request.OldAppointment = null;
            request.State = AppointmentState.ACCEPTED;
            appointmentController.Serialize();
            appointmentRequestController.Serialize();
        }
        //++
        public void RejectRequest(AppointmentRequest request)
        {
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentController.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
                appointmentController.Serialize();
            }
            AppointmentRequestController.Serialize();
        }
        //++
        private void DeleteAppointmens(Patient patient)
        {
            for (int i = appointmentController.Appointments.Count - 1; i >= 0; i--)
            {
                if (appointmentController.Appointments[i].Patient == patient)
                {
                    if (appointmentController.Appointments[i].Start > DateTime.Now)
                    {
                        throw new Exception("Can't delete selected patient, because of it's future appointments.");
                    }
                    DeleteAppointment(appointmentController.Appointments[i].Id);
                }
            }
        }
        //++
        private void DeletePrescriptions(MedicalRecord medicalRecord)
        {
            for (int i = prescriptionController.Prescriptions.Count - 1; i >= 0; i--)
            {
                if (prescriptionController.Prescriptions[i].MedicalRecord == medicalRecord)
                {
                    prescriptionController.Prescriptions.RemoveAt(i);
                }
            }
            prescriptionController.Serialize();
        }
        //++
        public void DeletePatient(Patient patient)
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;

            try
            {
                DeleteAppointmens(patient);
            }
            catch
            {
                throw;
            }
            DeletePrescriptions(medicalRecord);

            medicalRecordController.MedicalRecords.Remove(medicalRecord);
            medicalRecordController.Serialize();

            patientController.Patients.Remove(patient);
            patientController.Serialize();

        }
        //++
        public void AddPatient(Patient patient, MedicalRecord medRecord)
        {
            patientController.Patients.Add(patient);

            patient.MedicalRecord = medRecord;
            medRecord.Patient = patient;

            patientController.Serialize();
            medicalRecordController.Serialize();
            ingredientController.Serialize();
        }
        //++
        public void UpdatePatient()
        {
            patientController.Serialize();
            medicalRecordController.Serialize();
        }
        //++
        public void TryToExecuteSupplyRequest()
        {
            Room storage = roomController.GetStorage();
            foreach (SupplyRequest supplyRequest in supplyRequestController.SupplyRequests)
            {
                if (supplyRequest.Finished == false && DateTime.Now < supplyRequest.RequestCreated.AddDays(1))
                {
                    foreach (Equipment equipment in supplyRequest.OrderDetails.Keys)
                    {
                        storage.EquipmentAmount[equipment] += supplyRequest.OrderDetails[equipment];
                    }
                }
            }
        }
        //++
        public void AddSupplyRequest(Equipment equipment, int quantity)
        {
            SupplyRequest supplyRequest = new SupplyRequest(supplyRequestController.GenerateId(), equipment, quantity);
            supplyRequestController.SupplyRequests.Add(supplyRequest);
            supplyRequestController.Serialize();
        }
        //++
        public void ApplyEquipmentFilters(string roomType, string amount, string equipmentType,
            Dictionary<Equipment, int> equipmentAmount)
        {
            if (roomType != "All")
            {
                roomController.RoomTypeFilter(roomType, equipmentAmount);
            }

            if (amount != "All")
            {
                equipmentController.AmountFilter(amount, equipmentAmount);
            }

            if (equipmentType != "All")
            {
                equipmentController.EquipmentTypeFilter(equipmentType, equipmentAmount);
            }
        }
        //++
        public void ExecuteTransfer(Transfer transfer)
        {
            roomController.MoveFromRoom(transfer.FromRoom, transfer.Equipment, transfer.Amount);
            roomController.MoveToRoom(transfer.ToRoom, transfer.Equipment, transfer.Amount);
            roomController.Serialize();
            transferController.Transfers.Remove(transfer);
            transferController.Serialize();
        }
        //++
        public bool IsRoomAvailableForChange(Room room)
        {
            bool available = true;

            available = IsRoomAvailableAppointments(room);
            if (!available)
            {
                return available;
            }

            available = transferController.IsRoomAvailable(room);
            if (!available)
            {
                return available;
            }

            return available;
        }
        //++
        public void RemoveRoom(Room room)
        {
            foreach (Appointment appointment in appointmentController.Appointments)
            {
                if (appointment.Room == room)
                    appointment.Room = null;
            }
            appointmentController.Serialize();
            roomController.DeleteRoom(room);
        }
        //++
        public void AddPrescrition(Prescription prescription)
        {
            MedicalRecord medicalRecord = medicalRecordController.FindById(prescription.MedicalRecord.Id);
            medicalRecord.ValidatePrescription(prescription);
            medicalRecord.Prescriptions.Add(prescription);

            prescriptionController.Prescriptions.Add(prescription);
            prescriptionController.Serialize();
        }
        public bool IsRoomAvailableAppointments(Room room)
        {
            bool available = true;
            foreach (Appointment appointment in appointmentController.Appointments)
            {
                if (room == appointment.Room && appointment.Status != AppointmentStatus.FINISHED)
                {
                    available = false;
                    break;
                }
            }
            return available;
        }
        //+
        public bool IsRoomAvailableRenovationsAtAll(Room room)
        {
            bool available = true;
            available = simpleRenovationController.IsRoomAvailableAtAll(room);
            if (!available)
            {
                return available;
            }

            available = mergingRenovationController.IsRoomAvailableAtAll(room);
            if (!available)
            {
                return available;
            }

            available = splittingRenovationController.IsRoomAvailableAtAll(room);
            if (!available)
            {
                return available;
            }
            return available;
        }
        //++
        public bool IsRoomAvailableRenovationsAtTime(Room room, DateTime time)
        {
            bool available = true;
            available = simpleRenovationController.IsRoomAvailableAtTime(room, time);
            if (!available)
            {
                return available;
            }

            available = mergingRenovationController.IsRoomAvailableAtTime(room, time);
            if (!available)
            {
                return available;
            }

            available = splittingRenovationController.IsRoomAvailableAtTime(room, time);
            if (!available)
            {
                return available;
            }
            return available;
        }
        //+
        public Dictionary<Equipment, int> InitalizeEquipment()
        {
            Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
            foreach (Equipment equipment in EquipmentController.Equipment)
            {
                equipmentAmount[equipment] = 0;
            }
            return equipmentAmount;
        }
        //++
        public void StartSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.ACTIVE;
            simpleRenovationController.Serialize();
            roomController.MoveEquipmentToStorage(simpleRenovation.Room);
            roomController.Serialize();
        }
        //++
        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.FINISHED;
            roomController.UpdateRoom(simpleRenovation.Room, simpleRenovation.NewRoomName, simpleRenovation.NewRoomType);
            simpleRenovationController.SimpleRenovations.Remove(simpleRenovation);
            simpleRenovationController.Serialize();
        }
        //++
        public void StartMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            mergingRenovationController.Serialize();
            foreach (Room room in mergingRenovation.Rooms)
            {
                roomController.MoveEquipmentToStorage(room);
            }
            roomController.Serialize();
        }
        //++
        public void FinishMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            foreach (Room room in mergingRenovation.Rooms)
            {
                RemoveRoom(room);
            }
            Dictionary<Equipment, int> equipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(mergingRenovation.NewRoomName, mergingRenovation.NewRoomType, equipmentAmount);
            mergingRenovationController.MergingRenovations.Remove(mergingRenovation);
            mergingRenovationController.Serialize();
        }
        //++
        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.ACTIVE;
            splittingRenovationController.Serialize();
            roomController.MoveEquipmentToStorage(splittingRenovation.Room);
            roomController.Serialize();
        }
        //++
        public void FinishSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovation.Status = RenovationStatus.FINISHED;
            RemoveRoom(splittingRenovation.Room);
            Dictionary<Equipment, int> firstRoomEquipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(splittingRenovation.FirstNewRoomName, splittingRenovation.FirstNewRoomType,
                firstRoomEquipmentAmount);
            Dictionary<Equipment, int> secondRoomEquipmentAmount = InitalizeEquipment();
            roomController.CreateNewRoom(splittingRenovation.SecondNewRoomName, splittingRenovation.SecondNewRoomType,
                secondRoomEquipmentAmount);
            splittingRenovationController.SplittingRenovations.Remove(splittingRenovation);
            splittingRenovationController.Serialize();
        }

        public void TryToExecuteSimpleRenovations()
        {
            if (simpleRenovationController.SimpleRenovations.Count > 0)
            {
                foreach (SimpleRenovation simpleRenovation in simpleRenovationController.SimpleRenovations)
                {
                    if (DateTime.Now >= simpleRenovation.EndingDate)
                    {
                        FinishSimpleRenovation(simpleRenovation);
                        return;
                    }

                    if (DateTime.Now >= simpleRenovation.BeginningDate &&
                        simpleRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartSimpleRenovation(simpleRenovation);
                        return;
                    }
                }
            }

        }

        public void TryToExecuteMergingRenovations()
        {
            if (mergingRenovationController.MergingRenovations.Count > 0)
            {
                foreach (MergingRenovation mergingRenovation in mergingRenovationController.MergingRenovations)
                {
                    if (DateTime.Now >= mergingRenovation.EndingDate)
                    {
                        FinishMergingRenovation(mergingRenovation);
                        return;
                    }

                    if (DateTime.Now >= mergingRenovation.BeginningDate &&
                        mergingRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartMergingRenovation(mergingRenovation);
                        return;
                    }
                }
            }

        }

        public void TryToExecuteSplittingRenovations()
        {
            if (splittingRenovationController.SplittingRenovations.Count > 0)
            {
                foreach (SplittingRenovation splittingRenovation in splittingRenovationController.SplittingRenovations)
                {
                    if (DateTime.Now >= splittingRenovation.EndingDate)
                    {
                        FinishSplittingRenovation(splittingRenovation);
                        return;
                    }

                    if (DateTime.Now >= splittingRenovation.BeginningDate &&
                        splittingRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartSplittingRenovation(splittingRenovation);
                        return;
                    }
                }
            }

        }
        //++
        private List<Appointment> SearchDoubleCriterium(DateTime end, int[] from, int[] to, Doctor doctor)
        {
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date.AddHours(from[0]).AddMinutes(from[1]);
            DateTime todayEnd = DateTime.Now.Date.AddHours(to[0]).AddMinutes(to[1]);
            DateTime date = todayStart;

            int id = appointmentController.GenerateId();

            if (DateTime.Now > todayStart && DateTime.Now < todayEnd)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);
            else if (DateTime.Now > todayEnd)
                date = date.AddDays(1);

            while (date.Date <= end.Date)
            {
                while (date < todayEnd)
                {
                    try
                    {
                        Room room = AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                        Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                        (Patient)user, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);

                        appointment.Validate();
                        appointments.Add(appointment);
                        return appointments;
                    }
                    catch
                    {
                    }
                    date = date.AddMinutes(1);
                }
                todayStart = todayStart.AddDays(1);
                todayEnd = todayEnd.AddDays(1);
                date = todayStart;
            }
            return null;
        }
        //++
        private List<Appointment> SearchPriorityDoctor(DateTime end, Doctor doctor)
        {
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date;
            DateTime date = todayStart;

            int id = appointmentController.GenerateId();
            if (DateTime.Now > todayStart)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);

            while (date <= end)
            {
                try
                {
                    Room room = AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                    Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                    (Patient)user, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);

                    appointment.Validate();
                    appointments.Add(appointment);
                    return appointments;
                }
                catch
                {
                }
                date = date.AddMinutes(1);
            }
            return null;
        }
        //++
        private List<Appointment> SearchPriorityDate(DateTime end, int[] from, int[] to)
        {
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date.AddHours(from[0]).AddMinutes(from[1]);
            DateTime todayEnd = DateTime.Now.Date.AddHours(to[0]).AddMinutes(to[1]);
            DateTime date = todayStart;

            int id = appointmentController.GenerateId();

            if (DateTime.Now > todayStart && DateTime.Now < todayEnd)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);
            else if (DateTime.Now > todayEnd)
                date = date.AddDays(1);
            List<Doctor> doctors = doctorController.FindBySpecialization(Specialization.GENERAL);

            foreach (Doctor doctor in doctors)
            {
                while (date.Date <= end.Date)
                {
                    while (date < todayEnd)
                    {
                        try
                        {
                            Room room = AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                            Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                            (Patient)user, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);

                            appointment.Validate();
                            appointments.Add(appointment);
                            return appointments;
                        }
                        catch { }
                        date = date.AddMinutes(1);
                    }
                    todayStart = todayStart.AddDays(1);
                    todayEnd = todayEnd.AddDays(1);
                    date = todayStart;
                }
            }
            return null;

        }
        //++
        public List<Appointment> SearchNoPriority(DateTime end, int[] from, int[] to)
        {
            DateTime todayStart = end.AddDays(1);
            DateTime date = todayStart;
            int id = appointmentController.GenerateId();
            List<Doctor> doctors = doctorController.FindBySpecialization(Specialization.GENERAL);
            List<Appointment> appointments = new();
            while (true)
            {
                foreach (Doctor doctor in doctors)
                {
                    try
                    {
                        Room room = AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                        Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                        (Patient)user, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
                        appointment.Validate();
                        appointments.Add(appointment);
                        date = date.AddMinutes(14);
                        if (appointments.Count == 3)
                            return appointments;
                    }
                    catch
                    {
                    }


                    date = date.AddMinutes(1);
                }
            }

        }
        //++
        public List<Appointment> RecommendAppointment(DateTime end, int[] from, int[] to, Doctor doctor, bool priorityDoctor)
        {
            List<Appointment> appointments = SearchDoubleCriterium(end, from, to, doctor);
            if (appointments != null)
                return appointments;
            if (priorityDoctor)
            {
                appointments = SearchPriorityDoctor(end, doctor);
                if (appointments != null)
                    return appointments;
                appointments = SearchPriorityDate(end, from, to);
                if (appointments != null)
                    return appointments;
            }
            else
            {
                appointments = SearchPriorityDate(end, from, to);
                if (appointments != null)
                    return appointments;
                appointments = SearchPriorityDoctor(end, doctor);
                if (appointments != null)
                    return appointments;

            }
            return SearchNoPriority(end, from, to);

        }
        //++
        public List<Doctor> SortDoctorsByRatings(List<Doctor> doctors, SortDirection direction)
        {
            List<Tuple<Doctor, double>> ratings = new();
            foreach (Doctor doctor in doctors)
            {
                ratings.Add(new Tuple<Doctor, double>(doctor, doctorSurveyController.FindAverageRatingForDoctor(doctor)));
            }

            List<Tuple<Doctor, double>> sortedRatings;
            if (direction == SortDirection.DESCENDING)
                sortedRatings = ratings.OrderByDescending(t => t.Item2).ToList();
            else
                sortedRatings = ratings.OrderBy(t => t.Item2).ToList();
            List<Doctor> sortedDoctors = new();
            foreach (Tuple<Doctor, double> tuple in sortedRatings)
            {
                sortedDoctors.Add(tuple.Item1);
            }
            return sortedDoctors;
        }
        //++
        public List<Doctor> SortDoctors(List<Doctor> doctors, DoctorSortPriority priority, SortDirection direction)
        {
            List<Doctor> sortedDoctors = new();
            if (priority == DoctorSortPriority.RATINGS)
                sortedDoctors = SortDoctorsByRatings(doctors, direction);
            else if (priority == DoctorSortPriority.FIRST_NAME)
                sortedDoctors = doctorController.SortDoctorsByFirstName(doctors, direction);
            else if (priority == DoctorSortPriority.LAST_NAME)
                sortedDoctors = doctorController.SortDoctorsByLastName(doctors, direction);
            else
                sortedDoctors = doctorController.SortDoctorsBySpecialization(doctors, direction);
            return sortedDoctors;
        }
        //++
        public bool IsIngredientAvailableForChange(Ingredient ingredient)
        {
            bool available = true;

            foreach (Drug drug in drugController.Drugs)
            {
                if (drug.Ingredients.Contains(ingredient))
                {
                    available = false;
                    break;
                }
            }

            return available;
        }
        //++
        public bool IsDrugAvailableForChange(Drug drug)
        {
            bool available = true;

            foreach (Prescription prescription in prescriptionController.Prescriptions)
            {
                if (prescription.Drug == drug)
                {
                    available = false;
                    break;
                }
            }

            return available;


        }

    }
}
