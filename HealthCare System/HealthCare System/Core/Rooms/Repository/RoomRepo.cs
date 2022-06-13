using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.Rooms.Repository
{
    public class RoomRepo
    {
        List<Room> rooms;
        string path;

        public RoomRepo()
        {
            path = "../../../data/entities/Rooms.json";
            Load();
        }

        public RoomRepo(string path)
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

        public int GenerateId()
        {
            return rooms[rooms.Count - 1].Id + 1;
        }

        //TODO: CHANGE TO FIND NOT GET
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

        public void Add(Room room)
        {
            rooms.Add(room);
            Serialize();
        }

        public void Delete(Room room)
        {
            rooms.Remove(room);
            Serialize();
        }


        

        
    }
}
