﻿using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.AppotinmentRequests;
using HealthCare_System.Core.Users.Model;
using System;
using System.Windows;

namespace HealthCare_System.Core.Users
{
    public class UserService : IUserService
    {
        IPatientService patientService;
        IDoctorService doctorService;
        IManagerService managerService;
        ISecretaryService secretaryService;
        IAppointmentRequestService appointmentRequestService;

        public UserService(IPatientService patientService, IDoctorService doctorService,
            IManagerService managerService, ISecretaryService secretaryService,
            IAppointmentRequestService appointmentRequestService)
        {
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.managerService = managerService;
            this.secretaryService = secretaryService;
            this.appointmentRequestService = appointmentRequestService;
        }

        public void RunAntiTrollCheck(Patient patient)
        {
            DateTime now = DateTime.Now;
            int createRequests = 0;
            int editRequests = 0;

            foreach (AppointmentRequest request in appointmentRequestService.AppointmentRequests())
            {
                if (request.Patient == patient)
                {
                    if ((now - request.RequestCreated).TotalDays < 30)
                    {
                        if (request.Type == RequestType.CREATE)
                            createRequests++;
                        else
                            editRequests++;
                    }
                }
            }

            if (createRequests > 7 || editRequests > 4)
                patient.Blocked = true;
        }

        public Person Login(string mail, string password)
        {
            foreach (Doctor doctor in doctorService.Doctors())
                if (doctor.Mail == mail && doctor.Password == password)
                    return doctor;

            foreach (Patient patient in patientService.Patients())
                if (patient.Mail == mail && patient.Password == password)
                {
                    RunAntiTrollCheck(patient);
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
            foreach (Manager manager in managerService.Managers())
                if (manager.Mail == mail && manager.Password == password)
                    return manager;

            foreach (Secretary secretary in secretaryService.Secretaries())
                if (secretary.Mail == mail && secretary.Password == password)
                    return secretary;

            return null;
        }
    }
}
