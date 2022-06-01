using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;

namespace HealthCare_System.Services.SchedulingService
{
    class SchedulingService
    {
        
        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {

            List<Room> rooms = new List<Room>();


            foreach (Room room in roomController.GetRoomsByType(type))
                if (IsRoomAvailableRenovationsAtTime(room, start))
                    rooms.Add(room);

            foreach (Appointment appointment in appointmentController.Appointments)
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
            int anamnesisId = anamnesisController.GenerateId();
            Anamnesis anamnesis = new(anamnesisId, "");
            appointment.Anamnesis = anamnesis;

            Room room = AvailableRoom(appointment.Type, appointment.Start, appointment.End);
            appointment.Room = room;

            appointment.Validate();

            appointmentController.Appointments.Add(appointment);
            appointment.Doctor.Appointments.Add(appointment);
            appointment.Patient.MedicalRecord.Appointments.Add(appointment);
            anamnesisController.Anamneses.Add(appointment.Anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();

            return appointment;

        }
        public void UpdateAppointment(Appointment newAppointment)
        {
            newAppointment.Validate();
            Appointment appointment = appointmentController.FindById(newAppointment.Id);

            appointment.Start = newAppointment.Start;
            appointment.End = newAppointment.End;
            appointment.Doctor = newAppointment.Doctor;
            appointment.Patient = newAppointment.Patient;
            appointment.Status = newAppointment.Status;
            appointmentController.Serialize();

        }

        public void DeleteAppointment(int id)
        {
            Appointment appointment = appointmentController.FindById(id);
            if (appointment is null)
            {
                throw new Exception("Appointment is not found!");
            }
            appointmentController.Appointments.Remove(appointment);
            appointment.Doctor.Appointments.Remove(appointment);
            appointment.Patient.MedicalRecord.Appointments.Remove(appointment);
            anamnesisController.Anamneses.Remove(appointment.Anamnesis);
            appointmentController.Serialize();
            anamnesisController.Serialize();
        }
        public Appointment BookAppointmentByReferral(Referral referral)
        {
            Doctor doctor = referral.Doctor;
            if (doctor is null)
            {
                doctor = doctorController.FindBySpecialization(referral.Specialization)[0];
            }

            DateTime closestTimeForDoctor = doctor.getClosestFreeAppointment(15, referral.MedicalRecord.Patient);

            referral.Used = true;
            referralController.Serialize();

            int id = appointmentController.GenerateId();
            Appointment appointment = new(id, closestTimeForDoctor, closestTimeForDoctor.AddMinutes(15), doctor,
                referral.MedicalRecord.Patient, null, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
            return AddAppointment(appointment);

        }
        private void DeleteAppointmens(Patient patient)
        {
            for (int i = appointmentController.Appointments.Count - 1; i >= 0; i--)
            {
                if (appointmentController.Appointments[i].Patient == patient)
                {
                    if (appointmentController.Appointments[i].Start > DateTime.Now)
                    {
                        throw new Exception("Can't delete selected patient, because of it's future appointments.");
                    }
                    DeleteAppointment(appointmentController.Appointments[i].Id);
                }
            }
        }
    }
}
