using System;
using System.Collections.Generic;

namespace DataRepository
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public VehicleType VehicleType { get; set; }
        public int VehicleTypeId { get; set; }
        public ICollection<VehicleQuote> VehicleQuotes { get; set; }
    }
}