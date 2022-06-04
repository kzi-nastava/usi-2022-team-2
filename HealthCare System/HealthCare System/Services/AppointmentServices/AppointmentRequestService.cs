using System.Collections.Generic;
using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using HealthCare_System.Repository.AppointmentRepo;

namespace HealthCare_System.Services.AppointmentServices
{
    class AppointmentRequestService
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
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentService.AppointmentRepo.Appointments.Remove(request.NewAppointment);
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
