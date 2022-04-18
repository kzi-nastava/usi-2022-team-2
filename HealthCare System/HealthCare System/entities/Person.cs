using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    abstract class Person
    {
        string name;
        string lastName;
        DateTime birthDate;
        string mail;
        string password;
        public string Name {
            get { return name; } 
            set { name = value; } 
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
