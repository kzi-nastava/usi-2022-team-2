using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface IMergingRenovationService
    {
        void BookRenovation(MergingRenovationDto mergingRenovationDto);
        void FinishMergingRenovation(MergingRenovation mergingRenovation);
        List<MergingRenovation> MergingRenovations();
        void StartMergingRenovation(MergingRenovation mergingRenovation);
        void TryToExecuteMergingRenovations();
    }
}