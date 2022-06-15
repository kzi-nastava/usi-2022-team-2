using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.DaysOffRequests.Repository;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.DaysOffRequests
{
    class DaysOffRequestService : IDaysOffRequestService
    {
        IDaysOffRequestRepo daysOffRequestRepo;
        IDoctorService doctorService;
        IDaysOffNotificationService  daysOffNotificationService;

        public IDaysOffNotificationService DaysOffNotificationService { get => daysOffNotificationService; set => daysOffNotificationService = value; }

        public DaysOffRequestService(IDaysOffRequestRepo daysOffRequestRepo, IDoctorService doctorService, IDaysOffNotificationService daysOffNotificationService)
        {
            this.daysOffRequestRepo = daysOffRequestRepo;
            this.doctorService = doctorService;
            this.DaysOffNotificationService = daysOffNotificationService;
        }

        public DaysOffRequestService(IDaysOffRequestRepo daysOffRequestRepo, IDoctorService doctorService)
        {
            this.daysOffRequestRepo = daysOffRequestRepo;
            this.doctorService = doctorService;
        }

        public List<DaysOffRequest> DaysOffRequests()
        {
            return daysOffRequestRepo.DaysOffRequests;
        }

        public int GenerateId()
        {
            return daysOffRequestRepo.GenerateId();
        }

        public DaysOffRequest FindById(int id)
        {
            return daysOffRequestRepo.FindById(id);
        }


        public void Serialize()
        {
            daysOffRequestRepo.Serialize();
        }

        public List<DaysOffRequest> FillterByDoctor(Doctor doctor)
        {
            return daysOffRequestRepo.FillterByDoctor(doctor);
        }

        public bool IsValid(DaysOffRequest daysOffRequest)
        {
            List<DaysOffRequest> fillteredRequests = FillterByDoctor(daysOffRequest.Doctor);

            foreach (DaysOffRequest existingRequest in fillteredRequests)
                if ((existingRequest.Start <= daysOffRequest.Start && existingRequest.End >= daysOffRequest.Start) ||
                    (existingRequest.Start <= daysOffRequest.End && existingRequest.End >= daysOffRequest.End) ||
                    (daysOffRequest.Start <= existingRequest.Start && daysOffRequest.End >= existingRequest.End))
                        return false;

            return true;
        }

        public void Request(DaysOffRequestDto daysOffRequestDto)
        {
            DaysOffRequest daysOffRequest = new(daysOffRequestDto);

            if (!IsValid(daysOffRequest))
                throw new Exception("You have already requested absence in this period!");
            if (daysOffRequest.Doctor.HasAppointments(daysOffRequest.Start, daysOffRequest.End))
                throw new Exception("You have appointments in this period!");
            if (daysOffRequest.Doctor.IsFree(daysOffRequest.Start, daysOffRequest.End))
                throw new Exception("You are already free in this period!");

            daysOffRequestRepo.Add(daysOffRequest);
        }

        public void UrgentRequest(DaysOffRequestDto daysOffRequestDto)
        {
            Request(daysOffRequestDto);

            DaysOffRequest daysOffRequest = new(daysOffRequestDto);
            Doctor doctor = doctorService.FindByJmbg(daysOffRequest.Doctor.Jmbg);

            int numberOfDays = daysOffRequest.End.Day - daysOffRequest.Start.Day;
            for (int i = 0; i < numberOfDays; i++)
                doctor.FreeDates.Add(daysOffRequest.Start.AddDays(i));
            doctor.FreeDates.Add(daysOffRequest.End);

            doctorService.Serialize();
        }

        
        public void AcceptDaysOffRequest(DaysOffRequest daysOffRequest)
        {
            daysOffRequest.State = DaysOffRequestState.ACCEPTED;
            daysOffRequestRepo.Serialize();
            daysOffNotificationService.AddDaysOffNotification(daysOffRequest.Doctor, 
                "Request accepted! : Start - " + daysOffRequest.Start + " , End - " + daysOffRequest.End);
        }

        public void RejectDaysOffRequest(DaysOffRequest daysOffRequest, string message)
        {
            daysOffRequest.State = DaysOffRequestState.DENIED;
            daysOffRequestRepo.Serialize();

            daysOffNotificationService.AddDaysOffNotification(daysOffRequest.Doctor, 
                message + "  :  Start - " + daysOffRequest.Start + " , End - " + daysOffRequest.End);
        }
    }
}
