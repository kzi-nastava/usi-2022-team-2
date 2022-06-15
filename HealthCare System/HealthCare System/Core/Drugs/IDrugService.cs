using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Drugs.Repository;
using HealthCare_System.Core.Prescriptions;
using System.Collections.Generic;

namespace HealthCare_System.Core.Drugs
{
    public interface IDrugService
    {
        IDrugRepo DrugRepo { get; }

        public IPrescriptionService PrescriptionService { get; set; }

        void AcceptDrug(Drug drug);

        void CreateNew(DrugDto drugDTO);

        void Delete(Drug drug);

        List<Drug> Drugs();

        bool IsDrugAvailableForChange(Drug drug);

        void RejectDrug(Drug drug, string message);

        void Update(DrugDto drugDto, Drug drug);

        Drug FindById(int id);

        int GenerateId();

        void Serialize();

        List<Drug> FillterOnHold();

        List<Drug> FillterAccepted();
    }
}