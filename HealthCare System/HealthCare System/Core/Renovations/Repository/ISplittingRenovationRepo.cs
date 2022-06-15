using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations.Repository
{
    public interface ISplittingRenovationRepo
    {
        List<SplittingRenovation> SplittingRenovations { get; set; }

        void Add(SplittingRenovation splittingRenovation);

        void Delete(SplittingRenovation splittingRenovation);

        SplittingRenovation FindById(int id);

        int GenerateId();

        void Load();

        void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv");
    }
}