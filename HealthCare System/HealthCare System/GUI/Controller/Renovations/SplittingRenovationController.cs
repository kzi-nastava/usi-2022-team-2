using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Renovations
{
    class SplittingRenovationController
    {
        private readonly ISplittingRenovationService splittingRenovationService;

        public SplittingRenovationController(ISplittingRenovationService splittingRenovationService)
        {
            this.splittingRenovationService = splittingRenovationService;
        }

        public List<SplittingRenovation> SplittingRenovations()
        {
            return splittingRenovationService.SplittingRenovations();
        }

        public void BookRenovation(SplittingRenovationDto splittingRenovationDto)
        {
            splittingRenovationService.BookRenovation(splittingRenovationDto);
        }

        public void StartSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovationService.StartSplittingRenovation(splittingRenovation);
        }

        public void FinishSplittingRenovation(SplittingRenovation splittingRenovation)
        {
            splittingRenovationService.FinishSplittingRenovation(splittingRenovation);
        }

        public void TryToExecuteSplittingRenovations()
        {
            splittingRenovationService.TryToExecuteSplittingRenovations();

        }

        public SplittingRenovation FindById(int id)
        {
            return splittingRenovationService.FindById(id);
        }

        public int GenerateId()
        {
            return splittingRenovationService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv")
        {
            splittingRenovationService.Serialize();
        }
    }
}
