using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Users
{
    class ManagerController
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        public List<Manager> Managers()
        {
            return managerService.Managers();
        }

        public Manager FindByMail(string mail)
        {
            return managerService.FindByMail(mail);
        }

        public void Serialize()
        {
            managerService.Serialize();
        }
    }
}
