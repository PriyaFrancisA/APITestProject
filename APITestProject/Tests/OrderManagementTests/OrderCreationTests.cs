using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using FluentAssertions;
using System;
using System.Security.Cryptography;
using APITestProject.DataModel;
using System.IO;

namespace APITestProject.Tests.OrderManagementTests
{
    [TestFixture]
    public class OrderCreationTests
    {
        Context _context;

        [TestCase(HttpStatusCode.Created, TestName = "Create order gives success")]
        public void GivenIPostOrder_ThenIHaveCreated(HttpStatusCode expectedHttpStatusCode)
        {
            _context = new Context();
            var jsonbody = JsonConvert.SerializeObject(GetOrderRequestBody());
            //var jsonbody = "";
            var authorizationToken = _context.GetAccessToken(jsonbody.ToString());

            var client = new RestClient(Constants.BASE_URL);
            var request = new RestRequest(Constants.ORDER_CREATE, Method.POST);
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
        //Test 2 - create order without authorization
        //Test 3 - create order with instruction
        //Test 4 - create order without accountid,wrong accountid
        //Test 5 - create order without tenantid,wrong tenantid
        //Test 6 - create order with body containing all the optional values

        public CreateOrderDto GetOrderRequestBody()
        {
            CreateOrderDto objCreateOrder = new CreateOrderDto()
            {
                orderRef = Guid.NewGuid().ToString(),
                metadata = new Metadata { key1 = GetRandomString(), key2 = GetRandomString(), key3 = GetRandomString() },
                customerDefaults = new Customerdefaults { }
            };

            return objCreateOrder;
        }
        public string GetRandomString()
        {
            var randomString = Path.GetRandomFileName().Substring(0, 6);
            return randomString;
        }
        public string generateHmac()
        {
            string publicKey = Constants.PUBLIC_KEY;
            string privateKey = Constants.PRIVATE_KEY;

            var httpRequestBodyContent = GetOrderRequestBody().ToString();
            var nonce = CreateNonce();
            var timeStamp = (int)(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;

            //hash the body
            var hashedBody = SignBody(httpRequestBodyContent);
            //create the unsigned signature
            var unsignedSignature = $"{publicKey}:{nonce}:{timeStamp}:{hashedBody}";
            //generate the hmac signature
            var hmacToken = CreateToken(privateKey, unsignedSignature);
            //create signed signature
            var signedSignature = $"{publicKey}:{nonce}:{timeStamp}:{hmacToken}";

            return signedSignature;
            //Console.WriteLine($"hmac {signedSignature}");
            //Console.Read();
        }
        public static string CreateToken(string secret, string message)
        {
            byte[] keyByte = Convert.FromBase64String(secret);
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        private static string CreateNonce()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }
        private static string SignBody(string body)
        {
            if (body == string.Empty)
                return body;

            var bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(bodyBytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
