using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.DrugRepo;

namespace HealthCare_System.Services.DrugService
{
    class DrugService
    {
        DrugRepo drugRepo;

        public DrugService()
        {
            DrugRepoFactory drugRepoFactory = new();
            drugRepo = drugRepoFactory.CreateDrugRepository();
        }

        internal DrugRepo DrugRepo { get => drugRepo; }

        public void CreateNewDrug(string name, List<Ingredient> ingredients)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Drug drug = new Drug(GenerateId(), name, ingredients, DrugStatus.ON_HOLD, "");
            drugRepo.Add();
        }

        public void UpdateDrug(string name, List<Ingredient> ingredients, Drug drug)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            drug.Name = name;
            drug.Ingredients = ingredients;
            drug.Status = DrugStatus.ON_HOLD;
            drug.Message = "";
            Serialize();
        }
        //TODO: Move to DrugRequestService??
        public void RejectDrug(Drug drug, string message)
        {
            drug.Status = DrugStatus.REJECTED;
            drug.Message = message;
            Serialize();
        }

        public void AcceptDrug(Drug drug)
        {
            drug.Status = DrugStatus.ACCEPTED;
            drug.Message = "";
            Serialize();
        }

        public void DeleteDrug(Drug drug)
        {
            drugs.Remove(drug);
            Serialize();
        }

        public List<Drug> FillterOnHold()
        {
            List<Drug> filtered = new List<Drug>();

            foreach (Drug drug in drugs)
                if (drug.Status == DrugStatus.ON_HOLD)
                    filtered.Add(drug);

            return filtered;
        }

        public bool IsDrugAvailableForChange(Drug drug)
        {
            bool available = true;

            foreach (Prescription prescription in prescriptionController.Prescriptions)
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
