using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.EquipmentTransfers.Model;

namespace HealthCare_System.Core.EquipmentTransfers.Repository
{
    public class EquipmentTransferRepo : IEquipmentTransferRepo
    {
        List<Transfer> transfers = new List<Transfer>();
        string path;

        public EquipmentTransferRepo()
        {
            path = "../../../data/entities/Transfers.json";
            Load();
        }

        public EquipmentTransferRepo(string path)
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

        public void Add(Transfer transfer)
        {
            transfers.Add(transfer);
            Serialize();
        }

        public void Delete(Transfer transfer)
        {
            transfers.Remove(transfer);
            Serialize();
        }


    }
}
