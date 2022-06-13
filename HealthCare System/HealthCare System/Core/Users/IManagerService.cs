using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public interface IManagerService
    {
        List<Manager> Managers();
    }
}