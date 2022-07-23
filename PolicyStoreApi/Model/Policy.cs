using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApi.Model
{
    public class Policy
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("dob")]
        public string DateOfBirth { get; set; }

        [BsonElement("policyNo")]
        public string PolicyNo { get; set; }
    }
}
