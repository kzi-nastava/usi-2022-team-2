using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Prescriptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.gui.Controller
{
    class PrescriptionController
    {
        private readonly IPrescriptionService prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }

        public List<Prescription> Prescriptions()
        {
            return prescriptionService.Prescriptions();
        }

        public void DeletePrescriptions(MedicalRecord medicalRecord)
        {
            prescriptionService.DeletePrescriptions(medicalRecord);
        }

        public void AddPrescrition(PrescriptionDto prescriptionDto)
        {
            prescriptionService.AddPrescrition(prescriptionDto);
        }

        public Prescription FindById(int id)
        {
            return prescriptionService.FindById(id);
        }

        public int GenerateId()
        {
            return prescriptionService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/PrescriptionLinker.csv")
        {
            prescriptionService.Serialize();
        }
    }
}
