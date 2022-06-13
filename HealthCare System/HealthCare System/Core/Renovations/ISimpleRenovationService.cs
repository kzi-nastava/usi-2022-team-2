using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface ISimpleRenovationService
    {
        void BookRenovation(SimpleRenovationDto simpleRenovationDto);
        void FinishSimpleRenovation(SimpleRenovation simpleRenovation);
        List<SimpleRenovation> SimpleRenovations();
        void StartSimpleRenovation(SimpleRenovation simpleRenovation);
        void TryToExecuteSimpleRenovations();
    }
}