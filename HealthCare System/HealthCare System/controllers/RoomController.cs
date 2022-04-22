using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;

namespace HealthCare_System.controllers
{
    class RoomController
    {
        List<Room> rooms;

        public RoomController() 
        {
            Load();
        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        void Load()
        { 
            rooms = JsonSerializer.Deserialize<List<Room>>(File.ReadAllText("data/entities/Rooms.json"));
        }

        public Room FindById(int id)
        {
            foreach (Room room in rooms)
                if (room.Id == id)
                    return room;
            return null;
        }


    }
}
