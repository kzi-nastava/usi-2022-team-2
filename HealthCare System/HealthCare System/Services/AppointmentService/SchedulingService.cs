using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomService;
using HealthCare_System.Services.UserService;

namespace HealthCare_System.Services.SchedulingService
{
    class SchedulingService
    {
        
        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {

            List<Room> rooms = new List<Room>();

            RoomService.RoomService roomService = new();
            AppointmentService.AppointmentService appointmentService = new();

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
        public Appointment AddAppointment(Appointment appointment)
        {
            AnamnesisService.AnamnesisService anamnesisService = new();
            AppointmentService.AppointmentService appointmentService = new();

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
        public void UpdateAppointment(Appointment newAppointment)
        {
            AppointmentService.AppointmentService appointmentService = new();

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
            AppointmentService.AppointmentService appointmentService = new();
            AnamnesisService.AnamnesisService anamnesisService = new();

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
            DoctorService doctorService = new();
            AppointmentService.AppointmentService appointmentService = new();
            ReferralService.ReferralService referralService = new();

            Doctor doctor = referral.Doctor;
            if (doctor is null)
            {
                doctor = doctorService.DoctorRepo.FindBySpecialization(referral.Specialization)[0];
            }

            DateTime closestTimeForDoctor = doctor.getClosestFreeAppointment(15, referral.MedicalRecord.Patient);

            referral.Used = true;
            referralService.ReferralRepo.Serialize();

            int id = appointmentService.AppointmentRepo.GenerateId();
            Appointment appointment = new(id, closestTimeForDoctor, closestTimeForDoctor.AddMinutes(15), doctor,
                referral.MedicalRecord.Patient, null, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
            return AddAppointment(appointment);

        }
        public void DeleteAppointmens(Patient patient)
        {
            AppointmentService.AppointmentService appointmentService = new();

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
