using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface ISplittingRenovationService
    {
        ISplittingRenovationRepo SplittingRenovationRepo { get; }

        void BookRenovation(SplittingRenovationDto splittingRenovationDto);

        void FinishSplittingRenovation(SplittingRenovation splittingRenovation);

        List<SplittingRenovation> SplittingRenovations();

        void StartSplittingRenovation(SplittingRenovation splittingRenovation);

        void TryToExecuteSplittingRenovations();

        SplittingRenovation FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv");
        
    }
}