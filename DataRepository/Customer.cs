using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataRepository
{
    public class Customer
    {
         public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string EmailId { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
