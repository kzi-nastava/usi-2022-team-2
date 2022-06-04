using System;
using System.Collections.Generic;
using HealthCare_System.Repository.DrugRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.PrescriptionServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.Services.DrugServices
{
    class DrugService
    {
        DrugRepo drugRepo;
        PrescriptionService prescriptionService;

        public DrugService(DrugRepo drugRepo, PrescriptionService prescriptionService)
        {
            this.drugRepo = drugRepo;
            this.prescriptionService = prescriptionService;
        }

        internal DrugRepo DrugRepo { get => drugRepo; }

        public List<Drug> Drugs()
        {
            return drugRepo.Drugs;
        }

        public void CreateNew(DrugDTO drugDTO)
        {
            if (drugDTO.Name.Length > 30 || drugDTO.Name.Length < 5)
                throw new Exception();
            Drug drug = new Drug(drugDTO);
            drugRepo.Add(drug);
        }

        public void Update(DrugDTO drugDTO, Drug drug)
        {
            if (drugDTO.Name.Length > 30 || drugDTO.Name.Length < 5)
                throw new Exception();
            drug.Name = drugDTO.Name;
            drug.Ingredients = drugDTO.Ingredients;
            drug.Status = drugDTO.Status;
            drug.Message = drugDTO.Message;
            drugRepo.Serialize();
        }

        public void RejectDrug(Drug drug, string message)
        {
            drug.Status = DrugStatus.REJECTED;
            drug.Message = message;
            drugRepo.Serialize();
        }

        public void AcceptDrug(Drug drug)
        {
            drug.Status = DrugStatus.ACCEPTED;
            drug.Message = "";
            drugRepo.Serialize();
        }

        public void Delete(Drug drug)
        {
            drugRepo.Delete(drug);
        }

        public bool IsDrugAvailableForChange(Drug drug)
        {
            bool available = true;

            foreach (Prescription prescription in prescriptionService.Prescriptions())
            {
                if (prescription.Drug == drug)
                {
                    available = false;
                    break;
                }
            }

            return available;
        }
    }
}
