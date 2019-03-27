using System.Collections.Generic;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using DataRepository;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MetadataService.Controllers
{
    [Route(Constants.APIPath+"/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMetadataRepository _repository;
        private readonly ILocationService locationService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMetadataRepository repository, ILocationService locationService, ILogger<CustomersController> logger)
        {
            this._repository = repository;
            this.locationService = locationService;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _repository.GetAllCustomersAsync();
            return Ok(customers);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            _logger.LogInformation($"Customer get called with id :{id}");
            var customer = await _repository.GetCustomerAsync(id);
            return Ok(customer);
        }
        /// <summary>
        /// Saves a Customer.
        /// </summary>     
        /// <returns>A newly created customer</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SaveCustomer([FromBody]CustomerDto customerdto)
        {
            _logger.LogInformation($"Customer Save called with name :{customerdto.FirstName}");
            var customerZipIndex = locationService.GetInsuranceIndexByLocation(customerdto.ZipCode);
            var customer = await _repository.SaveCustomerAsync(customerdto);
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            _logger.LogInformation($"Customer delete called with id :{id}");
            var status = await _repository.DeleteCustomerAsync(id);
            return status ? (IActionResult)Ok() : NotFound();
        }
    }

}