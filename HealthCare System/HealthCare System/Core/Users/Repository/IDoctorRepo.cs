using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users.Repository
{
    public interface IDoctorRepo
    {
        public List<Doctor> Doctors { get; set; }

        void Load();

        public Doctor FindByMail(string mail);

        public Doctor FindByJmbg(string jmbg);

        public List<Doctor> FindBySpecialization(Specialization specialization);

        public List<Doctor> FindByFirstName(string firstName);

        public List<Doctor> FindByLastName(string lastName);

        public void Serialize();
        
    }
}
