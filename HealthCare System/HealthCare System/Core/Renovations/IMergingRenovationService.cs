using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface IMergingRenovationService
    {
        IMergingRenovationRepo MergingRenovationRepo { get; }

        void BookRenovation(MergingRenovationDto mergingRenovationDto);

        void FinishMergingRenovation(MergingRenovation mergingRenovation);

        List<MergingRenovation> MergingRenovations();

        void StartMergingRenovation(MergingRenovation mergingRenovation);

        void TryToExecuteMergingRenovations();

        MergingRenovation FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv");
        
    }
}