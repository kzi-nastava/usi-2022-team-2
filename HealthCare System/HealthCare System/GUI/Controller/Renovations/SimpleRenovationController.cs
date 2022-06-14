using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;


namespace HealthCare_System.GUI.Controller.Renovations
{
    class SimpleRenovationController
    {
        private readonly ISimpleRenovationService simpleRenovationService;

        public SimpleRenovationController(ISimpleRenovationService simpleRenovationService)
        {
            this.simpleRenovationService = simpleRenovationService;
        }

        public List<SimpleRenovation> SimpleRenovations()
        {
            return simpleRenovationService.SimpleRenovations();
        }

        public void BookRenovation(SimpleRenovationDto simpleRenovationDto)
        {
            simpleRenovationService.BookRenovation(simpleRenovationDto);
        }

        public void StartSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovationService.StartSimpleRenovation(simpleRenovation);
        }

        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovationService.FinishSimpleRenovation(simpleRenovation);
        }

        public void TryToExecuteSimpleRenovations()
        {
            simpleRenovationService.TryToExecuteSimpleRenovations();
        }

        public SimpleRenovation FindById(int id)
        {
            return simpleRenovationService.FindById(id);
        }

        public int GenerateId()
        {
            return simpleRenovationService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv")
        {
            simpleRenovationService.Serialize();
        }
    }
}
