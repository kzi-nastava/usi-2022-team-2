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
        ISimpleRenovationRepo simpleRenovationRepo;
        IRoomService roomService;
        IEquipmentTransferService equipmentTransferService;
        IEquipmentService equipmentService;

        public SimpleRenovationService(ISimpleRenovationRepo simpleRenovationRepo, IRoomService roomService,
            IEquipmentTransferService equipmentTransferService, IEquipmentService equipmentService)
        {
            this.simpleRenovationRepo = simpleRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public ISimpleRenovationRepo SimpleRenovationRepo { get => simpleRenovationRepo; }

        public IRoomService RoomService { get => roomService; set => roomService = value; }

        public IEquipmentTransferService EquipmentTransferService { get => equipmentTransferService; set => equipmentTransferService = value; }

        public IEquipmentService EquipmentService { get => equipmentService; set => equipmentService = value; }

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
            Serialize();
            equipmentTransferService.MoveEquipmentToStorage(simpleRenovation.Room);
            roomService.Serialize();
        }

        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.FINISHED;
            roomService.Update(simpleRenovation.Room, simpleRenovation.NewRoomName, simpleRenovation.NewRoomType);
            simpleRenovationRepo.Delete(simpleRenovation);
        }

        public void TryToExecuteSimpleRenovations()
        {
            if (SimpleRenovations().Count > 0)
            {
                foreach (SimpleRenovation simpleRenovation in SimpleRenovations())
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

        public SimpleRenovation FindById(int id)
        {
            return simpleRenovationRepo.FindById(id);
        }

        public int GenerateId()
        {
            return simpleRenovationRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv")
        {
            simpleRenovationRepo.Serialize();
        }
    }
}
