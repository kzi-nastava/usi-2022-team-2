using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.EquipmentRepo
{
    class EquipmentRepoFactory
    {
        private EquipmentRepo equipmentRepo;

        public EquipmentRepo CreateEquipmentRepository()
        {
            if (equipmentRepo == null)
                equipmentRepo = new EquipmentRepo();

            return equipmentRepo;
        }
    }
}
