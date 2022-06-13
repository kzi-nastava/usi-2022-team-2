using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public class SimpleRenovationService : ISimpleRenovationService
    {
        SimpleRenovationRepo simpleRenovationRepo;
        RoomService roomService;
        EquipmentTransferService equipmentTransferService;
        EquipmentService equipmentService;

        public SimpleRenovationService(SimpleRenovationRepo simpleRenovationRepo, RoomService roomService,
            EquipmentTransferService equipmentTransferService, EquipmentService equipmentService)
        {
            this.simpleRenovationRepo = simpleRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public SimpleRenovationRepo SimpleRenovationRepo { get => simpleRenovationRepo; }

        public List<SimpleRenovation> SimpleRenovations()
        {
            return simpleRenovationRepo.SimpleRenovations;
        }

        public void BookRenovation(SimpleRenovationDto simpleRenovationDto)
        {
            SimpleRenovation simpleRenovation = new SimpleRenovation(simpleRenovationDto);
            simpleRenovationRepo.Add(simpleRenovation);
        }

        public void StartSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.ACTIVE;
            simpleRenovationRepo.Serialize();
            equipmentTransferService.MoveEquipmentToStorage(simpleRenovation.Room);
            roomService.RoomRepo.Serialize();
        }

        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.FINISHED;
            roomService.Update(simpleRenovation.Room, simpleRenovation.NewRoomName, simpleRenovation.NewRoomType);
            simpleRenovationRepo.Delete(simpleRenovation);
        }

        public void TryToExecuteSimpleRenovations()
        {
            if (simpleRenovationRepo.SimpleRenovations.Count > 0)
            {
                foreach (SimpleRenovation simpleRenovation in simpleRenovationRepo.SimpleRenovations)
                {
                    if (DateTime.Now >= simpleRenovation.EndingDate)
                    {
                        FinishSimpleRenovation(simpleRenovation);
                        return;
                    }

                    if (DateTime.Now >= simpleRenovation.BeginningDate &&
                        simpleRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartSimpleRenovation(simpleRenovation);
                        return;
                    }
                }
            }

        }
    }
}
