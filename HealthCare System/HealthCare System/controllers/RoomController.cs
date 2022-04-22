using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.LoadRooms();
        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        void LoadRooms()
        { 
            this.rooms = JsonSerializer.Deserialize<List<Room>>(File.ReadAllText("data/entities/Rooms.json"));
        }

        public Room FindById(int id)
        {
            foreach (Room room in this.rooms)
                if (room.Id == id)
                    return room;
            return null;
        }


    }
}
