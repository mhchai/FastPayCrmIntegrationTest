using System.Collections.Generic;

namespace FastPayCrmIntegrationTest
{
    public class CampaignDetailsModel
    {
        public string campaignId { get; set; }
        public string campaignName { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public string campaignImageUrl { get; set; }
        public string campaignType { get; set; }
        public string category { get; set; }
        public List<MerchantDetailsModel> merchantDetailList { get; set; }
        public DiscountDetailsModel discountDetail { get; set; }
        public List<string> paymentOptions { get; set; }
    }
}
