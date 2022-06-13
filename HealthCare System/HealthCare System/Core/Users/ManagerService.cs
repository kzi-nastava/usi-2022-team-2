using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class ManagerService
    {
        ManagerRepo managerRepo;

        public ManagerService(ManagerRepo managerRepo)
        {
            this.managerRepo = managerRepo;
        }

        public ManagerRepo ManagerRepo { get => managerRepo;}

        public List<Manager> Managers()
        {
            return managerRepo.Managers;
        }
    }
}
