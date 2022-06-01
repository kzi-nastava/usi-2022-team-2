using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserService
{
    class UserService
    {
        UserRepo userRepo;

        public UserService()
        {
            UserRepoFactory userRepoFactory = new();
            userRepo = userRepoFactory.CreateUserRepository();
        }

        public UserRepo UserRepo { get => userRepo;}

        public void RunAntiTrollCheck(Patient patient)
        {
            DateTime now = DateTime.Now;
            int createRequests = 0;
            int editRequests = 0;

            foreach (AppointmentRequest request in appointmentRequests)
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
    }
}
