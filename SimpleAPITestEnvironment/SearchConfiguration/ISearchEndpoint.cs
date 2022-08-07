namespace SimpleAPITestEnvironment.SearchConfiguration
{
    /// <summary>
    /// This functionality should be injected via Factory
    /// </summary>
    public interface ISearchEndpoint
    {
        public string SubscriptionKey();
        public string ApiEndpoint();
    }
}
