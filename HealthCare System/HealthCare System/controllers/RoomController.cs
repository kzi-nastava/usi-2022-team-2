using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

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

    }
}
