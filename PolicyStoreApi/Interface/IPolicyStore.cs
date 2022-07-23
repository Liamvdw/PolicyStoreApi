using MongoDB.Bson;
using PolicyStoreApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PolicyStoreApi.Interface
{
    public interface IPolicyStore
    {
        IEnumerable<Policy> GetPolicies();
        Policy GetSinglePolicy(string policyNo);
        ResponseMessage AddPolicy(Policy policy);
        ResponseMessage UpdatePolicy(string policyNo, Policy policy);
        ResponseMessage DeletePolicy(string policyNo);
    }
}
