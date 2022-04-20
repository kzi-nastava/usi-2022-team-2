using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    public enum DaysOffRequestState { WAITING, ACCEPTED, DENIED}
    class DaysOffRequest
    {
        DateTime start;
        DateTime end;
        String description;
        DaysOffRequestState state;
        bool urgent;
        Doctor doctor;


    }
}
