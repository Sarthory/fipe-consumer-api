namespace FipeConsumer.Infrastructure.Configuration
 {
     public class FipeApiConfig
    {
        public required string BaseUrl { get; set;}
        public required string Token { get;set; }

        public FipeApiConfig(string baseUrl, string token) : this()
        {
            BaseUrl = baseUrl;
            Token = token;
        }
        public FipeApiConfig(){}
    }
 }
