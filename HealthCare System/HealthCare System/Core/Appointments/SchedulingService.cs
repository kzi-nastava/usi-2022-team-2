using System;
using System.Collections.Generic;
using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.Anamneses.Model;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Model;

namespace HealthCare_System.Core.Appointments
{
    public class SchedulingService
    {
        RoomService roomService;
        AppointmentService appointmentService;
        AnamnesisService anamnesisService;
        DoctorService doctorService;
        ReferralService referralService;

        public SchedulingService(RoomService roomService, AppointmentService appointmentService, 
            AnamnesisService anamnesisService, DoctorService doctorService, ReferralService referralService)
        {
            this.roomService = roomService;
            this.appointmentService = appointmentService;
            this.anamnesisService = anamnesisService;
            this.doctorService = doctorService;
            this.referralService = referralService;
        }
        
        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {
            List<Room> rooms = new List<Room>();

            foreach (Room room in roomService.RoomRepo.GetRoomsByType(type)) 
                if (roomService.IsRoomAvailableRenovationsAtTime(room, start))
                    rooms.Add(room);

            foreach (Appointment appointment in appointmentService.AppointmentRepo.Appointments)
            {

                if (rooms.Contains(appointment.Room) && ((appointment.Start <= start && appointment.End >= start) ||
                    (appointment.Start <= end && appointment.End >= end) ||
                    (start <= appointment.Start && end >= appointment.End)))
                {
                    rooms.Remove(appointment.Room);
                }
            }

            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms[0];
        }

        public Appointment AddAppointment(AppointmentDto appointmentDto)
        {
            Appointment appointment = new(appointmentDto);
            int anamnesisId = anamnesisService.AnamnesisRepo.GenerateId();
            Anamnesis anamnesis = new(anamnesisId, "");
            appointment.Anamnesis = anamnesis;

            Room room = AvailableRoom(appointment.Type, appointment.Start, appointment.End);
            appointment.Room = room;

            appointment.Validate();

            appointmentService.AppointmentRepo.Appointments.Add(appointment);
            appointment.Doctor.Appointments.Add(appointment);
            appointment.Patient.MedicalRecord.Appointments.Add(appointment);
            anamnesisService.AnamnesisRepo.Anamneses.Add(appointment.Anamnesis);
            appointmentService.AppointmentRepo.Serialize();
            anamnesisService.AnamnesisRepo.Serialize();

            return appointment;

        }

        public void UpdateAppointment(AppointmentDto newAppointmentDto)
        {
            Appointment newAppointment = new(newAppointmentDto);
            newAppointment.Validate();
            Appointment appointment = appointmentService.AppointmentRepo.FindById(newAppointment.Id);

            appointment.Start = newAppointment.Start;
            appointment.End = newAppointment.End;
            appointment.Doctor = newAppointment.Doctor;
            appointment.Patient = newAppointment.Patient;
            appointment.Status = newAppointment.Status;
            appointmentService.AppointmentRepo.Serialize();
        }

        public void DeleteAppointment(int id)
        {
            Appointment appointment = appointmentService.AppointmentRepo.FindById(id);
            if (appointment is null)
            {
                throw new Exception("Appointment is not found!");
            }
            appointmentService.AppointmentRepo.Appointments.Remove(appointment);
            appointment.Doctor.Appointments.Remove(appointment);
            appointment.Patient.MedicalRecord.Appointments.Remove(appointment);
            anamnesisService.AnamnesisRepo.Anamneses.Remove(appointment.Anamnesis);
            appointmentService.AppointmentRepo.Serialize();
            anamnesisService.AnamnesisRepo.Serialize();
        }

        public Appointment BookAppointmentByReferral(Referral referral)
        {
            Doctor doctor = referral.Doctor;
            if (doctor is null)
            {
                doctor = doctorService.DoctorRepo.FindBySpecialization(referral.Specialization)[0];
            }

            DateTime closestTimeForDoctor = doctor.getClosestFreeAppointment(15, referral.MedicalRecord.Patient);

            referral.Used = true;
            referralService.ReferralRepo.Serialize();

            int id = appointmentService.AppointmentRepo.GenerateId();
            AppointmentDto appointmentDto = new(id, closestTimeForDoctor, closestTimeForDoctor.AddMinutes(15), doctor,
                referral.MedicalRecord.Patient, null, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
            return AddAppointment(appointmentDto);
        }

        public void DeleteAppointmens(Patient patient)
        {
            for (int i = appointmentService.AppointmentRepo.Appointments.Count - 1; i >= 0; i--)
            {
                if (appointmentService.AppointmentRepo.Appointments[i].Patient == patient)
                {
                    if (appointmentService.AppointmentRepo.Appointments[i].Start > DateTime.Now)
                    {
                        throw new Exception("Can't delete selected patient, because of it's future appointments.");
                    }
                    DeleteAppointment(appointmentService.AppointmentRepo.Appointments[i].Id);
                }
            }
        }
    }
}
