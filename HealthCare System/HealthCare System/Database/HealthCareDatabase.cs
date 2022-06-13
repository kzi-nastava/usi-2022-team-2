using HealthCare_System.Core.Anamneses.Model;
using HealthCare_System.Core.Anamneses.Repository;
using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Appointments.Repository;
using HealthCare_System.Core.AppotinmentRequests.Repository;
using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.DaysOffRequests.Repository;
using HealthCare_System.Core.DoctorSurveys.Model;
using HealthCare_System.Core.DoctorSurveys.Repository;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Drugs.Repository;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Equipments.Repository;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.EquipmentTransfers.Repository;
using HealthCare_System.Core.HospitalSurveys.Repository;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.Ingredients.Repository;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.MedicalRecords.Repository;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Notifications.Repository;
using HealthCare_System.Core.Prescriptions.Model;
using HealthCare_System.Core.Prescriptions.Repository;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Referrals.Repository;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Rooms.Repository;
using HealthCare_System.Core.SupplyRequests.Model;
using HealthCare_System.Core.SupplyRequests.Repository;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using HealthCare_SystemCore.Core.Users.Repository;
using System;
using System.IO;

namespace HealthCare_System
{
    public enum AnamnesesSortCriterium { DATE, DOCTOR, SPECIALIZATION };
    public enum SortDirection { ASCENDING, DESCENDING };
    public enum DoctorSortPriority { RATINGS, FIRST_NAME, LAST_NAME, SPECIALIZATION };

    public enum AppointmentType { EXAMINATION, OPERATION }

    public enum AppointmentStatus { BOOKED, FINISHED, ON_HOLD }
}
namespace HealthCare_System.Database
{
    public class HealthCareDatabase
    {
        AnamnesisRepo anamnesisRepo;
        AppointmentRepo appointmentRepo;
        AppointmentRequestRepo appointmentRequestRepo;
        DaysOffNotificationRepo daysOffNotificationRepo;
        DaysOffRequestRepo daysOffRequestRepo;
        DelayedAppointmentNotificationRepo delayedAppointmentNotificationRepo;
        DoctorRepo doctorRepo;
        DoctorSurveyRepo doctorSurveyRepo;
        DrugRepo drugRepo;
        DrugNotificationRepo drugNotificationRepo;
        EquipmentRepo equipmentRepo;
        HospitalSurveyRepo hospitalSurveyRepo;
        IngredientRepo ingredientRepo;
        ManagerRepo managerRepo;
        MedicalRecordRepo medicalRecordRepo;
        MergingRenovationRepo mergingRenovationRepo;
        PatientRepo patientRepo;
        PrescriptionRepo prescriptionRepo;
        ReferralRepo referralRepo;
        RoomRepo roomRepo;
        SimpleRenovationRepo simpleRenovationRepo;
        SplittingRenovationRepo splittingRenovationRepo;
        SupplyRequestRepo supplyRequestRepo;
        SecretaryRepo secretaryRepo;
        EquipmentTransferRepo equipmentTransferRepo;

        internal AnamnesisRepo AnamnesisRepo { get => anamnesisRepo; set => anamnesisRepo = value; }

        internal AppointmentRepo AppointmentRepo { get => appointmentRepo; set => appointmentRepo = value; }

        internal AppointmentRequestRepo AppointmentRequestRepo 
            { get => appointmentRequestRepo; set => appointmentRequestRepo = value; }

        internal DaysOffNotificationRepo DaysOffNotificationRepo 
            { get => daysOffNotificationRepo; set => daysOffNotificationRepo = value; }

        internal DaysOffRequestRepo DaysOffRequestRepo { get => daysOffRequestRepo; set => daysOffRequestRepo = value; }

        internal DelayedAppointmentNotificationRepo DelayedAppointmentNotificationRepo 
            { get => delayedAppointmentNotificationRepo; set => delayedAppointmentNotificationRepo = value; }

        internal DoctorRepo DoctorRepo { get => doctorRepo; set => doctorRepo = value; }

        internal DoctorSurveyRepo DoctorSurveyRepo { get => doctorSurveyRepo; set => doctorSurveyRepo = value; }

        internal DrugRepo DrugRepo { get => drugRepo; set => drugRepo = value; }

        internal DrugNotificationRepo DrugNotificationRepo 
            { get => drugNotificationRepo; set => drugNotificationRepo = value; }

        internal EquipmentRepo EquipmentRepo { get => equipmentRepo; set => equipmentRepo = value; }

        internal HospitalSurveyRepo HospitalSurveyRepo { get => hospitalSurveyRepo; set => hospitalSurveyRepo = value; }

        internal IngredientRepo IngredientRepo { get => ingredientRepo; set => ingredientRepo = value; }

        internal ManagerRepo ManagerRepo { get => managerRepo; set => managerRepo = value; }

        internal MedicalRecordRepo MedicalRecordRepo { get => medicalRecordRepo; set => medicalRecordRepo = value; }

        internal MergingRenovationRepo MergingRenovationRepo 
            { get => mergingRenovationRepo; set => mergingRenovationRepo = value; }

        internal PatientRepo PatientRepo { get => patientRepo; set => patientRepo = value; }

        internal PrescriptionRepo PrescriptionRepo { get => prescriptionRepo; set => prescriptionRepo = value; }

        internal ReferralRepo ReferralRepo { get => referralRepo; set => referralRepo = value; }

        internal RoomRepo RoomRepo { get => roomRepo; set => roomRepo = value; }

        internal SimpleRenovationRepo SimpleRenovationRepo 
            { get => simpleRenovationRepo; set => simpleRenovationRepo = value; }

