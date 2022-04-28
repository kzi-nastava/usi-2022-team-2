using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;
using System;

namespace HealthCare_System.controllers
{
     class RoomController
    {
        List<Room> rooms;
        string path;

        public RoomController() 
        {
            path = "data/entities/Rooms.json";
            Load();
        }

        public RoomController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Room> Rooms { get => rooms; set => rooms = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        { 
            rooms = JsonSerializer.Deserialize<List<Room>>(File.ReadAllText(path));
        }

        public Room FindById(int id)
        {
            foreach (Room room in rooms)
                if (room.Id == id)
                    return room;
            return null;
        }

        public void Serialize()
        {
            string roomsJson = JsonSerializer.Serialize(rooms, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, roomsJson);
        }

        int GenerateId()
        {
            return rooms[rooms.Count - 1].Id + 1;
        }

        public List<Room> GetRoomsByType(AppointmentType type)
        {
            List<Room> filteredRooms = new List<Room>();
            TypeOfRoom typeOfRoom = TypeOfRoom.EXAMINATION_HALL;
            if (type == AppointmentType.OPERATION)
            {
                typeOfRoom = TypeOfRoom.OPERATION_HALL;
            }
            foreach (Room room in rooms)
            {
                if (room.Type == typeOfRoom)
                {
                    filteredRooms.Add(room);
                }
            }
            return rooms;
        }

        //Did this in filtering
        public Dictionary<Equipment, int> GetEquipmentFromAllRooms()
        {
            Dictionary<Equipment, int> equipmentAmountAllRooms = new Dictionary<Equipment, int>();
            foreach (Room room in rooms)
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountRoom in room.EquipmentAmount)
                {
                    if (equipmentAmountAllRooms.ContainsKey(equipmentAmountRoom.Key))
                    {
                        equipmentAmountAllRooms[equipmentAmountRoom.Key] += equipmentAmountRoom.Value;
                    }
                    else
                    {
                        equipmentAmountAllRooms[equipmentAmountRoom.Key] = equipmentAmountRoom.Value;
                    }
                }
            }
            return equipmentAmountAllRooms;
        }

        //Did this in filtering
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

        public Room CreateNewRoom(string name, TypeOfRoom type)
        {
            if (name.Length == 0)
                throw new Exception("Name cannot be empty");
            else if (name.Length > 30 || name.Length < 5)
                throw new Exception("Name must be between 5 and 30 characters");

            Room newRoom = new Room(GenerateId(), name, type);
            return newRoom;
        }
    }
}
