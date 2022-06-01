using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.UserRepo
{
    class DoctorRepoFactory
    {
        private DoctorRepo doctorRepo;

        public DoctorRepo CreateDoctorRepository()
        {
            if (doctorRepo == null)
                doctorRepo = new DoctorRepo();

            return doctorRepo;
        }
    }
}
