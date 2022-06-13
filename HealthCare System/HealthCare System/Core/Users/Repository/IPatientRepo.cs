using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Users.Repository
{
    public interface IPatientRepo
    {
        void Load();

        public Patient FindByMail(string mail);

        public Patient FindByJmbg(string jmbg);

        public void Add(Patient patient);

        public void Delete(Patient patient);

        public void Serialize();
    }
}
