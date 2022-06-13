using System.Collections.Generic;
using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.AppotinmentRequests.Repository;
using HealthCare_System.Model;

namespace HealthCare_System.Core.AppotinmentRequests
{
    public class AppointmentRequestService : IAppointmentRequestService
    {
        AppointmentRequestRepo appointmentRequestRepo;
        AppointmentService appointmentService;

        public AppointmentRequestService(AppointmentRequestRepo appointmentRequestRepo, AppointmentService appointmentService)
        {
            this.appointmentRequestRepo = appointmentRequestRepo;
            this.appointmentService = appointmentService;
        }

        internal AppointmentRequestRepo AppointmentRequestRepo { get => appointmentRequestRepo; }

        public List<AppointmentRequest> AppointmentRequests()
        {
            return appointmentRequestRepo.AppointmentRequests;
        }

        public void AcceptRequest(AppointmentRequest request)
        {
            if (request.Type == RequestType.DELETE)
            {
                appointmentService.Appointments().Remove(request.NewAppointment);
                request.NewAppointment = null;
            }
            appointmentService.Appointments().Remove(request.OldAppointment);
            request.OldAppointment = null;
            request.State = AppointmentState.ACCEPTED;
            appointmentService.AppointmentRepo.Serialize();
            appointmentRequestRepo.Serialize();
        }
        public void RejectRequest(AppointmentRequest request)
        {
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentService.Appointments().Remove(request.NewAppointment);
                request.NewAppointment = null;
                appointmentService.AppointmentRepo.Serialize();
            }
            appointmentRequestRepo.Serialize();
        }
        public void Add(AppointmentRequestDto requestDto)
        {
            AppointmentRequest request = new(requestDto);
            appointmentRequestRepo.Add(request);
        }
    }
}
