using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class ManagerService : IManagerService
    {
        IManagerRepo managerRepo;

        public ManagerService(IManagerRepo managerRepo)
        {
            this.managerRepo = managerRepo;
        }

        public IManagerRepo ManagerRepo { get => managerRepo; }

        public List<Manager> Managers()
        {
            return managerRepo.Managers;
        }

        public Manager FindByMail(string mail)
        {
            return managerRepo.FindByMail(mail);
        }

        public void Serialize()
        {
            managerRepo.Serialize();
        }
    }
}
