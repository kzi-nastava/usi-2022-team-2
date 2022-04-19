using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    abstract class Person
    {
        string jmbg;
        string firstName;
        string lastName;
        DateTime birthDate;
        string mail;
        string password;
        public Person()
        {

        }
        public Person(string jmbg,string name, string lastName, DateTime birthDate, string mail, string password)
        {
            this.jmbg = jmbg;
            this.firstName = name;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.mail = mail;
            this.password = password;
        }
        public Person(Person person)
        {
            this.jmbg = person.jmbg;
            this.firstName = person.firstName;
            this.lastName = person.lastName;
            this.birthDate = person.birthDate;
            this.mail = person.mail;
            this.password = person.password;
        }

        [JsonPropertyName("jmbg")]
        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        [JsonPropertyName("firstName")]
        public string FirstName {
            get { return firstName; } 
            set { firstName = value; } 
        }

        [JsonPropertyName("lastName")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        [JsonPropertyName("mail")]
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        [JsonPropertyName("password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public override string ToString()
        {
            return "Person[" + "jmbg: " + this.jmbg + ", firstName: " + this.FirstName + ", lastName: " + this.LastName +
                ", birthDate: " + this.BirthDate.ToString("dd/MM/yyyy") + ", mail: " + this.Mail + ", password: " + this.Password + "]";
        }
    }
}
