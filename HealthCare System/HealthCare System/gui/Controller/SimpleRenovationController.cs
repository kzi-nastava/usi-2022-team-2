using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Renovations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.gui.Controller
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
