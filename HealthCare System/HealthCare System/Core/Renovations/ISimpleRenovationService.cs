using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface ISimpleRenovationService
    {
        ISimpleRenovationRepo SimpleRenovationRepo { get; }

        void BookRenovation(SimpleRenovationDto simpleRenovationDto);

        void FinishSimpleRenovation(SimpleRenovation simpleRenovation);

        List<SimpleRenovation> SimpleRenovations();

        void StartSimpleRenovation(SimpleRenovation simpleRenovation);

        void TryToExecuteSimpleRenovations();
    }
}