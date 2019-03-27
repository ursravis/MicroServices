using System;
using System.Collections.Generic;

namespace DataRepository
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<VehicleQuote> VehicleQuotes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerMonth { get; set; }
        public double MaxCoverage { get; set; }
    }
}