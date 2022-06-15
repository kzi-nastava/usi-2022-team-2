using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public interface IManagerService
    {

        IManagerRepo ManagerRepo { get; }
        List<Manager> Managers();
        public Manager FindByMail(string mail);

        public void Serialize();
    }
}