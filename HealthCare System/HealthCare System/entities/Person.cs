using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    abstract class Person
    {
        string firstName;
        string lastName;
        DateTime birthDate;
        string mail;
        string password;
        public Person()
        {

        }
        public Person(string name, string lastName, DateTime birthDate, string mail, string password)
        {
            this.firstName = name;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.mail = mail;
            this.password = password;
        }
        public Person(Person person)
        {
            this.firstName = person.firstName;
            this.lastName = person.lastName;
            this.birthDate = person.birthDate;
            this.mail = person.mail;
            this.password = person.password;
        }

        public string FirstName {
            get { return firstName; } 
            set { firstName = value; } 
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
