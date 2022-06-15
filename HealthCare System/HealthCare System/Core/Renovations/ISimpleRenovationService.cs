using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public interface ISimpleRenovationService
    {
        ISimpleRenovationRepo SimpleRenovationRepo { get; }

        IRoomService RoomService { get; set; }

        IEquipmentTransferService EquipmentTransferService { get; set; }

        IEquipmentService EquipmentService { get; set; }

        void BookRenovation(SimpleRenovationDto simpleRenovationDto);

        void FinishSimpleRenovation(SimpleRenovation simpleRenovation);

        List<SimpleRenovation> SimpleRenovations();

        void StartSimpleRenovation(SimpleRenovation simpleRenovation);

        void TryToExecuteSimpleRenovations();

        SimpleRenovation FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv");
        
    }
}