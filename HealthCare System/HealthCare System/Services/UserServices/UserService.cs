using System;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Model;
using System.Windows;

namespace HealthCare_System.Services.UserServices
{
    class UserService
    {
        PatientService patientService;
        DoctorService doctorService;
        ManagerService managerService;
        SecretaryService secretaryService;
        AppointmentRequestService appointmentRequestService;

        public UserService(PatientService patientService, DoctorService doctorService, 
            ManagerService managerService, SecretaryService secretaryService, 
            AppointmentRequestService appointmentRequestService)
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
