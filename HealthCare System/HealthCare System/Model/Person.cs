using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.Model
{
    public abstract class Person
    {
        string jmbg;
        string firstName;
        string lastName;
        DateTime birthDate;
        string mail;
        string password;

        public Person() { }

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
            jmbg = person.jmbg;
            firstName = person.firstName;
            lastName = person.lastName;
            birthDate = person.birthDate;
            mail = person.mail;
            password = person.password;
        }

        [JsonPropertyName("jmbg")]
        public string Jmbg { get => jmbg; set => jmbg = value; }

        [JsonPropertyName("firstName")]
        public string FirstName { get => firstName; set => firstName = value; }

        [JsonPropertyName("lastName")]
        public string LastName { get => lastName; set => lastName = value; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }

        [JsonPropertyName("mail")]
        public string Mail { get => mail; set => mail = value; }

        [JsonPropertyName("password")]
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return "Person[" + "jmbg: " + jmbg + ", firstName: " + FirstName 
                + ", lastName: " + LastName + ", birthDate: " + BirthDate.ToString("dd/MM/yyyy") +
                ", mail: " + Mail + ", password: " + Password + "]";
        }
    }
}
