using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Users
{
    class SecretaryController
    {
        private readonly ISecretaryService secretaryService;

        public SecretaryController(ISecretaryService secretaryService)
        {
            this.secretaryService = secretaryService;
        }

        public List<Secretary> Secretaries()
        {
            return secretaryService.Secretaries();
        }

        public void Serialize()
        {
            secretaryService.Serialize();
        }
    }
}
