using System;

namespace HealthCare_System.entities
{
    public class Manager : Person
    {
        public Manager()
        {

        }

        public Manager(string jmbg, string firstName, string lastName, DateTime birthDate, string mail, string password) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
        }

        public Manager(Manager manager) : base(manager.Jmbg, manager.FirstName, manager.LastName, manager.BirthDate, manager.Mail, manager.Password)
        {

        }
    }
}
