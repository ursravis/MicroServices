using System;
using System.Collections.Generic;
namespace DomainModels
{
    public class QuoteDto
    {
        public int QuoteId { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<VehicleDto> Vehicles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerMonth { get; set; }
        public double MaxCoverage { get; set; }
    }
}