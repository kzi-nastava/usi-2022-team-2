using HealthCare_System.Core.EquipmentTransfers.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.EquipmentTransfers.Repository
{
    public interface IEquipmentTransferRepo
    {
        string Path { get; set; }

        List<Transfer> Transfers { get; set; }

        void Add(Transfer transfer);

        void Delete(Transfer transfer);

        Transfer FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/TransferLinker.csv");
    }
}