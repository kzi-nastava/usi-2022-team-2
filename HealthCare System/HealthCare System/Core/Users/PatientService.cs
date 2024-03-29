﻿using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class PatientService : IPatientService
    {
        IPatientRepo patientRepo;
        ISchedulingService schedulingService;
        IPrescriptionService prescriptionService;
        IMedicalRecordService medicalRecordService;
        IIngredientService ingredientService;

        public PatientService(IPatientRepo patientRepo, ISchedulingService schedulingService,
            IPrescriptionService prescriptionService, IMedicalRecordService medicalRecordService,
            IIngredientService ingredientService)
        {
            this.patientRepo = patientRepo;
            this.SchedulingService = schedulingService;
            this.PrescriptionService = prescriptionService;
            this.MedicalRecordService = medicalRecordService;
            this.IngredientService = ingredientService;
        }

        public IPatientRepo PatientRepo { get => patientRepo; }
        public ISchedulingService SchedulingService { get => schedulingService; set => schedulingService = value; }
        public IPrescriptionService PrescriptionService { get => prescriptionService; set => prescriptionService = value; }
        public IMedicalRecordService MedicalRecordService { get => medicalRecordService; set => medicalRecordService = value; }
        public IIngredientService IngredientService { get => ingredientService; set => ingredientService = value; }

        public List<Patient> Patients()
        {
            return patientRepo.Patients;
        }

        public void DeletePatient(Patient patient)
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;
            try
            {
                SchedulingService.DeleteAppointmens(patient);
            }
            catch
            {
                throw;
            }
            PrescriptionService.DeletePrescriptions(medicalRecord);

            MedicalRecordService.Delete(medicalRecord);
            patientRepo.Delete(patient);

        }
        public void AddPatient(PersonDto personDto, MedicalRecord medRecord)
        {
            Patient patient = new();
            patient.Jmbg = personDto.Jmbg;
            patient.FirstName = personDto.FirstName;
            patient.LastName = personDto.LastName;
            patient.Mail = personDto.Mail;
            patient.BirthDate = personDto.BirthDate;
            patient.Password = personDto.Password;
            patientRepo.Add(patient);

            patient.MedicalRecord = medRecord;
            medRecord.Patient = patient;
            MedicalRecordService.Serialize();
            IngredientService.Serialize();
        }
        public void UpdatePatient()
        {
            patientRepo.Serialize();
            MedicalRecordService.Serialize();
        }

        public void BlockPatient(Patient patient)
        {
            patient.Blocked = !patient.Blocked;
            patientRepo.Serialize();
        }

        public Patient FindByMail(string mail)
        {
            return patientRepo.FindByMail(mail);
        }

        public Patient FindByJmbg(string jmbg)
        {
            return patientRepo.FindByJmbg(jmbg);
        }

        public void Serialize()
        {
            patientRepo.Serialize();
        }
    }
}
