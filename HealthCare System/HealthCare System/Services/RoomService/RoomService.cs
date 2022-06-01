using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.RoomService
{
    class RoomService
    {
        public bool IsRoomAvailableAtAll(Room room)
        {
            bool available = true;
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
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

        public bool IsRoomAvailableAtTime(Room room, DateTime time)
        {
            bool available = true;
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
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
    }
    public void RoomTypeFilter(string roomType, Dictionary<Equipment, int> equipmentAmount)
    {
        foreach (Room room in rooms)
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

        Room newRoom = new Room(GenerateId(), name, type, equipmentAmount);
        rooms.Add(newRoom);
        Serialize();
    }

    public void UpdateRoom(Room room, string name, TypeOfRoom type)
    {
        if (name.Length > 30 || name.Length < 5)
            throw new Exception();
        room.Name = name;
        room.Type = type;
        Serialize();
    }

    public void DeleteRoom(Room room)
    {
        MoveEquipmentToStorage(room);
        rooms.Remove(room);
        Serialize();
    }

    public bool IsRoomAvailableAtAll(Room room)
    {
        bool available = true;
        foreach (SimpleRenovation simpleRenovation in simpleRenovations)
        {
            if (room == simpleRenovation.Room)
            {
                available = false;
                return available;
            }
        }
        return available;
    }

    public bool IsRoomAvailableAtTime(Room room, DateTime time)
    {
        bool available = true;
        foreach (SimpleRenovation simpleRenovation in simpleRenovations)
        {
            if (room == simpleRenovation.Room && time.AddMinutes(15) >= simpleRenovation.BeginningDate)
            {
                available = false;
                return available;
            }
        }
        return available;
    }
    public bool IsRoomAvailableAtAll(Room room)
    {
        bool available = true;
        foreach (SplittingRenovation splittingRenovation in splittingRenovations)
        {
            if (room == splittingRenovation.Room)
            {
                available = false;
                return available;
            }
        }
        return available;
    }

    public bool IsRoomAvailableAtTime(Room room, DateTime time)
    {
        bool available = true;
        foreach (SplittingRenovation splittingRenovation in splittingRenovations)
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

}
