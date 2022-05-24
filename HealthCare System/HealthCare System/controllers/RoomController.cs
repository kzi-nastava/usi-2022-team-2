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
            path = "../../../data/entities/Rooms.json";
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

        public void Serialize(string linkPath = "../../../data/links/Room_equipment.csv")
        {
            string roomsJson = JsonSerializer.Serialize(rooms, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, roomsJson);
            string csv = "";
            foreach (Room room in rooms)
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in room.EquipmentAmount)
                {
                    csv += room.Id.ToString() + ";" + equipmentAmountEntry.Key.Id.ToString() + ";" + 
                        equipmentAmountEntry.Value.ToString() + "\n";
                }
                
            }
            File.WriteAllText(linkPath, csv);
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
            return filteredRooms;
        }

        public Room GetStorage()
        {
            Room retRoom = null;
            foreach (Room room in rooms)
            {
                if (room.Type == TypeOfRoom.STORAGE)
                {
                    retRoom = room;
                }
            }
            return retRoom;
        }

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

        public void MoveToRoom(Room room, Equipment equipmnet, int amount)
        {
            room.EquipmentAmount[equipmnet] += amount;
        }

        public void MoveFromRoom(Room room, Equipment equipmnet, int amount)
        {
            if (room.EquipmentAmount[equipmnet] < amount)
                throw new Exception("Amount to be moved is larger then current amount in a room");
            room.EquipmentAmount[equipmnet] -= amount;
        }

        public void MoveEquipmentToStorage(Room room)
        {
            foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in room.EquipmentAmount)
            {
                MoveFromRoom(room, equipmentAmountEntry.Key, equipmentAmountEntry.Value);
                MoveToRoom(FindById(1003), equipmentAmountEntry.Key, equipmentAmountEntry.Value);
            }
        }

        
    }
}
