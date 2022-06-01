using System.Collections.Generic;
using HealthCare_System.Model;
using System.Text.Json;
using System.IO;
using System;

namespace HealthCare_System.controllers
{
    class MergingRenovationController
    {
        List<MergingRenovation> mergingRenovations;
        string path;

        public MergingRenovationController()
        {
            path = "../../../data/entities/MergingRenovations.json";
            Load();
        }

        public MergingRenovationController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<MergingRenovation> MergingRenovations { get => mergingRenovations; set => mergingRenovations = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            mergingRenovations = JsonSerializer.Deserialize<List<MergingRenovation>>(File.ReadAllText(path));
        }

        public MergingRenovation FindById(int id)
        {
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
                if (mergingRenovation.Id == id)
                    return mergingRenovation;
            return null;
        }

        public int GenerateId()
        {
            if (mergingRenovations.Count > 0)
            {
                return mergingRenovations[mergingRenovations.Count - 1].Id + 1;
            }
            return 1001;
        }

        public void Serialize(string linkPath = "../../../data/links/MergingRenovation_Room.csv")
        {
            string mergingRenovationsJson = JsonSerializer.Serialize(mergingRenovations,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, mergingRenovationsJson);
            string csv = "";
            foreach (MergingRenovation mergingRenovation in mergingRenovations)
            {
                csv += mergingRenovation.Id + ";" + mergingRenovation.Rooms[0].Id + ";" 
                    + mergingRenovation.Rooms[1].Id + ";" + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

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

        public void BookRenovation(DateTime start, DateTime end, Room firstRoom, 
            Room secondRoom, string newRoomName, TypeOfRoom newRoomType)
        {
            List<Room> rooms = new List<Room> {firstRoom, secondRoom};
            MergingRenovation mergingRenovation = new MergingRenovation(GenerateId(), start, end, rooms,
                RenovationStatus.BOOKED, newRoomName, newRoomType);
            mergingRenovations.Add(mergingRenovation);
            Serialize();
        }

    }
}
