using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class SecretaryService : ISecretaryService
    {
        SecretaryRepo secretaryRepo;

        public SecretaryService(SecretaryRepo secretaryRepo)
        {
            this.secretaryRepo = secretaryRepo;
        }

        public SecretaryRepo SecretaryRepo { get => secretaryRepo; }

        public List<Secretary> Secretaries()
        {
            return secretaryRepo.Secretaries;
        }
    }
}
