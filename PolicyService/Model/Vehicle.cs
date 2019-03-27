using Amazon.DynamoDBv2.DataModel;
using DomainModels;
using System;
using System.Collections.Generic;
namespace PolicyService
{
    public class Vehicle
    {
        public Vehicle() {}
         public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}