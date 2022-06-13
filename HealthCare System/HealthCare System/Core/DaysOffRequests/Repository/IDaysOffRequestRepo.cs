using HealthCare_System.Core.DaysOffRequests.Model;

namespace HealthCare_System.Core.DaysOffRequests.Repository
{
    public interface IDaysOffRequestRepo
    {
        string Path { get; set; }

        DaysOffRequest FindById(int id);
        void Serialize();
    }
}