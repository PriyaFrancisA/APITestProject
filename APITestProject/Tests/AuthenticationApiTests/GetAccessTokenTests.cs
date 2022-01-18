using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using FluentAssertions;

namespace APITestProject.Tests.AuthenticationApiTests
{
    [TestFixture]
    public class GetAccessTokenTests
    {
        Context _context;

        [TestCase(HttpStatusCode.Created, TestName = "Get access token gives success")]
        public void GivenIPostAgendaDays_ThenIHaveSuccess(HttpStatusCode expectedHttpStatusCode)
        {
            _context = new Context();

            var jsonbody = "";
            string signature = _context.GetSignedSignature(jsonbody.ToString());

            var client = new RestClient(Constants.BASE_URL);
            var request = new RestRequest(Constants.AUTHENTICATION_API_GET_ACCESS_TOKEN, Method.POST);
            request.AddHeader("Authorization", string.Format("hmac {0}", signature));

            IRestResponse response = client.Execute<dynamic>(request);
            var responseDetails = JsonConvert.DeserializeObject<dynamic>(response.Content);
            string accessToken = responseDetails.accessToken;

            // assert
            response.StatusCode.Should().Be(expectedHttpStatusCode);
        }
    }
}
