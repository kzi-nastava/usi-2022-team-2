using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Model;

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

        public List<Secretary> Secretaries()
        {
            return secretaryRepo.Secretaries;
        }
    }
}
