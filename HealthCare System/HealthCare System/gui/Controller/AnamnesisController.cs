using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.Anamneses.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.gui.Controller
{
    class AnamnesisController
    {
        IAnamnesisService anamnesisService;

        public AnamnesisController(IAnamnesisService anamnesisService)
        {
            this.anamnesisService = anamnesisService;
        }

        public List<Anamnesis> Anamneses()
        {
            return anamnesisService.Anamneses();
        }

        public Anamnesis FindById(int id)
        {
            return anamnesisService.FindById(id);
        }

        public void Serialize()
        {
            anamnesisService.Serialize();
        }

        public int GenerateId()
        {
            return anamnesisService.GenerateId();
        }

        public void Update(int id, string description)
        {
            anamnesisService.Update(id, description);
        }
    }
}
