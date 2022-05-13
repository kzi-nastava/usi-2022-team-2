using System.Collections.Generic;
using HealthCare_System.entities;
using System.Text.Json;
using System.IO;
using System;

namespace HealthCare_System.controllers
{
    class SplittingRenovationController
    {
        List<SplittingRenovation> splittingRenovations;
        string path;

        public SplittingRenovationController()
        {
            path = "../../../data/entities/SplittingRenovations.json";
            Load();
        }

        public SplittingRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<SplittingRenovation> SplittingRenovations { get => splittingRenovations; 
            set => splittingRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            splittingRenovations = JsonSerializer.Deserialize<List<SplittingRenovation>>(File.ReadAllText(path));
        }

        public SplittingRenovation FindById(int id)
        {
            foreach (SplittingRenovation splittingRenovation in splittingRenovations)
                if (splittingRenovation.Id == id)
                    return splittingRenovation;
            return null;
        }

        public int GenerateId()
        {
            if (splittingRenovations.Count > 0)
            {
                return splittingRenovations[splittingRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/SplittingRenovation_Room.csv")
        {
            string splittingRenovationsJson = JsonSerializer.Serialize(splittingRenovations, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, splittingRenovationsJson);
            string csv = "";
            foreach (SplittingRenovation splittingRenovation in splittingRenovations)
            {
                csv += splittingRenovation.Id + ";" + splittingRenovation.Room.Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
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

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string firstNewRoomName, TypeOfRoom firstNewRoomType, string secondNewRoomName,
            TypeOfRoom secondNewRoomType)
        {
            SplittingRenovation splittingRenovation = new SplittingRenovation(GenerateId(), start, end,
                RenovationStatus.BOOKED, room, firstNewRoomName, firstNewRoomType,
                secondNewRoomName, secondNewRoomType);
            splittingRenovations.Add(splittingRenovation);
            Serialize();
        }
    }
}
