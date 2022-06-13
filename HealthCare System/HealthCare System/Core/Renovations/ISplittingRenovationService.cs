using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface ISplittingRenovationService
    {
        void BookRenovation(SplittingRenovationDto splittingRenovationDto);
        void FinishSplittingRenovation(SplittingRenovation splittingRenovation);
        List<SplittingRenovation> SplittingRenovations();
        void StartSplittingRenovation(SplittingRenovation splittingRenovation);
        void TryToExecuteSplittingRenovations();
    }
}