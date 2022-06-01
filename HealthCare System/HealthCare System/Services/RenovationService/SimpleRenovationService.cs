using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.RenovationRepo;

namespace HealthCare_System.Services.RenovationService
{
    class SimpleRenovationService
    {
        SimpleRenovationRepo simpleRenovationRepo;

        public SimpleRenovationService()
        {
            SimpleRenovationRepoFactory simpleRenovationRepoFactory = new();
            simpleRenovationRepo = simpleRenovationRepoFactory.CreateSimpleRenovationRepository();
        }

        public SimpleRenovationRepo SimpleRenovationRepo { get => simpleRenovationRepo;}

        public void BookRenovation(DateTime start, DateTime end, Room room,
            string newRoomName, TypeOfRoom newRoomType)
        {
            SimpleRenovation simpleRenovation = new SimpleRenovation(GenerateId(), start, end,
                RenovationStatus.BOOKED, room, newRoomName, newRoomType);
            simpleRenovations.Add(simpleRenovation);
            Serialize();
        }

        public void StartSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.ACTIVE;
            simpleRenovationController.Serialize();
            roomController.MoveEquipmentToStorage(simpleRenovation.Room);
            roomController.Serialize();
        }

        public void FinishSimpleRenovation(SimpleRenovation simpleRenovation)
        {
            simpleRenovation.Status = RenovationStatus.FINISHED;
            roomController.UpdateRoom(simpleRenovation.Room, simpleRenovation.NewRoomName, simpleRenovation.NewRoomType);
            simpleRenovationController.SimpleRenovations.Remove(simpleRenovation);
            simpleRenovationController.Serialize();
        }

        public void TryToExecuteSimpleRenovations()
        {
            if (simpleRenovationController.SimpleRenovations.Count > 0)
            {
                foreach (SimpleRenovation simpleRenovation in simpleRenovationController.SimpleRenovations)
                {
                    if (DateTime.Now >= simpleRenovation.EndingDate)
                    {
                        FinishSimpleRenovation(simpleRenovation);
                        return;
                    }

                    if (DateTime.Now >= simpleRenovation.BeginningDate &&
                        simpleRenovation.Status == RenovationStatus.BOOKED)
                    {
                        StartSimpleRenovation(simpleRenovation);
                        return;
                    }
                }
            }

        }
    }
}
