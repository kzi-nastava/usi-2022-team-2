using System.Collections.Generic;
using HealthCare_System.Model;
using System.Text.Json;
using System.IO;
using System;

namespace HealthCare_System.controllers
{
    class SimpleRenovationController
    {
        List<SimpleRenovation> simpleRenovations;
        string path;

        public SimpleRenovationController()
        {
            path = "../../../data/entities/SimpleRenovations.json";
            Load();
        }

        public SimpleRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<SimpleRenovation> SimpleRenovations { get => simpleRenovations; set => simpleRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load() 
        {
            simpleRenovations = JsonSerializer.Deserialize<List<SimpleRenovation>>(File.ReadAllText(path));
        }

        public SimpleRenovation FindById(int id)
        {
            foreach (SimpleRenovation simpleRenovation in simpleRenovations)
                if (simpleRenovation.Id == id)
                    return simpleRenovation;
            return null;
        }

        public int GenerateId()
        {
            if (simpleRenovations.Count > 0)
            {
                return simpleRenovations[simpleRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv")
        {
            string simpleRenovationsJson = JsonSerializer.Serialize(simpleRenovations, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, simpleRenovationsJson);
            string csv = "";
            foreach (SimpleRenovation simpleRenovation in simpleRenovations)
            {
                csv += simpleRenovation.Id + ";" + simpleRenovation.Room.Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
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

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string newRoomName, TypeOfRoom newRoomType)
        {
            SimpleRenovation simpleRenovation = new SimpleRenovation(GenerateId(), start, end, 
                RenovationStatus.BOOKED, room, newRoomName, newRoomType);
            simpleRenovations.Add(simpleRenovation);
            Serialize();
        }


    }
}
