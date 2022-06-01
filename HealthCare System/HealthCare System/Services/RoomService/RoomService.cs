using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using HealthCare_System.Repository.RenovationRepo;
using HealthCare_System.Repository.RoomRepo;
using HealthCare_System.Services.EquipmentService;

namespace HealthCare_System.Services.RoomService
{
    class RoomService
    {
        MergingRenovationRepo mergingRenovationRepo;
        SimpleRenovationRepo simpleRenovationRepo;
        SplittingRenovationRepo splittingRenovationRepo;
        RoomRepo roomRepo;
        
        EquipmentTransferService equipmentTransferService;

        public RoomService()
        {
            equipmentTransferService = new();

            MergingRenovationRepoFactory mergingRenovationRepoFactory = new();
            mergingRenovationRepo = mergingRenovationRepoFactory.CreateMergingRenovationRepository();

            SimpleRenovationRepoFactory simpleRenovationRepoFactory = new();
            simpleRenovationRepo = simpleRenovationRepoFactory.CreateSimpleRenovationRepository();

            SplittingRenovationRepoFactory splittingRenovationRepoFactory = new();
            splittingRenovationRepo = splittingRenovationRepoFactory.CreateSplittingRenovationRepository();

            RoomRepoFactory roomRepoFactory = new();
            roomRepo = roomRepoFactory.CreateRoomRepository();
        }

        public bool IsRoomAvailableAtAllMerging(Room room)
        {
            bool available = true;
            foreach (MergingRenovation mergingRenovation in mergingRenovationRepo.MergingRenovations)
            {
                foreach (Room roomInMerging in mergingRenovation.Rooms)
                {
                    if (room == roomInMerging)
                    {
                        available = false;
                        return available;
                    }
                }
            }
            return available;
        }

        public bool IsRoomAvailableAtTimeMerging(Room room, DateTime time)
        {
            bool available = true;
            foreach (MergingRenovation mergingRenovation in mergingRenovationRepo.MergingRenovations)
            {
                foreach (Room roomInMerging in mergingRenovation.Rooms)
                {
                    if (room == roomInMerging && time.AddMinutes(15) >= mergingRenovation.BeginningDate)
                    {
                        available = false;
                        return available;
                    }
                }
            }
            return available;
        }

        public void RoomTypeFilter(string roomType, Dictionary<Equipment, int> equipmentAmount)
        {
            foreach (Room room in roomRepo.Rooms)
            {
                if (roomType != room.Type.ToString())
                {
                    foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
                    {
                        equipmentAmount[equipmentAmountEntry.Key] -= room.EquipmentAmount[equipmentAmountEntry.Key];
                    }
                }
            }
        }

        public void CreateNewRoom(string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();

            Room newRoom = new Room(roomRepo.GenerateId(), name, type, equipmentAmount);
            roomRepo.Add(newRoom);
        }

        public void UpdateRoom(Room room, string name, TypeOfRoom type)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            room.Name = name;
            room.Type = type;
            roomRepo.Serialize();
        }

        public void DeleteRoom(Room room)
        {
            equipmentTransferService.MoveEquipmentToStorage(room);
            roomRepo.Delete(room);
        }

        public bool IsRoomAvailableAtAllSimple(Room room)
        {
            bool available = true;
            foreach (SimpleRenovation simpleRenovation in simpleRenovationRepo.SimpleRenovations)
            {
                if (room == simpleRenovation.Room)
                {
                    available = false;
                    return available;
                }
            }
            return available;
        }

        public bool IsRoomAvailableAtTimeSimple(Room room, DateTime time)
        {
            bool available = true;
            foreach (SimpleRenovation simpleRenovation in simpleRenovationRepo.SimpleRenovations)
            {
                if (room == simpleRenovation.Room && time.AddMinutes(15) >= simpleRenovation.BeginningDate)
                {
                    available = false;
                    return available;
                }
            }
            return available;
        }

        public bool IsRoomAvailableAtAllSplitting(Room room)
        {
            bool available = true;
            foreach (SplittingRenovation splittingRenovation in splittingRenovationRepo.SplittingRenovations)
            {
                if (room == splittingRenovation.Room)
                {
                    available = false;
                    return available;
                }
            }
            return available;
        }

        public bool IsRoomAvailableAtTimeSplitting(Room room, DateTime time)
        {
            bool available = true;
            foreach (SplittingRenovation splittingRenovation in splittingRenovationRepo.SplittingRenovations)
            {
                if (room == splittingRenovation.Room && time.AddMinutes(15) >= splittingRenovation.BeginningDate)
                {
                    available = false;
                    return available;
                }
            }
            return available;
        }

        public bool IsRoomAvailable(Room room)
        {
            bool available = true;
            foreach (Transfer transfer in transfers)
            {
                if (room == transfer.FromRoom || room == transfer.ToRoom)
                {
                    available = false;
                    break;
                }
            }
            return available;
        }

        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {

            List<Room> rooms = new List<Room>();


            foreach (Room room in roomController.GetRoomsByType(type))
                if (IsRoomAvailableRenovationsAtTime(room, start))
                    rooms.Add(room);

            foreach (Appointment appointment in appointmentController.Appointments)
            {

                if (rooms.Contains(appointment.Room) && ((appointment.Start <= start && appointment.End >= start) ||
                    (appointment.Start <= end && appointment.End >= end) ||
                    (start <= appointment.Start && end >= appointment.End)))
                {
                    rooms.Remove(appointment.Room);
                }
            }

            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms[0];
        }
    }
    

}
