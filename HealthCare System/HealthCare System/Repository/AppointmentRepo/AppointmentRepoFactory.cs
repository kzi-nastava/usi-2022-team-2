using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.AppointmentRepo
{
    class AppointmentRepoFactory
    {
        private AppointmentRepo appointmentRepo;

        public AppointmentRepo CreateAppointmentRepository()
        {
            if (appointmentRepo == null)
                appointmentRepo = new AppointmentRepo();

            return appointmentRepo;
        }
    }
}
