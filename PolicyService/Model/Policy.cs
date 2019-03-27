using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
namespace PolicyService
{
    [DynamoDBTable("PolicyTable")]
    public class Policy
    {
        public Policy()
        {
        }

        [DynamoDBHashKey]
        public string PolicyId { get; set; } = Guid.NewGuid().ToString();

        public Customer CustomerInfo { get; set; }

        public DateTime PolicyEffectiveDate { get; set; }
        public DateTime PolicyEndDate { get; set; }

        public double PremiumPerMonth { get; set; }

        public double Coverage { get; set; }

        public int QuoteIdRef { get; set; }

        public string Status { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
    
}