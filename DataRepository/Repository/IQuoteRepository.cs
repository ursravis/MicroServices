using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels;

namespace DataRepository
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<QuoteDto>> GetQuotesAsync(int customerId);
        Task<IEnumerable<QuoteDto>> SaveQuotesAsync(int customerId, IEnumerable<QuoteDto> quotes);
        Task<int> DeleteQuotesAsync(int customerId);

    }
}