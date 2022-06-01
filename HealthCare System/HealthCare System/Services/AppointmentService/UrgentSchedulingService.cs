using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.SchedulingService
{
    class UrgentSchedulingService
    {
        public Appointment BookClosestEmergancyAppointment(List<Doctor> doctors, Patient patient, int duration)
        {
            AppointmentService.AppointmentService appointmentService = new();

            DateTime limitTime = DateTime.Now.AddHours(2);
            DateTime start = limitTime;
            DateTime closestTimeForDoctor;
            Doctor doctor = doctors[0];
            foreach (Doctor doc in doctors)
            {
                closestTimeForDoctor = doc.getClosestFreeAppointment(duration, patient);
                if (closestTimeForDoctor < start)
                {
                    start = closestTimeForDoctor;
                    doctor = doc;
                }
            }

            if (limitTime == start)
            {
                return null;
            }
            AppointmentType type = Appointment.getTypeByDuration(duration);

            int id = appointmentService.AppointmentRepo.GenerateId();
            Appointment appointment = new Appointment(id, start, start.AddMinutes(duration), type, AppointmentStatus.BOOKED, false, true);
            appointment.Doctor = doctor;
            return appointment;
        }
        public Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration, Patient patient)
        {
            AppointmentService.AppointmentService appointmentService = new();

            Dictionary<Appointment, DateTime> appointmentsDict = new Dictionary<Appointment, DateTime>();
            DateTime currentTime = DateTime.Now;
            Appointment currentAppointment;

            foreach (Appointment appointment in appointmentService.AppointmentRepo.Appointments)
            {
                if (doctors.Contains(appointment.Doctor) && appointment.Start > currentTime && appointment.Start <= currentTime.AddHours(2))
                {
                    appointmentsDict[appointment] = appointment.Doctor.getNextFreeAppointment(appointment.Start, appointment.End);
                    while (!((appointment.Doctor.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration)) &&
                        appointment.Patient.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration)))))
                    {
                        if (appointment.Doctor.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration))
                            && !appointment.Patient.IsAvailable(appointmentsDict[appointment], appointmentsDict[appointment].AddMinutes(duration)))
                        {
                            appointmentsDict[appointment] = appointmentsDict[appointment].AddMinutes(duration);
                            continue;
                        }

                        appointmentsDict[appointment] = appointment.Doctor.getNextFreeAppointment(appointmentsDict[appointment],
                                                                                                  appointmentsDict[appointment].AddMinutes(duration));
                    }

                }
                if (appointmentsDict.Count == 5) { break; }
            }
            return appointmentsDict;
        }
    }
}
