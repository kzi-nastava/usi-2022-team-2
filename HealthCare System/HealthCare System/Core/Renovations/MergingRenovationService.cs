using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations.Model;
using HealthCare_System.Core.Renovations.Repository;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations
{
    public class MergingRenovationService : IMergingRenovationService
    {
        IMergingRenovationRepo mergingRenovationRepo;
        IRoomService roomService;
        IEquipmentTransferService equipmentTransferService;
        IEquipmentService equipmentService;

        public MergingRenovationService(IMergingRenovationRepo mergingRenovationRepo, IRoomService roomService,
            IEquipmentTransferService equipmentTransferService, IEquipmentService equipmentService)
        {
            this.mergingRenovationRepo = mergingRenovationRepo;
            this.roomService = roomService;
            this.equipmentTransferService = equipmentTransferService;
            this.equipmentService = equipmentService;
        }

        public IMergingRenovationRepo MergingRenovationRepo { get => mergingRenovationRepo; }

        public IRoomService RoomService { get => roomService; set => roomService = value; }

        public IEquipmentTransferService EquipmentTransferService { get => equipmentTransferService; set => equipmentTransferService = value; }

        public IEquipmentService EquipmentService { get => equipmentService; set => equipmentService = value; }

        public List<MergingRenovation> MergingRenovations()
        {
            return mergingRenovationRepo.MergingRenovations;
        }

        public void BookRenovation(MergingRenovationDto mergingRenovationDto)
        {
            MergingRenovation mergingRenovation = new MergingRenovation(mergingRenovationDto);
            mergingRenovationRepo.Add(mergingRenovation);
        }

        public void StartMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            Serialize();
            foreach (Room room in mergingRenovation.Rooms)
            {
                equipmentTransferService.MoveEquipmentToStorage(room);
            }
            roomService.Serialize();
        }

        public void FinishMergingRenovation(MergingRenovation mergingRenovation)
        {
            mergingRenovation.Status = RenovationStatus.ACTIVE;
            foreach (Room room in mergingRenovation.Rooms)
            {
                roomService.RemoveRoom(room);
            }
            Dictionary<Equipment, int> equipmentAmount = equipmentService.InitalizeEquipment();
            roomService.Create(mergingRenovation.NewRoomName, mergingRenovation.NewRoomType, equipmentAmount);
            mergingRenovationRepo.Delete(mergingRenovation);
        }

        public void TryToExecuteMergingRenovations()
        {
            if (MergingRenovations().Count > 0)
            {
                foreach (MergingRenovation mergingRenovation in MergingRenovations())
                {
                    if (DateTime.Now >= mergingRenovation.EndingDate)
                    {
                        FinishMergingRenovation(mergingRenovation);
                        return;
                    }

                    if (DateTime.Now >= mergingRenovation.BeginningDate &&
                        mergingRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartMergingRenovation(mergingRenovation);
                        return;
                    }
                }
            }

        }

        public MergingRenovation FindById(int id)
        {
            return mergingRenovationRepo.FindById(id);
        }

        public int GenerateId()
        {
            return mergingRenovationRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv")
        {
            mergingRenovationRepo.Serialize();
        }
    }
}
