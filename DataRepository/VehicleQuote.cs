namespace DataRepository
{
    public class VehicleQuote
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int QuoteId { get; set; }

        public Quote Quote { get; set; }        
    }
}