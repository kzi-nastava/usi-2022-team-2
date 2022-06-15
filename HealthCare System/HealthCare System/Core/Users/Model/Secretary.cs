using System;

namespace HealthCare_System.Core.Users.Model
{
    public class Secretary : Person
    {

        public Secretary() { }

        public Secretary(string jmbg, string firstName, string lastName, DateTime birthDate, 
            string mail, string password) : base(jmbg, firstName, lastName, birthDate, mail, password) { }

        public Secretary(Secretary secretary) : base(secretary.Jmbg, secretary.FirstName,
            secretary.LastName, secretary.BirthDate, secretary.Mail, secretary.Password) { }
    }
}
