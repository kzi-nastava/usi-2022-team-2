using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface IMergingRenovationService
    {
        IMergingRenovationRepo MergingRenovationRepo { get; }

        IRoomService RoomService { get; set; }

        IEquipmentTransferService EquipmentTransferService { get; set; }

        IEquipmentService EquipmentService { get; set; }


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