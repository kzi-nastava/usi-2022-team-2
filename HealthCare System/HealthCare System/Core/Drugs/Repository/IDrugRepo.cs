using HealthCare_System.Core.Drugs.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Drugs.Repository
{
    public interface IDrugRepo
    {
        string Path { get; set; }

        List<Drug> Drugs { get; set; }
        void Add(Drug drug);

        void Delete(Drug drug);

        List<Drug> FillterAccepted();

        List<Drug> FillterOnHold();

        Drug FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/Drug_Ingredient.csv");
    }
}