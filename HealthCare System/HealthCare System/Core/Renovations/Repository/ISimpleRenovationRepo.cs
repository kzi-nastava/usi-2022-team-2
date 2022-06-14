using HealthCare_System.Core.Renovations.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Renovations.Repository
{
    public interface ISimpleRenovationRepo
    {
        List<SimpleRenovation> SimpleRenovations { get; set; }

        void Add(SimpleRenovation simpleRenovation);

        void Delete(SimpleRenovation simpleRenovation);

        SimpleRenovation FindById(int id);

        int GenerateId();

        void Load();

        void Serialize(string linkPath = "../../../data/links/SimpleRenovation_Room.csv");
    }
}