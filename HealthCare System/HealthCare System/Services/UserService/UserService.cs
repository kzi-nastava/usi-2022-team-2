using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.UserService
{
    class UserService
    {
        public void RunAntiTrollCheck(Patient patient)
        {
            DateTime now = DateTime.Now;
            int createRequests = 0;
            int editRequests = 0;

            foreach (AppointmentRequest request in appointmentRequests)
            {
                if (request.Patient == patient)
                {
                    if ((now - request.RequestCreated).TotalDays < 30)
                    {
                        if (request.Type == RequestType.CREATE)
                            createRequests++;
                        else
                            editRequests++;
                    }
                }
            }

            if (createRequests > 7 || editRequests > 4)
                patient.Blocked = true;
        }
    }
}
