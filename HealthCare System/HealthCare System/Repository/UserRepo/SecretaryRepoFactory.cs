using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.UserRepo
{
    class SecretaryRepoFactory
    {
        private SecretaryRepo secretaryRepo;

        public SecretaryRepo CreateSecretaryRepository()
        {
            if (secretaryRepo == null)
                secretaryRepo = new SecretaryRepo();

            return secretaryRepo;
        }
    }
}
