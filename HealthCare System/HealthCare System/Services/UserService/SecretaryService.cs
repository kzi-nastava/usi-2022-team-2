using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserService
{
    class SecretaryService
    {
        SecretaryRepo secretaryRepo;

        public SecretaryService()
        {
            SecretaryRepoFactory secretaryRepoFactory = new();
            secretaryRepo = secretaryRepoFactory.CreateSecretaryRepository();
        }

        public SecretaryRepo SecretaryRepo { get => secretaryRepo;}
    }
}
