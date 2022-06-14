using HealthCare_System.Core.AppointmentRequests.Model;
using HealthCare_System.Core.AppotinmentRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class AppointmentRequestController
    {
        private readonly IAppointmentRequestService appointmentRequestService;

        public AppointmentRequestController(IAppointmentRequestService appointmentRequestService)
        {
            this.appointmentRequestService = appointmentRequestService;
        }
        public List<AppointmentRequest> AppointmentRequests()
        {
            return appointmentRequestService.AppointmentRequests();
        }

        public void AcceptRequest(AppointmentRequest request)
        {
            appointmentRequestService.AcceptRequest(request);
        }
        public void RejectRequest(AppointmentRequest request)
        {
            appointmentRequestService.RejectRequest(request);
        }
        public void Add(AppointmentRequestDto requestDto)
        {
            appointmentRequestService.Add(requestDto);
        }

        public AppointmentRequest FindById(int id)
        {
            return appointmentRequestService.FindById(id);
        }

        public void Serialize(string linkPath = "../../../data/links/AppointmentRequestLinker.csv")
        {
            appointmentRequestService.Serialize(linkPath);
        }
        public int GenerateId()
        {
            return appointmentRequestService.GenerateId();
        }

    }
}
