using System.Threading.Tasks;

namespace MetadataService
{
    public interface ILocationService
    {
        Task<double> GetInsuranceIndexByLocation(int zipcode);
    }
}