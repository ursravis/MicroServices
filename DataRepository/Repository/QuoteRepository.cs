using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;

namespace DataRepository
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly InsuranceContext insuranceContext;
        private readonly IMapper _mapper;
        public QuoteRepository(InsuranceContext insuranceContext, IMapper mapper)
        {
            this.insuranceContext = insuranceContext;
            this._mapper = mapper;
        }

        public async Task<int> DeleteQuotesAsync(int customerId)
        {
              var quotes = await this.insuranceContext.Quotes
                                    .Where(q => q.CustomerId == customerId).ToListAsync();
                                    
            this.insuranceContext.Quotes.RemoveRange(quotes);
            return  await this.insuranceContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuoteDto>> GetQuotesAsync(int customerId)
        {
            var quotes = await this.insuranceContext.Quotes
                                    .Include(q => q.Customer)
                                    .Include(q => q.VehicleQuotes).ThenInclude(vq => vq.Vehicle)
                                    .Where(q => q.CustomerId == customerId).ToListAsync();
            var quoteDtos = new List<QuoteDto>();
            quotes.ForEach(q => quoteDtos.Add(_mapper.Map<Quote, QuoteDto>(q)));

            return quoteDtos;
        }

        public async Task<IEnumerable<QuoteDto>> SaveQuotesAsync(int customerId, IEnumerable<QuoteDto> quotes)
        {
            foreach (QuoteDto quote in quotes)
            {
                var newQuote = new Quote();
                newQuote.CustomerId = customerId;
                newQuote.EndDate = quote.EndDate;
                newQuote.MaxCoverage = quote.MaxCoverage;
                newQuote.PricePerMonth = quote.PricePerMonth;
                newQuote.StartDate = quote.StartDate;
                newQuote.VehicleQuotes = new List<VehicleQuote>();
                foreach (VehicleDto vehicle in quote.Vehicles)
                {
                    var vehicleQuote = new VehicleQuote();
                    vehicleQuote.VehicleId = vehicle.VehicleId;
                    newQuote.VehicleQuotes.Add(vehicleQuote);
                }
                this.insuranceContext.Quotes.Add(newQuote);
            }
            await this.insuranceContext.SaveChangesAsync();
            return await this.GetQuotesAsync(customerId);
        }

    }
}