using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using System.Collections.Generic;

namespace HealthCare_System.gui.Controller
{
    class DrugController
    {
        private readonly IDrugService drugService;

        public DrugController(IDrugService drugService)
        {
            this.drugService = drugService;
        }

        public void AcceptDrug(Drug drug)
        {
            drugService.AcceptDrug(drug);
        }

        public void CreateNew(DrugDto drugDTO)
        {
            drugService.CreateNew(drugDTO);
        }

        public void Delete(Drug drug)
        {
            drugService.Delete(drug);
        }

        public List<Drug> Drugs()
        {
            return drugService.Drugs();
        }

        public bool IsDrugAvailableForChange(Drug drug)
        {
            return drugService.IsDrugAvailableForChange(drug);
        }

        public void RejectDrug(Drug drug, string message)
        {
            drugService.RejectDrug(drug, message);
        }

        public void Update(DrugDto drugDto, Drug drug)
        {
            drugService.Update(drugDto, drug);
        }

        public Drug FindById(int id)
        {
            return drugService.FindById(id);
        }

        public int GenerateId()
        {
            return drugService.GenerateId();
        }

        public void Serialize()
        {
            drugService.Serialize();
        }

        public List<Drug> FillterOnHold()
        {
            return drugService.FillterOnHold();
        }

        public List<Drug> FillterAccepted()
        {
            return drugService.FillterAccepted();
        }
    }
}
