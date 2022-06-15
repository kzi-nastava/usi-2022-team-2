using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.AppotinmentRequests;
using HealthCare_System.Core.DoctorSurveys;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.HospitalSurveys;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.SupplyRequests;
using HealthCare_System.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Database
{
    public class ServiceBuilder
    {
        IAnamnesisService anamnesisService;

        IAppointmentRequestService appointmentRequestService;
        IAppointmentRecomendationService appointmentRecomendationService;
        IAppointmentService appointmentService;
        ISchedulingService schedulingService;
        IUrgentSchedulingService urgentSchedulingService;
        IDoctorSurveyService doctorSurveyService;

        IDrugService drugService;
        IEquipmentService equipmentService;
        IEquipmentTransferService equipmentTransferService;
        IHospitalSurveyService hospitalSurveyService;
        IIngredientService ingredientService;
        IMedicalRecordService medicalRecordService;

        IDelayedAppointmentNotificationService delayedAppointmentNotificationService;
        IDrugNotificationService drugNotificationService;
        IPrescriptionService prescriptionService;
        IReferralService referralService;
        IMergingRenovationService mergingRenovationService;
        ISimpleRenovationService simpleRenovationService;
        ISplittingRenovationService splittingRenovationService;

        IRoomService roomService;
        ISupplyRequestService supplyRequestService;
        IDoctorService doctorService;
        IManagerService managerService;
        IPatientService patientService;
        ISecretaryService secretaryService;
        IUserService userService;

        public ServiceBuilder()
        {
            HealthCareDatabase database = new();
            anamnesisService = new AnamnesisService(database.AnamnesisRepo);
            hospitalSurveyService = new HospitalSurveyService(database.HospitalSurveyRepo);
            referralService = new ReferralService(database.ReferralRepo);
            doctorSurveyService = new DoctorSurveyService(database.DoctorSurveyRepo);
            managerService = new ManagerService(database.ManagerRepo);
            secretaryService = new SecretaryService(database.SecretaryRepo);
            delayedAppointmentNotificationService = new DelayedAppointmentNotificationService(database.DelayedAppointmentNotificationRepo);
            drugNotificationService = new DrugNotificationService(database.DrugNotificationRepo);
            medicalRecordService = new MedicalRecordService(database.MedicalRecordRepo);
            prescriptionService = new PrescriptionService(database.PrescriptionRepo, medicalRecordService);
            drugService = new DrugService(database.DrugRepo, prescriptionService);
            ingredientService = new IngredientService(database.IngredientRepo, drugService);
            doctorService = new DoctorService(database.DoctorRepo, doctorSurveyService);
            appointmentService = new AppointmentService(database.AppointmentRepo, schedulingService);//sch
            appointmentRequestService = new AppointmentRequestService(database.AppointmentRequestRepo, appointmentService);

            appointmentRecomendationService = new AppointmentRecomendationService(appointmentService, schedulingService, doctorService);
            schedulingService = new SchedulingService(roomService, appointmentService, anamnesisService, doctorService, referralService);
            urgentSchedulingService = new UrgentSchedulingService(appointmentService, schedulingService);
            equipmentService = new EquipmentService(database.EquipmentRepo, roomService);
            equipmentTransferService = new EquipmentTransferService(database.EquipmentTransferRepo, roomService);
            mergingRenovationService = new MergingRenovationService(database.MergingRenovationRepo, roomService, equipmentTransferService, equipmentService);
            simpleRenovationService = new SimpleRenovationService(database.SimpleRenovationRepo, roomService, equipmentTransferService, equipmentService);
            splittingRenovationService = new SplittingRenovationService(database.SplittingRenovationRepo, roomService, equipmentTransferService, equipmentService);
            roomService = new RoomService(mergingRenovationService, simpleRenovationService, equipmentTransferService, splittingRenovationService, appointmentService, database.RoomRepo);
            patientService = new PatientService(database.PatientRepo, schedulingService, prescriptionService, medicalRecordService, ingredientService);
            userService = new UserService(patientService, doctorService, managerService, secretaryService, appointmentRequestService);
            supplyRequestService = new SupplyRequestService(database.SupplyRequestRepo, roomService, equipmentTransferService);

            appointmentService.
        }

        public IAnamnesisService AnamnesisService { get => anamnesisService;}
        public IAppointmentRequestService AppointmentRequestService { get => appointmentRequestService; }
        public IAppointmentRecomendationService AppointmentRecomendationService { get => appointmentRecomendationService;}
        public IAppointmentService AppointmentService { get => appointmentService;}
        public ISchedulingService SchedulingService { get => schedulingService; }
        public IUrgentSchedulingService UrgentSchedulingService { get => urgentSchedulingService;}
        public IDoctorSurveyService DoctorSurveyService { get => doctorSurveyService;}
        public IDrugService DrugService { get => drugService;}
        public IEquipmentService EquipmentService { get => equipmentService;}
        public IEquipmentTransferService EquipmentTransferService { get => equipmentTransferService;}
        public IHospitalSurveyService HospitalSurveyService { get => hospitalSurveyService;}
        public IIngredientService IngredientService { get => ingredientService;}
        public IMedicalRecordService MedicalRecordService { get => medicalRecordService;}
        public IDelayedAppointmentNotificationService DelayedAppointmentNotificationService { get => delayedAppointmentNotificationService;}
        public IDrugNotificationService DrugNotificationService { get => drugNotificationService;}
        public IPrescriptionService PrescriptionService { get => prescriptionService;}
        public IReferralService ReferralService { get => referralService;}
        public IMergingRenovationService MergingRenovationService { get => mergingRenovationService;}
        public ISimpleRenovationService SimpleRenovationService { get => simpleRenovationService;}
        public ISplittingRenovationService SplittingRenovationService { get => splittingRenovationService;}
        public IRoomService RoomService { get => roomService;}
        public ISupplyRequestService SupplyRequestService { get => supplyRequestService;}
        public IDoctorService DoctorService { get => doctorService;}
        public IManagerService ManagerService { get => managerService;}
        public IPatientService PatientService { get => patientService;}
        public ISecretaryService SecretaryService { get => secretaryService;}
        public IUserService UserService { get => userService;}
    }
}
