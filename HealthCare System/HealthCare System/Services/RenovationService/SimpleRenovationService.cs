using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.RoomService;
using HealthCare_System.Services.EquipmentService;

namespace HealthCare_System.Services.RenovationService
{
    class SimpleRenovationService
    {
        SimpleRenovationRepo simpleRenovationRepo;
        RoomService.RoomService roomService;
        EquipmentTransferService equipmentTransferService;
        EquipmentService.EquipmentService equipmentService;

        public SimpleRenovationService()
        {
            SimpleRenovationRepoFactory simpleRenovationRepoFactory = new();
            simpleRenovationRepo = simpleRenovationRepoFactory.CreateSimpleRenovationRepository();
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
            roomService = new();
            equipmentTransferService = new();
            simpleRenovation.Status = RenovationStatus.ACTIVE;
            simpleRenovationRepo.Serialize();
            equipmentTransferService.MoveEquipmentToStorage(simpleRenovation.Room);
            roomService.RoomRepo.Serialize();
        }

        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            roomService = new();
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
