using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;

namespace HealthCare_System.Core.Appointments
{
    public interface ISchedulingService
    {
        IRoomService RoomService { get; set; }
        IAppointmentService AppointmentService { get; set; }
        IAnamnesisService AnamnesisService { get; set; }
        IDoctorService DoctorService { get; set; }
        IReferralService ReferralService { get; set; }
        Appointment AddAppointment(AppointmentDto appointmentDto);
        Room AvailableRoom(AppointmentType type, DateTime start, DateTime end);
        Appointment BookAppointmentByReferral(Referral referral);
        void DeleteAppointmens(Patient patient);
        void DeleteAppointment(int id);
        void UpdateAppointment(AppointmentDto newAppointmentDto);
    }
}