using HealthCare_System.controllers;
using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.factory
{
    class HealthCareFactory
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
        ReferralController referralController;
        RoomController roomController;
        SimpleRenovationController simpleRenovationController;
        SplittingRenovationController splittingRenovationController;
        SupplyRequestController supplyRequestController;

        public HealthCareFactory()
        {
            this.anamnesisController = new();
            this.appointmentController = new();
            this.appointmentRequestController = new();
            this.daysOffNotificationController = new();
            this.daysOffRequestController = new();
            this.delayedAppointmentNotificationController = new();
            this.doctorController = new();
            this.doctorSurveyController = new();
            this.drugController = new();
            this.drugNotificationController = new();
            this.equipmentController = new();
            this.hospitalSurveyController = new();
            this.ingredientController = new();
            this.managerController = new();
            this.medicalRecordController = new();
            this.mergingRenovationController = new();
            this.patientController = new();
            this.referralController = new();
            this.roomController = new();
            this.simpleRenovationController = new();
            this.splittingRenovationController = new();
            this.supplyRequestController = new();

            this.LinkDrugIngredient();
            this.LinkDrugNotification();
            this.LinkAppointmentRequest();
            this.LinkDaysOffNotification();
            this.LinkDaysOffRequest();
        }


        //TODO add the rest of user types
        public Person Login(string mail, string password)
        {
            foreach (Doctor doctor in this.doctorController.Doctors)
                if (doctor.Mail == mail && doctor.Password == password)
                    return doctor;

            foreach (Patient patient in this.patientController.Patients)
                if (patient.Mail == mail && patient.Password == password)
                    return patient;

            foreach (Manager manager in this.managerController.Managers)
                if (manager.Mail == mail && manager.Password == password)
                    return manager;

            return null;
        }
        
        void LinkDrugNotification(string path = "data/links/Notification_Patient_Drug.csv")
        {
            StreamReader file = new StreamReader(path);

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
            StreamReader file = new StreamReader(path);

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
            StreamReader file = new StreamReader(path);

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
            StreamReader file = new StreamReader(path);

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
            StreamReader file = new StreamReader(path);

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
            StreamReader file = new StreamReader(path);

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                int notificationtId = Convert.ToInt32(line.Split(";")[0]);
                int appointmentId = Convert.ToInt32(line.Split(";")[1].Trim());

                DelayedAppointmentNotification notification= this.delayedAppointmentNotificationController.FindById(notificationtId);
                Appointment appointment = this.appointmentController.FindById(appointmentId);

                notification.Appointment= appointment;
            }

            file.Close();
        }

    }
}
