using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions.Model;
using HealthCare_System.Core.Prescriptions.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Prescriptions
{
    public interface IPrescriptionService
    {
        PrescriptionRepo PrescriptionRepo { get; }

        void AddPrescrition(PrescriptionDto prescriptionDto);
        void DeletePrescriptions(MedicalRecord medicalRecord);
        List<Prescription> Prescriptions();
    }
}