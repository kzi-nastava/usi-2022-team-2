using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class RoomController
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService; 
        }

        public List<Room> Rooms()
        {
            return roomService.Rooms();
        }

        public Room Storage()
        {
            return roomService.Storage();
        }

        public bool IsRoomAvailableAtAllMerging(Room room)
        {
            return roomService.IsRoomAvailableAtAllMerging(room);
        }

        public bool IsRoomAvailableAtTimeMerging(Room room, DateTime time)
        {
            return roomService.IsRoomAvailableAtTimeMerging(room, time);
        }

        public void Create(string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount)
        {
            roomService.Create(name, type, equipmentAmount);
        }

        public void Create(RoomDto roomDto)
        {
            roomService.Create(roomDto);
        }

        public void Update(Room room, string name, TypeOfRoom type)
        {
            roomService.Update(room, name, type);
        }

        public void Update(Room room, RoomDto roomDto)
        {
            roomService.Update(room, roomDto);
        }

        public void Delete(Room room)
        {
            roomService.Delete(room);
        }

        public bool IsRoomAvailableAtAllSimple(Room room)
        {
            return roomService.IsRoomAvailableAtAllSimple(room);
        }

        public bool IsRoomAvailableAtTimeSimple(Room room, DateTime time)
        {
            return roomService.IsRoomAvailableAtTimeSimple(room, time);
        }

        public bool IsRoomAvailableAtAllSplitting(Room room)
        {
            return roomService.IsRoomAvailableAtAllSplitting(room);
        }

        public bool IsRoomAvailableAtTimeSplitting(Room room, DateTime time)
        {
            return roomService.IsRoomAvailableAtTimeSplitting(room, time);
        }

        public bool IsRoomAvailable(Room room)
        {
            return roomService.IsRoomAvailable(room);
        }

        public bool IsRoomAvailableForChange(Room room)
        {
            return roomService.IsRoomAvailableForChange(room);
        }

        public void RemoveRoom(Room room)
        {
            roomService.RemoveRoom(room);
        }

        public bool IsRoomAvailableAppointments(Room room)
        {
            return roomService.IsRoomAvailableAppointments(room);
        }

        public bool IsRoomAvailableRenovationsAtAll(Room room)
        {
            return roomService.IsRoomAvailableRenovationsAtAll(room);
        }

        public bool IsRoomAvailableRenovationsAtTime(Room room, DateTime time)
        {
            return roomService.IsRoomAvailableRenovationsAtTime(room, time);
        }

        public Room FindById(int id)
        {
            return roomService.FindById(id);
        }

        public void Serialize(string linkPath = "../../../data/links/Room_equipment.csv")
        {
            roomService.Serialize(linkPath);
        }

        public int GenerateId()
        {
            return roomService.GenerateId();
        }

        //TODO: CHANGE TO FIND NOT GET
        public List<Room> GetRoomsByType(AppointmentType type)
        {
            return roomService.GetRoomsByType(type);
        }
    }
}
