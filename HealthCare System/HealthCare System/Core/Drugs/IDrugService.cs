using HealthCare_System.Core.Drugs.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Drugs
{
    public interface IDrugService
    {
        void AcceptDrug(Drug drug);

        void CreateNew(DrugDto drugDTO);

        void Delete(Drug drug);

        List<Drug> Drugs();

        bool IsDrugAvailableForChange(Drug drug);

        void RejectDrug(Drug drug, string message);

        void Update(DrugDto drugDto, Drug drug);
    }
}