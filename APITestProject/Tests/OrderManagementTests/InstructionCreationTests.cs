using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using FluentAssertions;
using System;
using APITestProject.DataModel;
using System.IO;

namespace APITestProject.Tests.OrderManagementTests
{
    [TestFixture]
    public class InstructionCreationTests
    {
        Context _context;

        [TestCase(HttpStatusCode.Created, TestName = "Create instruction gives success")]
        public void GivenIPostInstruction_ThenIHaveCreated(HttpStatusCode expectedHttpStatusCode)
        {
            _context = new Context();
            var jsonbody = JsonConvert.SerializeObject(GetInstructionRequestBody());
            var authorizationToken = _context.GetAccessToken(jsonbody.ToString());

            var client = new RestClient(Constants.BASE_URL);
            var request = new RestRequest(Constants.INSTRUCTION_CREATE, Method.POST)
                            .AddUrlSegment("orderRef", GetRandomString());
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonbody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-account-id", Constants.ACCOUNT_ID);
            request.AddHeader("x-tenant-id", Constants.TENANT_ID);
            request.AddHeader("Authorization", string.Format("Bearer {0}", authorizationToken));
            var response = client.Execute(request);

            //assert
            response.StatusCode.Should().Be(expectedHttpStatusCode);
        }
        //Test 2 - create instruction without authorization
        //Test 3 - create instruction without orderRef
        //Test 4 - create instruction without accountid,wrong accountid
        //Test 5 - create instruction without tenantid,wrong tenantid
        //Test 6 - create instruction with body containing all the optional values

        public CreateInstructionDto GetInstructionRequestBody()
        {
            CreateInstructionDto objCreateInstruction = new CreateInstructionDto()
            {
                instructionRef = Guid.NewGuid().ToString(),
                customerRef = Guid.NewGuid().ToString(),
                direction = "DEBIT",
                financialInstrumentId = "",
                amount = " 6.99",
                currency = "Euro",
                country = "Ireland",
                settledByDate = "2025-12-12",
                schemeId = "654EB81FF7F07F7CF5A1EE3FF6972E90",
                metadata = new Metadata { key1 = GetRandomString(), key2 = GetRandomString(), key3 = GetRandomString() },
            };

            return objCreateInstruction;
        }

        public string GetRandomString()
        {
            var randomString = Path.GetRandomFileName().Substring(0, 6);
            return randomString;
        }
    }
}
