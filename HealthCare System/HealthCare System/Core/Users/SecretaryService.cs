using System.Collections.Generic;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Model;

namespace HealthCare_System.Core.Users
{
    public class SecretaryService
    {
        SecretaryRepo secretaryRepo;

        public SecretaryService(SecretaryRepo secretaryRepo)
        {
            this.secretaryRepo = secretaryRepo;
        }

        public SecretaryRepo SecretaryRepo { get => secretaryRepo;}

        public List<Secretary> Secretaries()
        {
            return secretaryRepo.Secretaries;
        }
    }
}
