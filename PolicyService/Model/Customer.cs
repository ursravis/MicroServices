using Amazon.DynamoDBv2.DataModel;
using DomainModels;
using System;
using System.Collections.Generic;
namespace PolicyService
{
     public class Customer       
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
    }
}