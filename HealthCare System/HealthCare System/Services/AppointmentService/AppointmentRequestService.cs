using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using HealthCare_System.Repository.AppointmentRepo;

namespace HealthCare_System.Services.AppointmentService
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
            AppointmentService appointmentService = new();

            if (request.Type == RequestType.DELETE)
            {
                appointmentService.AppointmentRepo.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
            }
            appointmentService.AppointmentRepo.Appointments.Remove(request.OldAppointment);
            request.OldAppointment = null;
            request.State = AppointmentState.ACCEPTED;
            appointmentService.AppointmentRepo.Serialize();
            appointmentRequestRepo.Serialize();
        }
        public void RejectRequest(AppointmentRequest request)
        {
            AppointmentService appointmentService = new();
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentService.AppointmentRepo.Appointments.Remove(request.NewAppointment);
                request.NewAppointment = null;
                appointmentService.AppointmentRepo.Serialize();
            }
            appointmentRequestRepo.Serialize();
        }
    }
}
