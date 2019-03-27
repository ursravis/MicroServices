using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PolicyService
{
    [Route(Constants.APIPath+"/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly ILogger<PoliciesController> _logger;
        private readonly IAmazonDynamoDB _dynamoDb;

        public PoliciesController(ILogger<PoliciesController> logger, IAmazonDynamoDB dynamoDb)
        {
            this._logger = logger;
            this._dynamoDb = dynamoDb;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var context = new DynamoDBContext(_dynamoDb);
            var policy = await context.LoadAsync<Policy>(id.ToString());
            return policy != null ? Ok(policy) : (ObjectResult)NotFound(id);
        }
        [HttpPost()]
        public async Task<IActionResult> CreatePolicy([FromBody]Policy policy)
        {
            // var policy = new Policy()
            // {
            //     CustomerInfo = quote.Customer,
            //     PolicyEffectiveDate = quote.StartDate,
            //     PolicyEndDate = quote.EndDate,
            //     Coverage = quote.MaxCoverage,
            //     PremiumPerMonth = quote.PricePerMonth,
            //     QuoteIdRef = quote.QuoteId,
            //     Status = "New"
            // };
            // var vehicles=new List<Vehicle>();
            // if(quote.Vehicles != null)
            // foreach(var vehicle in quote.Vehicles)
            // {
            //     vehicles.Add(new Vehicle(){

            //     })
            // }
            // policy.Vehicles=vehicles;
            policy.PolicyId=Guid.NewGuid().ToString();
            policy.Status="New";
            var context = new DynamoDBContext(_dynamoDb);
            await context.SaveAsync(policy);
            return Ok(policy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var context = new DynamoDBContext(_dynamoDb);
            await context.DeleteAsync<Policy>(id.ToString());
            return Ok();
        }

    }
}