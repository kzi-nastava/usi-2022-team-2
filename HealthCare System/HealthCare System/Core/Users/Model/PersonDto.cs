using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Users.Model
{
    public class PersonDto
    {
        string jmbg;
        string firstName;
        string lastName;
        string mail;
        DateTime birthDate;
        string password;

        public PersonDto(string jmbg, string firstName, string lastName, string mail, DateTime birthDate, string password)
        {
            this.Jmbg = jmbg;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mail = mail;
            this.BirthDate = birthDate;
            this.Password = password;
        }

        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Mail { get => mail; set => mail = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Password { get => password; set => password = value; }
    }
}
