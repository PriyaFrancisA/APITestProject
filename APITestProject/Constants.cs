namespace APITestProject
{
   public class Constants
    {
        public const string BASE_URL = "https://sandbox-api.imbursepayments.com/";

        //Order Management 
        public const string ORDER_CREATE = "/v1/order-management";
        public const string INSTRUCTION_CREATE = "/v1/order-management/{orderRef}/instruction";

        //Authentication Api 
        public const string AUTHENTICATION_API_GET_ACCESS_TOKEN = "/v1/identity/hmac";

        public const string ACCOUNT_ID = "782f1b71-7ca4-4465-917f-68d58ffbec8b";
        public const string TENANT_ID = "6423ae63-59b6-4986-a949-c910ac622471";
        public const string PUBLIC_KEY = "7934d5e6-260c-46d5-9309-e72a59cb90cd";
        public const string PRIVATE_KEY = "aWRpTN9tRsf2EyM8rcvz7bohO/fAg6IF+daZ1JYE9AM=";
    }
}
