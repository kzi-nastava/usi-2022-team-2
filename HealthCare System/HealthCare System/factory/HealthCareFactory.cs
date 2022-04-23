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

            this.LinkRoomEquipment();
            this.LinkSimpleRenovationRoom();
            this.LinkSplittingRenovationRoom();
            this.LinkMergingRenovationRoom();
            this.LinkSupplyRequestEquipment();
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

                room.EquipmentAmount[equipmentId] = amount;
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
                int mergingRenovationId = Convert.ToInt32(line.Split(";")[0].Trim());
                int firstRoomId = Convert.ToInt32(line.Split(";")[1]);
                int secondRoomId = Convert.ToInt32(line.Split(";")[2]);

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
    }
}