        internal SplittingRenovationRepo SplittingRenovationRepo 
            { get => splittingRenovationRepo; set => splittingRenovationRepo = value; }

        internal SupplyRequestRepo SupplyRequestRepo { get => supplyRequestRepo; set => supplyRequestRepo = value; }

        internal SecretaryRepo SecretaryRepo { get => secretaryRepo; set => secretaryRepo = value; }

        internal EquipmentTransferRepo EquipmentTransferRepo 
            { get => equipmentTransferRepo; set => equipmentTransferRepo = value; }

        public HealthCareDatabase()
        {
            anamnesisRepo = new();
            appointmentRepo = new();
            appointmentRequestRepo = new();
            daysOffNotificationRepo = new();
            daysOffRequestRepo = new();
            delayedAppointmentNotificationRepo = new();
            doctorRepo = new();
            doctorSurveyRepo = new();
            drugRepo = new();
            drugNotificationRepo = new();
            equipmentRepo = new();
            hospitalSurveyRepo = new();
            ingredientRepo = new();
            managerRepo = new();
            medicalRecordRepo = new();
            mergingRenovationRepo = new();
            patientRepo = new();
            prescriptionRepo = new();
            referralRepo = new();
            roomRepo = new();
            simpleRenovationRepo = new();
            splittingRenovationRepo = new();
            supplyRequestRepo = new();
            secretaryRepo = new();
            equipmentTransferRepo = new();

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

        void LinkDrugIngredient(string path = "../../../data/links/Drug_Ingredient.csv")
        {
            StreamReader file = new(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int drugId = Convert.ToInt32(line.Split(";")[0]);
                int ingredientId = Convert.ToInt32(line.Split(";")[1].Trim());

                Drug drug = drugRepo.FindById(drugId);
                Ingredient ingredient = ingredientRepo.FindById(ingredientId);

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


                AppointmentRequest request = appointmentRequestRepo.FindById(requestId);
                Patient patient = patientRepo.FindByJmbg(patientId);
                Appointment oldAppointment = appointmentRepo.FindById(oldAppointmentId);
                Appointment newAppointment = null;
                if (newAppointmentId != -1)
                {
                    appointmentRepo.FindById(newAppointmentId);
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

                DaysOffNotification notification = daysOffNotificationRepo.FindById(notificationId);
                Doctor doctor = doctorRepo.FindByJmbg(doctorId);

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

                DaysOffRequest request = daysOffRequestRepo.FindById(requestId);
                Doctor doctor = doctorRepo.FindByJmbg(doctorId);

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

                DelayedAppointmentNotification notification = delayedAppointmentNotificationRepo.FindById(notificationtId);
                Appointment appointment = appointmentRepo.FindById(appointmentId);

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

                MedicalRecord medicalRecord = medicalRecordRepo.FindById(medicalRecordId);
                Patient patient = patientRepo.FindByJmbg(patientJmbg);

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

                Appointment appointment = appointmentRepo.FindById(appointmentId);
                Doctor doctor = doctorRepo.FindByJmbg(doctorJmbg);
                Patient patient = patientRepo.FindByJmbg(patientJmbg);
                Room room;
                if (roomId == -1)
                {
                    room = null;
                }
                else
                {
                    room = roomRepo.FindById(roomId);
                }

                Anamnesis anamnesis = anamnesisRepo.FindById(anamnesisId);

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

                MedicalRecord medicalRecord = medicalRecordRepo.FindById(medicalRecordId);
                Ingredient ingredient = ingredientRepo.FindById(ingrediantId);

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

                Doctor doctor = doctorRepo.FindByJmbg(doctorJmbg);
                DoctorSurvey doctorSurvey = doctorSurveyRepo.FindById(surveyId);

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

                Prescription prescription = prescriptionRepo.FindById(prescriptionId);
                MedicalRecord medicalRecord = medicalRecordRepo.FindById(medicalRecordId);
                Drug drug = drugRepo.FindById(drugId);

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

                Referral referral = referralRepo.FindById(referralId);
                Doctor doctor = doctorRepo.FindByJmbg(doctorJmbg);
                MedicalRecord medicalRecord = medicalRecordRepo.FindById(medicalRecordId);

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

                Room room = this.roomRepo.FindById(roomId);
                Equipment equipment = equipmentRepo.FindById(equipmentId);

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

                SimpleRenovation simpleRenovation = simpleRenovationRepo.FindById(simpleRenovationId);
                Room room = roomRepo.FindById(roomId);

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

                SplittingRenovation splittingRenovation = splittingRenovationRepo.FindById(splittingRenovationId);
                Room room = roomRepo.FindById(roomId);

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

                MergingRenovation mergingRenovation = mergingRenovationRepo.FindById(mergingRenovationId);
                Room firstRoom = roomRepo.FindById(firstRoomId);
                Room secondRoom = roomRepo.FindById(secondRoomId);

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

                SupplyRequest supplyRequest = supplyRequestRepo.FindById(supplyRequestId);
                Equipment equipment = equipmentRepo.FindById(equipmentId);

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

                Transfer transfer = equipmentTransferRepo.FindById(transferId);
                Room fromRoom = roomRepo.FindById(fromRoomId);
                Room toRoom = roomRepo.FindById(toRoomId);
                Equipment equipment = equipmentRepo.FindById(equipmentId);

                transfer.FromRoom = fromRoom;
                transfer.ToRoom = toRoom;
                transfer.Equipment = equipment;
            }

            file.Close();
        }

    }
}
