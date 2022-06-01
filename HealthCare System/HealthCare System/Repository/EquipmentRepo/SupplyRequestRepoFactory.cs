using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.EquipmentRepo
{
    class SupplyRequestRepoFactory
    {
        private SupplyRequestRepo supplyRequestRepo;

        public SupplyRequestRepo CreateSupplyRequestRepository()
        {
            if (supplyRequestRepo == null)
                supplyRequestRepo = new SupplyRequestRepo();

            return supplyRequestRepo;
        }
    }
}
