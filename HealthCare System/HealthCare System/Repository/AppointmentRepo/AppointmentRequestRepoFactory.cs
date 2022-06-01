using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.AppointmentRepo
{
    class AppointmentRequestRepoFactory
    {
        private AppointmentRequestRepo appointmentRequestRepo;

        public AppointmentRequestRepo CreateAppointmentRequestRepository()
        {
            if (appointmentRequestRepo == null)
                appointmentRequestRepo = new AppointmentRequestRepo();

            return appointmentRequestRepo;
        }
    }
}
