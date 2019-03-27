using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRepository;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace MetadataService.Controllers
{
    [Route(Constants.APIPath+"/[Controller]")]
    public class QuotesController:ControllerBase
    {
        private readonly IQuoteRepository _repository;
        private readonly ILogger<QuotesController> _logger;
        public QuotesController(IQuoteRepository repository,ILogger<QuotesController> logger)
        {   
             this._repository = repository;
            this._logger = logger;
        }
          [HttpGet("{customerId}")]
        public async Task<IActionResult> GetQuotes(int customerId)
        {
            var quotes = await _repository.GetQuotesAsync(customerId);
            return Ok(quotes);
        }
       
        [HttpPost("{customerId}")]
        public async Task<IActionResult> SaveQuotes(int customerId,[FromBody] IEnumerable<QuoteDto> quotes)
        {
            if (quotes != null)
            {
                var savedquotes = await _repository.SaveQuotesAsync(customerId, quotes.ToList());
                return Ok(savedquotes);
            }
            return BadRequest();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteQuotes(int customerId)
        {
            var result = await _repository.DeleteQuotesAsync(customerId);
            return Ok(result);
        }
    }
}