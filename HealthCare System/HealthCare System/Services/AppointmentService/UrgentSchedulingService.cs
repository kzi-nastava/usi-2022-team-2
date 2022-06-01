using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.SchedulingService
{
    class UrgentSchedulingService
    {
        public Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration, Patient patient)
        {
            Dictionary<Appointment, DateTime> appointmentsDict = new Dictionary<Appointment, DateTime>();
            DateTime currentTime = DateTime.Now;
            Appointment currentAppointment;

            foreach (Appointment appointment in appointments)
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
