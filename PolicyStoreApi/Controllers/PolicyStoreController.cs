using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PolicyStoreApi.Interface;
using PolicyStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolicyStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyStoreController : ControllerBase, IPolicyStore
    {
        private readonly ILogger<PolicyStoreController> _logger;
        private IMongoCollection<Policy> _policyCollection;

        public PolicyStoreController(ILogger<PolicyStoreController> logger, IMongoClient client)
        {
            var database = client.GetDatabase("PolicyStore");
            _policyCollection = database.GetCollection<Policy>("Policy");
            _logger = logger;
        }

        [HttpPost]
        [Route("AddPolicy")]
        public ResponseMessage AddPolicy([FromBody] Policy policy)
        {
            var response = new ResponseMessage();
            try
            {
                //Adds a single document to the Policy collection.
                _policyCollection.InsertOne(policy);

                response.IsSuccessful = true;
                response.Message = "Successful";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        [HttpDelete]
        [Route("DeletePolicy/{policyNo}")]
        public ResponseMessage DeletePolicy(string policyNo)
        {
            var response = new ResponseMessage();
            try
            {
                //Gets the policy number you're looking for.
                var result = _policyCollection.Find(x => x.PolicyNo == policyNo).FirstOrDefault();

                //If policy number is found, then delete it.
                //Else throw an error.
                if (result != null)
                {
                    _policyCollection.DeleteOne(x => x.Id == result.Id);
                    response.IsSuccessful = true;
                    response.Message = $"Delete has been Successful.";
                }
                else
                    throw new Exception($"Oops, we can't find your policy.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        [HttpPut]
        [Route("UpdatePolicy/{policyNo}")]
        public ResponseMessage UpdatePolicy(string policyNo, [FromBody] Policy policy)
        {
            var response = new ResponseMessage();
            try
            {
                //If policy number is found, then update the object.
                //else throw an error
                var policyResult = GetSinglePolicy(policyNo);
                policy.Id = policyResult.Id;
                var result = _policyCollection.ReplaceOne(item => item.Id == policyResult.Id, policy);

                response.IsSuccessful = true;
                response.Message = $"Update was Successful.";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        [HttpGet]
        [Route("GetPolicies")]
        public IEnumerable<Policy> GetPolicies()
        {
            var result = new List<Policy>();
            try
            {
                //Gets a list of all policies
                var filter = Builders<Policy>.Filter.Empty;
                result = _policyCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }

        [HttpGet]
        [Route("GetSinglePolicy")]
        public Policy GetSinglePolicy(string policyNo)
        {
            var result = new Policy();
            try
            {
                //Gets a single policy.
                result = _policyCollection.Find(x => x.PolicyNo == policyNo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return result;
        }
    }
}
