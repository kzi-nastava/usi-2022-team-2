using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    class UrgentAppointmentDto
    {
        List<Doctor> doctors;
        Doctor doctor;
        Patient patient;
        int duration;
        DateTime delayedStart;
        DateTime delayedEnd;

        public UrgentAppointmentDto(Doctor doctor, Patient patient, int duration)
        {
            this.patient = patient;
            this.doctor = doctor;
            this.duration = duration;
        }

        public UrgentAppointmentDto(List<Doctor> doctors, Patient patient, int duration)
        {
            this.Doctors = doctors;
            this.Patient = patient;
            this.Duration = duration;
        }

        public List<Doctor> Doctors { get => doctors; set => doctors = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public int Duration { get => duration; set => duration = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public DateTime DelayedStart { get => delayedStart; set => delayedStart = value; }
        public DateTime DelayedEnd { get => delayedEnd; set => delayedEnd = value; }
    }
}
