using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.AppointmentRepo;

namespace HealthCare_System.Services.SchedulingService
{
    class AppointmentRequestService
    {
        AppointmentRequestRepo appointmentRequestRepo;

        public AppointmentRequestService()
        {
            AppointmentRequestRepoFactory appointmentRequestRepoFactory = new();
            appointmentRequestRepo = appointmentRequestRepoFactory.CreateAppointmentRequestRepository();
        }

        internal AppointmentRequestRepo AppointmentRequestRepo { get => appointmentRequestRepo; }

        public void AcceptRequest(AppointmentRequest request)
        {
            if (request.Type == RequestType.DELETE)
            {
                appointmentController.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
            }
            appointmentController.Appointments.Remove(request.OldAppointment);
            request.OldAppointment = null;
            request.State = AppointmentState.ACCEPTED;
            appointmentController.Serialize();
            appointmentRequestController.Serialize();
        }
        public void RejectRequest(AppointmentRequest request)
        {
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentController.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
                appointmentController.Serialize();
            }
            AppointmentRequestController.Serialize();
        }
    }
}
