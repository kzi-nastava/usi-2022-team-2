﻿using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.EquipmentTransfers.Model
{
    public class TransferDto
    {
        int id;
        DateTime momentOfTransfer;
        int amount;
        Room fromRoom;
        Room toRoom;
        Equipment equipment;

        public TransferDto(int id, DateTime momentOfTransfer, int amount, Room fromRoom, Room toRoom, Equipment equipment)
        {
            this.id = id;
            this.momentOfTransfer = momentOfTransfer;
            this.amount = amount;
            this.fromRoom = fromRoom;
            this.toRoom = toRoom;
            this.equipment = equipment;
        }

        public int Id { get => id; set => id = value; }
        public DateTime MomentOfTransfer { get => momentOfTransfer; set => momentOfTransfer = value; }
        public int Amount { get => amount; set => amount = value; }
        public Room FromRoom { get => fromRoom; set => fromRoom = value; }
        public Room ToRoom { get => toRoom; set => toRoom = value; }
        public Equipment Equipment { get => equipment; set => equipment = value; }
    }
}