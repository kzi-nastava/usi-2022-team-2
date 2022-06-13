using HealthCare_System.Core.Users.Model;

namespace HealthCare_System.Core.Users
{
    public interface IUserService
    {
        Person Login(string mail, string password);
        void RunAntiTrollCheck(Patient patient);
    }
}