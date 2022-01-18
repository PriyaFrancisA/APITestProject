using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Security.Cryptography;

namespace APITestProject
{
   public class Context
    {
        public IRestResponse PostHttpMethod(string baseUrl, string endpoint, string jsonbody, HttpStatusCode expectedHttpStatusCode)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            request.AddHeader("user-id", "AutoApiTest");
            request.AddParameter("application/json", jsonbody, ParameterType.RequestBody);
            IRestResponse response = new RestResponse();
            response = client.Execute(request);
            response.StatusCode.Should().Be(expectedHttpStatusCode);
          
            //log
            Console.WriteLine("This is the status code from PostHttpMethod method " + response.StatusCode);
            return response;
        }

        public string GetAccessToken(string HttpRequestBodyContent)
        {
            string signature = GetSignedSignature(HttpRequestBodyContent);

            var client = new RestClient(Constants.BASE_URL);
            var request = new RestRequest(Constants.AUTHENTICATION_API_GET_ACCESS_TOKEN, Method.POST);
            request.AddHeader("Authorization", string.Format("hmac {0}", signature));

            IRestResponse response = client.Execute<dynamic>(request);
            var responseDetails = JsonConvert.DeserializeObject<dynamic>(response.Content);
            string accessToken = responseDetails.accessToken;
            return accessToken;
        }

        public string GetSignedSignature(string HttpRequestBodyContent)
        {
            string publicKey = Constants.PUBLIC_KEY;
            string privateKey = Constants.PRIVATE_KEY;

            var httpRequestBodyContent = HttpRequestBodyContent;
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
