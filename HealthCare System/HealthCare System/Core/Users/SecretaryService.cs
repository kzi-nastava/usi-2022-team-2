using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class SecretaryService : ISecretaryService
    {
        ISecretaryRepo secretaryRepo;

        public SecretaryService(ISecretaryRepo secretaryRepo)
        {
            this.secretaryRepo = secretaryRepo;
        }

        public ISecretaryRepo SecretaryRepo { get => secretaryRepo; }

        public List<Secretary> Secretaries()
        {
            return secretaryRepo.Secretaries;
        }

        public void Serialize()
        {
            secretaryRepo.Serialize();
        }
    }
}
