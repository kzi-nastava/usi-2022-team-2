﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using HealthCare_System.Model;
using System.IO;

namespace HealthCare_System.controllers
{
    class TransferController
    {
        List<Transfer> transfers = new List<Transfer>();
        string path;

        public TransferController()
        {
            path = "../../../data/entities/Transfers.json";
            Load();
        }

        public TransferController(string path)
        {
            this.path = path;
            Load();
        }

        public List<Transfer> Transfers { get => transfers; set => transfers = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            transfers = JsonSerializer.Deserialize<List<Transfer>>(File.ReadAllText(path));
        }

        public Transfer FindById(int id)
        {
            foreach (Transfer transfer in transfers)
                if (transfer.Id == id)
                    return transfer;
            return null;
        }

        public void Serialize(string linkPath = "../../../data/links/TransferLinker.csv")
        {
            string transfersJson = JsonSerializer.Serialize(transfers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, transfersJson);
            string csv = "";
            foreach (Transfer transfer in transfers)
            {
                csv += transfer.Id.ToString() + ";" + transfer.FromRoom.Id + ";" 
                    + transfer.ToRoom.Id + ";" + transfer.Equipment.Id + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }

        public int GenerateId()
        {
            if (transfers.Count == 0)
                return 1001;
            return transfers[transfers.Count - 1].Id + 1;
        }
        
        public void Add(DateTime momentOfTransfer, Room fromRoom, Room toRoom,
            Equipment equipment, int amount)
        {
            int id = GenerateId();
            Transfer transfer = new Transfer(id, momentOfTransfer, fromRoom, toRoom, equipment, amount);
            if (!CheckWithOthers(transfer))
                throw new Exception("Entered amount to be moved is larger than amount availabel " +
                    "in the room after all the transfers are finished.");
            transfers.Add(transfer);
            Serialize();
        }

        public bool CheckWithOthers(Transfer newTransfer)
        {
            bool valid = true;
            int amountOfEquipmentStashed = 0;
            if (transfers.Count > 0)
            {
                foreach (Transfer stashedTransfer in transfers)
                {
                    if (newTransfer.FromRoom == stashedTransfer.FromRoom &&
                        newTransfer.Equipment == stashedTransfer.Equipment)
                        amountOfEquipmentStashed += stashedTransfer.Amount;
                }

                int amountInFroomRoom = newTransfer.FromRoom.EquipmentAmount[newTransfer.Equipment];
                int amountLeft = amountInFroomRoom - amountOfEquipmentStashed;
                if (amountLeft < newTransfer.Amount)
                    valid = false;
            }      
            return valid;
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
}