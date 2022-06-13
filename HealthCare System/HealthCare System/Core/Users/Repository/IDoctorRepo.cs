using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Users.Repository
{
    public interface IDoctorRepo
    {
        void Load();

        public Doctor FindByMail(string mail);

        public Doctor FindByJmbg(string jmbg);

        public List<Doctor> FindBySpecialization(Specialization specialization);

        public List<Doctor> FindByFirstName(string firstName);

        public List<Doctor> FindByLastName(string lastName);

        public void Serialize();
        
    }
}
