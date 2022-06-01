using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.EquipmentRepo
{
    class EquipmentTransferRepoFactory
    {
        private EquipmentTransferRepo equipmentTransferRepo;

        public EquipmentTransferRepo CreateEquipmentTransferRepository()
        {
            if (equipmentTransferRepo == null)
                equipmentTransferRepo = new EquipmentTransferRepo();

            return equipmentTransferRepo;
        }
    }
}
