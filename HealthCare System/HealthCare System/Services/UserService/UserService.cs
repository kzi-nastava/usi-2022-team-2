using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Services.AppointmentService;
using HealthCare_System.Model;
using System.Windows;

namespace HealthCare_System.Services.UserService
{
    class UserService
    {
        UserRepo userRepo;
        PatientService patientService;
        DoctorService doctorService;
        ManagerService managerService;
        SecretaryService secretaryService;
        AppointmentRequestService appointmentRequestService;

        public UserService()
        {
            UserRepoFactory userRepoFactory = new();
            userRepo = userRepoFactory.CreateUserRepository();
        }

        public UserRepo UserRepo { get => userRepo;}

        public void RunAntiTrollCheck(Patient patient)
        {
            appointmentRequestService = new();
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
            doctorService = new();
            patientService = new();
            managerService = new();
            secretaryService = new();
            appointmentRequestService = new();

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
