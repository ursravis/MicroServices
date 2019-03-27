using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels;

namespace DataRepository
{
    public interface IMetadataRepository
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerAsync(int id);
        Task<CustomerDto> SaveCustomerAsync(CustomerDto customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<IEnumerable<VehicleDto>> GetVehiclesAsync(int customerId);
        Task<IEnumerable<VehicleDto>> SaveVehiclesAsync(int customerId,List<VehicleDto> vehicles);

    }
}