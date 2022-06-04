using System.Collections.Generic;
using HealthCare_System.Model;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserServices
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
