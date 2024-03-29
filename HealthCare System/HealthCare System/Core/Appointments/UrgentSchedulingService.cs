﻿using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Model;
using HealthCare_System.Model.Core.Appointments.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public class UrgentSchedulingService : IUrgentSchedulingService
    {
        IAppointmentService appointmentService;
        ISchedulingService schedulingService;

        public IAppointmentService AppointmentService { get => appointmentService; set => appointmentService = value; }
        public ISchedulingService SchedulingService { get => schedulingService; set => schedulingService = value; }

        public UrgentSchedulingService(IAppointmentService appointmentService, ISchedulingService schedulingService)
        {
            this.appointmentService = appointmentService;
            this.schedulingService = schedulingService;
        }

        public Appointment ReplaceAppointment(Appointment toReplaceAppointment, UrgentAppointmentDto urgentAppointmentDto)
        {
            AppointmentType type = Appointment.getTypeByDuration(urgentAppointmentDto.Duration);
            int id = appointmentService.GenerateId();
            AppointmentDto appointmentDto = new AppointmentDto(id, toReplaceAppointment.Start, toReplaceAppointment.End, urgentAppointmentDto.Doctor,
                urgentAppointmentDto.Patient, null, type, AppointmentStatus.BOOKED, null, false, true);

            toReplaceAppointment.Start = urgentAppointmentDto.DelayedStart;
            toReplaceAppointment.End = urgentAppointmentDto.DelayedEnd;

            return schedulingService.AddAppointment(appointmentDto);
        }

        public void BookClosestEmergancyAppointment(UrgentAppointmentDto urgentAppointmentDto)
        {

            DateTime limitTime = DateTime.Now.AddHours(2);
            DateTime start = limitTime;
            DateTime closestTimeForDoctor;
            Doctor doctor = urgentAppointmentDto.Doctors[0];
            foreach (Doctor doc in urgentAppointmentDto.Doctors)
            {
                closestTimeForDoctor = doc.getClosestFreeAppointment(urgentAppointmentDto.Duration, urgentAppointmentDto.Patient);
                if (closestTimeForDoctor < start)
                {
                    start = closestTimeForDoctor;
                    doctor = doc;
                }
            }

            if (limitTime == start)
            {
                throw new Exception("There is no available appointment in next 2h. Select one booked to be replaced.");
            }

            AppointmentType type = Appointment.getTypeByDuration(urgentAppointmentDto.Duration);
            int id = appointmentService.GenerateId();
            AppointmentDto appointmentDto = new AppointmentDto(id, start, start.AddMinutes(urgentAppointmentDto.Duration), doctor, urgentAppointmentDto.Patient, null,
                type, AppointmentStatus.BOOKED, null, false, true);

            Appointment _ = schedulingService.AddAppointment(appointmentDto);
        }

        public Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration,
            Patient patient)
        {

            Dictionary<Appointment, DateTime> appointmentsDict = new Dictionary<Appointment, DateTime>();
            DateTime currentTime = DateTime.Now;
            Appointment currentAppointment;

            foreach (Appointment appointment in appointmentService.Appointments())
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
