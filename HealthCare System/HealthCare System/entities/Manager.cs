using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Manager : Person
    {
        public Manager()
        {

        }

        public Manager(string firstName, string lastName, DateTime birthDate, string mail, string password) : base(firstName, lastName, birthDate, mail, password)
        {
        }

        public Manager(Manager manager) : base(manager.FirstName, manager.LastName, manager.BirthDate, manager.Mail, manager.Password)
        {

        }
    }
}
