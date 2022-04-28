using HealthCare_System.controllers;
using HealthCare_System.entities;
using HealthCare_System.gui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Collections.Generic;

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

        internal AnamnesisController AnamnesisController { get => anamnesisController; set => anamnesisController = value; }
        internal AppointmentController AppointmentController { get => appointmentController; set => appointmentController = value; }
        internal AppointmentRequestController AppointmentRequestController { get => appointmentRequestController; set => appointmentRequestController = value; }
        internal DaysOffNotificationController DaysOffNotificationController { get => daysOffNotificationController; set => daysOffNotificationController = value; }
        internal DaysOffRequestController DaysOffRequestController { get => daysOffRequestController; set => daysOffRequestController = value; }
        internal DelayedAppointmentNotificationController DelayedAppointmentNotificationController { get => delayedAppointmentNotificationController; set => delayedAppointmentNotificationController = value; }
        internal DoctorController DoctorController { get => doctorController; set => doctorController = value; }
        internal DoctorSurveyController DoctorSurveyController { get => doctorSurveyController; set => doctorSurveyController = value; }
        internal DrugController DrugController { get => drugController; set => drugController = value; }
        internal DrugNotificationController DrugNotificationController { get => drugNotificationController; set => drugNotificationController = value; }
        internal EquipmentController EquipmentController { get => equipmentController; set => equipmentController = value; }
        internal HospitalSurveyController HospitalSurveyController { get => hospitalSurveyController; set => hospitalSurveyController = value; }
        internal IngredientController IngredientController { get => ingredientController; set => ingredientController = value; }
        internal ManagerController ManagerController { get => managerController; set => managerController = value; }
        internal MedicalRecordController MedicalRecordController { get => medicalRecordController; set => medicalRecordController = value; }
        internal MergingRenovationController MergingRenovationController { get => mergingRenovationController; set => mergingRenovationController = value; }
        internal PatientController PatientController { get => patientController; set => patientController = value; }
        internal PrescriptionController PrescriptionController { get => prescriptionController; set => prescriptionController = value; }
        internal ReferralController ReferralController { get => referralController; set => referralController = value; }
        internal RoomController RoomController { get => roomController; set => roomController = value; }
        internal SimpleRenovationController SimpleRenovationController { get => simpleRenovationController; set => simpleRenovationController = value; }
        internal SplittingRenovationController SplittingRenovationController { get => splittingRenovationController; set => splittingRenovationController = value; }
        internal SupplyRequestController SupplyRequestController { get => supplyRequestController; set => supplyRequestController = value; }
        public Person User { get => user; set => user = value; }
        internal SecretaryController SecretaryController{ get => secretaryController; set => secretaryController = value; }

        

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

            LinkDrugIngredient();
            LinkDrugNotification();
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
        }


        //TODO add the rest of user types
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
                        return null;
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
        
        void LinkDrugNotification(string path = "data/links/Notification_Patient_Drug.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationId = Convert.ToInt32(line.Split(";")[0]);
                string patientId = line.Split(";")[1];
                int drugId = Convert.ToInt32(line.Split(";")[2].Trim());

                DrugNotification notification = this.drugNotificationController.FindById(notificationId);
                Patient patient = this.patientController.FindByJmbg(patientId);
                Drug drug = this.drugController.FindById(drugId);

                notification.Drug=drug;
                notification.Patient = patient;
            }

            file.Close();
        }

        void LinkDrugIngredient(string path = "data/links/Drug_Ingredient.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream) 
            {
                string line = file.ReadLine();
                int drugId = Convert.ToInt32(line.Split(";")[0]);
                int ingredientId = Convert.ToInt32(line.Split(";")[1].Trim());

                Drug drug = this.drugController.FindById(drugId);
                Ingredient ingredient = this.ingredientController.FindById(ingredientId);

                drug.Ingredients.Add(ingredient);
            }

            file.Close();
        }

        void LinkAppointmentRequest(string path = "data/links/AppointmentRequestLinker.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int requestId = Convert.ToInt32(line.Split(";")[0]);
                string patientId = line.Split(";")[1];
                int appointmentId = Convert.ToInt32(line.Split(";")[2].Trim());

                AppointmentRequest request = this.appointmentRequestController.FindById(requestId);
                Patient patient = this.patientController.FindByJmbg(patientId);
                Appointment appointment = this.appointmentController.FindById(appointmentId);

                request.Patient = patient;
                request.Appointment = appointment;
            }

            file.Close();
        }

        void LinkDaysOffNotification(string path = "data/links/Notification_Doctor.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationId = Convert.ToInt32(line.Split(";")[0]);
                string doctorId = line.Split(";")[1].Trim();

                DaysOffNotification notification = this.daysOffNotificationController.FindById(notificationId);
                Doctor doctor = this.doctorController.FindByJmbg(doctorId);

                notification.Doctor = doctor;
            }

            file.Close();
        }

        void LinkDaysOffRequest(string path = "data/links/DaysOffRequest_Doctor.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int requestId= Convert.ToInt32(line.Split(";")[0]);
                string doctorId = line.Split(";")[1].Trim();

                DaysOffRequest request= this.daysOffRequestController.FindById(requestId);
                Doctor doctor = this.doctorController.FindByJmbg(doctorId);

                request.Doctor = doctor;
            }

            file.Close();
        }

        void LinkDelayedAppointmentNotification(string path = "data/links/DelayedAppointmentNotificationLinker.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationtId = Convert.ToInt32(line.Split(";")[0]);
                int appointmentId = Convert.ToInt32(line.Split(";")[1].Trim());

                DelayedAppointmentNotification notification= this.delayedAppointmentNotificationController.FindById(notificationtId);
                Appointment appointment = this.appointmentController.FindById(appointmentId);

                notification.Appointment = appointment;
            }

            file.Close();
        }

        void LinkMedicalRecordPatient(string path = "data/links/MedicalRecord_Patient.csv")
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

        void LinkAppointment(string path = "data/links/AppointmentLinker.csv")
        {
            StreamReader file = new(path);
            while(!file.EndOfStream)
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
                Room room = roomController.FindById(roomId);
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

        void LinkMedicalRecordIngrediant(string path = "data/links/MedicalRecord_Ingredient.csv")
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

        void LinkDoctorSurvey(string path = "data/links/Doctor_DoctorSurvey.csv")
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

        void LinkPrescription(string path = "data/links/PrescriptionLinker.csv")
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

        void LinkReferral(string path = "data/links/ReferralLinker.csv")
        {
            StreamReader file = new(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int referralId = Convert.ToInt32(line.Split(";")[0]);
                string doctorJmbg = line.Split(";")[1];
                string patientJmbg = line.Split(";")[2].Trim();

                Referral referral = referralController.FindById(referralId);
                Doctor doctor = doctorController.FindByJmbg(doctorJmbg);
                Patient patient = patientController.FindByJmbg(patientJmbg);

                referral.Doctor = doctor;
                referral.Patient = patient;
            }

            file.Close();
        }

        void LinkRoomEquipment(string path = "data/links/Room_Equipment.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int roomId = Convert.ToInt32(line.Split(";")[0]);
                int equipmentId = Convert.ToInt32(line.Split(";")[1].Trim());
                int amount = Convert.ToInt32(line.Split(";")[2].Trim());

                Room room = this.roomController.FindById(roomId);
                Equipment equipment = this.equipmentController.FindById(equipmentId);

                room.EquipmentAmount[equipment] = amount;
            }

            file.Close();
        }

        void LinkSimpleRenovationRoom(string path = "data/links/SimpleRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int simpleRenovationId = Convert.ToInt32(line.Split(";")[0].Trim());
                int roomId = Convert.ToInt32(line.Split(";")[1]);

                SimpleRenovation simpleRenovation = this.simpleRenovationController.FindById(simpleRenovationId);
                Room room = this.roomController.FindById(roomId);

                simpleRenovation.Room = room;
            }

            file.Close();
        }

        void LinkSplittingRenovationRoom(string path = "data/links/SplittingRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int splittingRenovationId = Convert.ToInt32(line.Split(";")[0].Trim());
                int roomId = Convert.ToInt32(line.Split(";")[1]);

                SplittingRenovation splittingRenovation = this.splittingRenovationController.FindById(splittingRenovationId);
                Room room = this.roomController.FindById(roomId);

                splittingRenovation.Room = room;
            }

            file.Close();
        }

        void LinkMergingRenovationRoom(string path = "data/links/MergingRenovation_Room.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int mergingRenovationId = Convert.ToInt32(line.Split(";")[0]);
                int firstRoomId = Convert.ToInt32(line.Split(";")[1]);
                int secondRoomId = Convert.ToInt32(line.Split(";")[2].Trim());

                MergingRenovation mergingRenovation = this.mergingRenovationController.FindById(mergingRenovationId);
                Room firstRoom = this.roomController.FindById(firstRoomId);
                Room secondRoom = this.roomController.FindById(secondRoomId);

                mergingRenovation.Rooms.Add(firstRoom);
                mergingRenovation.Rooms.Add(secondRoom);
            }

            file.Close();
        }

        void LinkSupplyRequestEquipment(string path = "data/links/SupplyRequest_Equipment.csv")
        {
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int supplyRequestId = Convert.ToInt32(line.Split(";")[0]);
                int equipmentId = Convert.ToInt32(line.Split(";")[1].Trim());
                int amount = Convert.ToInt32(line.Split(";")[2].Trim());

                SupplyRequest supplyRequest = this.supplyRequestController.FindById(supplyRequestId);
                Equipment equipment = this.equipmentController.FindById(equipmentId);

                supplyRequest.OrderDetails[equipment] = amount;
            }

            file.Close();
        }

        public void PrintContnent()
        {
            Console.WriteLine("Anamneses:");
            foreach (Anamnesis anamnesis in anamnesisController.Anamneses)
                Console.WriteLine(anamnesis.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Appointments:");
            foreach (Appointment appointment in appointmentController.Appointments)
                Console.WriteLine(appointment.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("AppointmentRequests:");
            foreach (AppointmentRequest appointmentRequest in appointmentRequestController.AppointmentRequests)
                Console.WriteLine(appointmentRequest.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("DaysOffNotifications:");
            foreach (DaysOffNotification daysOffNotification in daysOffNotificationController.DaysOffNotifications)
                Console.WriteLine(daysOffNotification.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("DaysOffRequests:");
            foreach (DaysOffRequest daysOffRequest in daysOffRequestController.DaysOffRequests)
                Console.WriteLine(daysOffRequest.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("DelayedAppointmentNotification:");
            foreach (DelayedAppointmentNotification delayedAppointmentNotification in 
                delayedAppointmentNotificationController.DelayedAppointmentNotifications)
                Console.WriteLine(delayedAppointmentNotification.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Doctors:");
            foreach (Doctor doctor in doctorController.Doctors)
                Console.WriteLine(doctor.ToString() + " appointments:" + doctor.Appointments.Count);
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("DoctorSurvey:");
            foreach (DoctorSurvey doctorSurvey in doctorSurveyController.DoctorSurveys)
                Console.WriteLine(doctorSurvey.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Drug:");
            foreach (Drug drug in drugController.Drugs)
                Console.WriteLine(drug.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("DrugNotification:");
            foreach (DrugNotification drugNotification in drugNotificationController.DrugNotifications)
                Console.WriteLine(drugNotification.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Equipment:");
            foreach (Equipment equipment in equipmentController.Equipment)
                Console.WriteLine(equipment.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("HospitalSurvey:");
            foreach (HospitalSurvey hospitalSurvey in hospitalSurveyController.HospitalSurveys)
                Console.WriteLine(hospitalSurvey.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Ingredient:");
            foreach (Ingredient ingredient in ingredientController.Ingredients)
                Console.WriteLine(ingredient.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Manager:");
            foreach (Manager manager in managerController.Managers)
                Console.WriteLine(manager.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("MedicalRecord:");
            foreach (MedicalRecord medicalRecord in medicalRecordController.MedicalRecords)
                Console.WriteLine(medicalRecord.ToString() + " appointments: " + medicalRecord.Appointments.Count);
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("MergintRenovations:");
            foreach (MergingRenovation mergingRenovation in mergingRenovationController.MergingRenovations)
                Console.WriteLine(mergingRenovation.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Patient:");
            foreach (Patient patient in patientController.Patients)
                Console.WriteLine(patient.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Prescription:");
            foreach (Prescription prescription in prescriptionController.Prescriptions)
                Console.WriteLine(prescription.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Refferal:");
            foreach (Referral referral in referralController.Referrals)
                Console.WriteLine(referral.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Room:");
            foreach (Room room in roomController.Rooms)
                Console.WriteLine(room.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("SimpleRenovation:");
            foreach (SimpleRenovation simpleRenovation in simpleRenovationController.SimpleRenovations)
                Console.WriteLine(simpleRenovation.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("SplittingRenovation:");
            foreach (SplittingRenovation splittingRenovation in splittingRenovationController.SplittingRenovations)
                Console.WriteLine(splittingRenovation.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("SupplyRequest:");
            foreach (SupplyRequest supplyRequest in supplyRequestController.SupplyRequests)
                Console.WriteLine(supplyRequest.ToString());
            Console.WriteLine("-------------------------------------------");
        }

        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {

            List<Room> rooms = roomController.GetRoomsByType(type);
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

        public Appointment AddAppointment(DateTime start, DateTime end, Doctor doctor, Patient patient, AppointmentType type, AppointmentStatus status, bool emergency)
        {
            Room room = AvailableRoom(type, start, end);
            if (!doctor.IsAvailable(start, end))
            {
                throw new Exception("Doctor is not available!");
            }
            if (!patient.IsAvailable(start, end))
            {
                throw new Exception("Patient is not available!");
            }
            if (room is null)
            {
                throw new Exception("Room is not found!");
            }
            int appointmentId = appointmentController.GenerateId();
            int anamnesisId = anamnesisController.GenerateId();
            Anamnesis anamnesis = new Anamnesis(anamnesisId, "");
            Appointment appointment = new Appointment(appointmentId, start, end, doctor, patient, room, type, status, anamnesis, false, emergency);
            appointmentController.Appointments.Add(appointment);
            doctor.Appointments.Add(appointment);
            patient.MedicalRecord.Appointments.Add(appointment);
            anamnesisController.Anamneses.Add(anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();
            return appointment;

        }

        public Appointment AddAppointment(Appointment appointment)
        {
            Room room = AvailableRoom(appointment.Type, appointment.Start, appointment.End);
            if (!appointment.Doctor.IsAvailable(appointment.Start, appointment.End))
            {
                throw new Exception("Doctor is not available!");
            }
            if (!appointment.Patient.IsAvailable(appointment.Start, appointment.End))
            {
                throw new Exception("Patient is not available!");
            }
            if (room is null)
            {
                throw new Exception("Room is not found!");
            }
            int anamnesisId = anamnesisController.GenerateId();
            Anamnesis anamnesis = new Anamnesis(anamnesisId, "");
            appointment.Room = room;
            appointment.Anamnesis = anamnesis;
            appointmentController.Appointments.Add(appointment);
            appointment.Doctor.Appointments.Add(appointment);
            appointment.Patient.MedicalRecord.Appointments.Add(appointment);
            anamnesisController.Anamneses.Add(anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();
            return appointment;

        }

        public void UpdateAppointment(int id, DateTime start, DateTime end, Doctor doctor, Patient patient,AppointmentStatus status)
        {
            Appointment appointment = appointmentController.FindById(id);
            if (appointment is null)
            {
                throw new Exception("Appointment is not found!");
            }
            if (doctor.Specialization != appointment.Doctor.Specialization)
            {
                throw new Exception("Cannot choose doctor with different specialization!");
            }
            if (!doctor.IsAvailable(start, end))
            {
                throw new Exception("Doctor is not available!");
            }
            if (!patient.IsAvailable(start, end))
            {
                throw new Exception("Patient is not available!");
            }
            appointment.Start = start;
            appointment.End = end;
            appointment.Doctor = doctor;
            appointment.Patient = patient;
            appointment.Status = status;
            appointmentController.Serialize();

        }
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
        //Did this in filtering
        public void ApplyEquipmentFilters(string roomType, string amount, string equipmentType, Dictionary<Equipment, int> equipmentAmount) 
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

        
    }
}
