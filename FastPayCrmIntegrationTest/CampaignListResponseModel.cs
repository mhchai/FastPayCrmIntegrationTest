namespace FastPayCrmIntegrationTest
{
    public class CampaignListResponseModel
    {
        public string signature { get; set; }
        public string timestamp { get; set; }
        public CompaignListMessageModel message { get; set; }
        public string responseStatus { get; set; }
    }
}
