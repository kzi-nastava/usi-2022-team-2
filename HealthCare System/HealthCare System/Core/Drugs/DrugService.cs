using System;
using System.Collections.Generic;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Drugs.Repository;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Prescriptions.Model;

namespace HealthCare_System.Core.Drugs
{
    public class DrugService : IDrugService
    {
        IDrugRepo drugRepo;
        IPrescriptionService prescriptionService;

        public DrugService(IDrugRepo drugRepo, IPrescriptionService prescriptionService)
        {
            this.drugRepo = drugRepo;
            this.prescriptionService = prescriptionService;
        }

        public IDrugRepo DrugRepo { get => drugRepo; }

        public List<Drug> Drugs()
        {
            return drugRepo.Drugs;
        }

        public void CreateNew(DrugDto drugDTO)
        {
            if (drugDTO.Name.Length > 30 || drugDTO.Name.Length < 5)
                throw new Exception();
            Drug drug = new Drug(drugDTO);
            drugRepo.Add(drug);
        }

        public void Update(DrugDto drugDto, Drug drug)
        {
            if (drugDto.Name.Length > 30 || drugDto.Name.Length < 5)
                throw new Exception();
            drug.Name = drugDto.Name;
            drug.Ingredients = drugDto.Ingredients;
            drug.Status = drugDto.Status;
            drug.Message = drugDto.Message;
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
