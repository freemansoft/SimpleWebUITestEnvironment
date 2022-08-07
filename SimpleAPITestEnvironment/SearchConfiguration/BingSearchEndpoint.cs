namespace SimpleAPITestEnvironment.SearchConfiguration
{
    // Add your Bing Search V7 subscription key and endpoint to your environment variables - .runsettings if running in visual studio
    public class BingSearchEndpoint : ISearchEndpoint
    {
        public string ApiEndpoint()
        {
            return Environment.GetEnvironmentVariable("BING_SEARCH_V7_ENDPOINT") + "/v7.0/search";
        }

        public string SubscriptionKey()
        {
            return Environment.GetEnvironmentVariable("BING_SEARCH_V7_SUBSCRIPTION_KEY") ?? "";
        }
    }
}
