using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class UserController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public void RunAntiTrollCheck(Patient patient)
        {
            userService.RunAntiTrollCheck(patient);
        }

        public Person Login(string mail, string password)
        {
            return userService.Login(mail, password);
        }
    }
}
