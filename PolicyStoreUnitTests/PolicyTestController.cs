using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using PolicyStoreApi.Controllers;
using PolicyStoreApi.Model;

namespace PolicyStoreUnitTests
{
    [TestClass]
    public class PolicyTestController
    {
        private readonly ILogger<PolicyStoreController> _logger;
        private IMongoClient _client = new MongoClient();
        
        public PolicyTestController()
        {
            _client = new MongoClient("mongodb+srv://liamvdw:Onepiece90@policystorecluster.xypf63l.mongodb.net/?retryWrites=true&w=majority");
        }

        [TestMethod]
        public void UpdatePolicyMethodTest()
        {
            //arrange
            string policyNo = "333333333";
            var controller = new PolicyStoreController(_logger, _client);

            var policy = controller.GetSinglePolicy(policyNo);

            policy.FirstName = "Liam";
            policy.DateOfBirth = "10/07/1990";
            policy.LastName = "Van Der Westhuizen";
            policy.Gender = "Male";
            policy.PolicyNo = "333333333";

            //act
            var result = controller.UpdatePolicy(policyNo, policy);

            var expectedResult = controller.GetSinglePolicy(policyNo);

            //assert
            Assert.AreNotSame(expectedResult, policy);
        }

        [TestMethod]
        public void GetAllPoliciesMethodTest()
        {
            //arrange
            var controller = new PolicyStoreController(_logger, _client);

            //act            
            var result = controller.GetPolicies();

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddPolicyMethodTest()
        {
            //arrange
            var controller = new PolicyStoreController(_logger, _client);
            //var policy = new Policy
            //{
            //    FirstName = "Tamara",
            //    DateOfBirth = "03/01/1992",
            //    LastName = "Van Der Westhuizen",
            //    Gender = "Female",
            //    PolicyNo = "111111111"                
            //};

            var policy = new Policy
            {
                FirstName = "Nico",
                DateOfBirth = "15/09/1980",
                LastName = "Robin",
                Gender = "Female",
                PolicyNo = "222222222"
            };

            //act            
            var result = controller.AddPolicy(policy);

            //assert
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void DeletePolicyMethodTest()
        {
            //arrange
            string policyNo = "222222222";
            var controller = new PolicyStoreController(_logger, _client);

            //act            
            var policy = controller.GetSinglePolicy(policyNo);
            var result = controller.DeletePolicy(policy.PolicyNo);

            //assert
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
