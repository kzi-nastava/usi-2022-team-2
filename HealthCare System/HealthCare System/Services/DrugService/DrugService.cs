using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.DrugRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.PrescriptionService;

namespace HealthCare_System.Services.DrugService
{
    class DrugService
    {
        DrugRepo drugRepo;
        PrescriptionService.PrescriptionService prescriptionService;//?

        public DrugService()
        {
            DrugRepoFactory drugRepoFactory = new();
            drugRepo = drugRepoFactory.CreateDrugRepository();
        }

        internal DrugRepo DrugRepo { get => drugRepo; }

        public List<Drug> Drugs()
        {
            return drugRepo.Drugs;
        }

        public void CreateNewDrug(string name, List<Ingredient> ingredients)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Drug drug = new Drug(drugRepo.GenerateId(), name, ingredients, DrugStatus.ON_HOLD, "");
            drugRepo.Add(drug);
        }

        public void UpdateDrug(string name, List<Ingredient> ingredients, Drug drug)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            drug.Name = name;
            drug.Ingredients = ingredients;
            drug.Status = DrugStatus.ON_HOLD;
            drug.Message = "";
            drugRepo.Serialize();
        }
        //TODO: Move to DrugRequestService??
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

        public void DeleteDrug(Drug drug)
        {
            drugRepo.Delete(drug);
        }

        public List<Drug> FillterOnHold()
        {
            List<Drug> filtered = new List<Drug>();

            foreach (Drug drug in drugRepo.Drugs)
                if (drug.Status == DrugStatus.ON_HOLD)
                    filtered.Add(drug);

            return filtered;
        }

        public bool IsDrugAvailableForChange(Drug drug)
        {
            prescriptionService = new();
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
