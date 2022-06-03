using System;
using System.Collections.Generic;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomServices;
using HealthCare_System.Services.EquipmentServices;

namespace HealthCare_System.Services.RenovationServices
{
    class SimpleRenovationService
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

        public SimpleRenovationRepo SimpleRenovationRepo { get => simpleRenovationRepo;}

        public List<SimpleRenovation> SimpleRenovations()
        {
            return simpleRenovationRepo.SimpleRenovations;
        }

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string newRoomName, TypeOfRoom newRoomType)
        {
            SimpleRenovation simpleRenovation = new SimpleRenovation(simpleRenovationRepo.GenerateId(), start, end,
                RenovationStatus.BOOKED, room, newRoomName, newRoomType);
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
            roomService.UpdateRoom(simpleRenovation.Room, simpleRenovation.NewRoomName, simpleRenovation.NewRoomType);
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
