using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserService
{
    class ManagerService
    {
        ManagerRepo managerRepo;

        public ManagerService()
        {
            ManagerRepoFactory managerRepoFactory = new();
            managerRepo = managerRepoFactory.CreateManagerRepository();
        }

        public ManagerRepo ManagerRepo { get => managerRepo;}

        public List<Manager> Managers()
        {
            return managerRepo.Managers;
        }
    }
}
