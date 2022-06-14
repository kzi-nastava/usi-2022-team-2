using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class SecretaryController
    {
        ISecretaryService secretaryService;

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
