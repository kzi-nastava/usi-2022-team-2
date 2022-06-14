using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users.Repository
{
    public interface ISecretaryRepo
    {
        List<Secretary> Secretaries { get ; set ; }

        void Load();
        void Serialize();
    }
}