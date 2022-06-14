using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Rooms.Repository;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Rooms
{
    public interface IRoomService
    {
        IRoomRepo RoomRepo { get ; }
        
        void Create(RoomDto roomDto);
        void Create(string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount);
        void Delete(Room room);
        bool IsRoomAvailable(Room room);
        bool IsRoomAvailableAppointments(Room room);
        bool IsRoomAvailableAtAllMerging(Room room);
        bool IsRoomAvailableAtAllSimple(Room room);
        bool IsRoomAvailableAtAllSplitting(Room room);
        bool IsRoomAvailableAtTimeMerging(Room room, DateTime time);
        bool IsRoomAvailableAtTimeSimple(Room room, DateTime time);
        bool IsRoomAvailableAtTimeSplitting(Room room, DateTime time);
        bool IsRoomAvailableForChange(Room room);
        bool IsRoomAvailableRenovationsAtAll(Room room);
        bool IsRoomAvailableRenovationsAtTime(Room room, DateTime time);
        void RemoveRoom(Room room);
        List<Room> Rooms();
        Room Storage();
        void Update(Room room, RoomDto roomDto);
        void Update(Room room, string name, TypeOfRoom type);
    }
}