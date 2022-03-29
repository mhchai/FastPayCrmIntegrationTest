using Newtonsoft.Json;

namespace FastPayCrmIntegrationTest
{
    public class CouponReqDiscountDetailModel
    {
        [JsonProperty("amount", Required = Required.Always)]
        public string Amount { get; set; }

        [JsonProperty("currencyCode", Required =Required.Always)]
        public string CurrencyCode { get; set; }

    }
}
