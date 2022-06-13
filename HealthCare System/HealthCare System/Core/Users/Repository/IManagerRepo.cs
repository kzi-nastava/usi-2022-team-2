using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Core.Users.Repository
{
    public interface IManagerRepo
    {
        void Load();

        public Manager FindByMail(string mail);

        public void Serialize();
        
    }
}
