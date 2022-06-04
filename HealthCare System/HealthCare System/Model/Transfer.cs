using HealthCare_System.Model.Dto;
using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Model
{
    public class Transfer
    {
        int id;
        DateTime momentOfTransfer;
        int amount;
        Room fromRoom;
        Room toRoom;
        Equipment equipment;

        public Transfer() { }

        public Transfer(int id, DateTime momentOfTransfer, int amount)
        {
            this.id = id;
            this.momentOfTransfer = momentOfTransfer;
            this.amount = amount;
        }

        public Transfer(int id, DateTime momentOfTransfer, Room fromRoom, Room toRoom,
            Equipment equipment, int amount)
        {
            this.id = id;
            this.momentOfTransfer = momentOfTransfer;
            this.fromRoom = fromRoom;
            this.toRoom = toRoom;
            this.equipment = equipment;
            this.amount = amount;
        }

        public Transfer(Transfer transfer)
        {
            id = transfer.id;
            momentOfTransfer = transfer.momentOfTransfer;
            fromRoom = transfer.fromRoom;
            toRoom = transfer.toRoom;
            equipment = transfer.equipment;
            amount = transfer.amount;
        }

        public Transfer(TransferDTO transferDTO)
        {
            id = transferDTO.Id;
            momentOfTransfer = transferDTO.MomentOfTransfer;
            fromRoom = transferDTO.FromRoom;
            toRoom = transferDTO.ToRoom;
            equipment = transferDTO.Equipment;
            amount = transferDTO.Amount;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("momentOfTransfer")]
        public DateTime MomentOfTransfer { get => momentOfTransfer; set => momentOfTransfer = value; }

        [JsonPropertyName("amount")]
        public int Amount { get => amount; set => amount = value; }

        [JsonIgnore]
        public Room FromRoom { get => fromRoom; set => fromRoom = value; }

        [JsonIgnore]
        public Room ToRoom { get => toRoom; set => toRoom = value; }

        [JsonIgnore]
        public Equipment Equipment { get => equipment; set => equipment = value; }

        public override string ToString()
        {
            return "Transfer[" + "id: " + id + ", amount: " + amount + ", equipment: " 
                + equipment.Name + ", fromRoom: " + fromRoom.Id + ", toRoom: " + toRoom.Id + "]";
        }

    }
}
