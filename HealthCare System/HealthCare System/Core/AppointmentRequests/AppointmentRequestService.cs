using System.Collections.Generic;
using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.AppotinmentRequests.Repository;
using HealthCare_System.Model;

namespace HealthCare_System.Core.AppotinmentRequests
{
    public class AppointmentRequestService : IAppointmentRequestService
    {
        IAppointmentRequestRepo appointmentRequestRepo;
        IAppointmentService appointmentService;

        public AppointmentRequestService(IAppointmentRequestRepo appointmentRequestRepo, IAppointmentService appointmentService)
        {
            this.appointmentRequestRepo = appointmentRequestRepo;
            this.appointmentService = appointmentService;
        }

        public IAppointmentRequestRepo AppointmentRequestRepo { get => appointmentRequestRepo; }
        public IAppointmentService AppointmentService { get => appointmentService; set => appointmentService = value; }

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
            appointmentService.Serialize();
            Serialize();
        }
        public void RejectRequest(AppointmentRequest request)
        {
            request.State = AppointmentState.DENIED;
            if (request.Type == RequestType.UPDATE)
            {
                appointmentService.Appointments().Remove(request.NewAppointment);
                request.NewAppointment = null;
                appointmentService.Serialize();
            }
            Serialize();
        }
        public void Add(AppointmentRequestDto requestDto)
        {
            AppointmentRequest request = new(requestDto);
            appointmentRequestRepo.Add(request);
        }

        public AppointmentRequest FindById(int id)
        {
            return appointmentRequestRepo.FindById(id);
        }

        public void Serialize(string linkPath = "../../../data/links/AppointmentRequestLinker.csv")
        {
            appointmentRequestRepo.Serialize(linkPath);
        }
        public int GenerateId()
        {
            return appointmentRequestRepo.GenerateId();
        }
    }
}
