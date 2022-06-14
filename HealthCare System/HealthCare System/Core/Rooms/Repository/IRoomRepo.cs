using HealthCare_System.Core.Rooms.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Rooms.Repository
{
    public interface IRoomRepo
    {
        List<Room> Rooms { get ; set ; }
        void Add(Room room);
        void Delete(Room room);
        Room FindById(int id);
        int GenerateId();
        List<Room> GetRoomsByType(AppointmentType type);
        Room GetStorage();
        void Load();
        void Serialize(string linkPath = "../../../data/links/Room_equipment.csv");
    }
}