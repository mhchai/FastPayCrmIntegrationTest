using Newtonsoft.Json;

namespace FastPayCrmIntegrationTest
{
    public class CouponDetailModel
    {
        [JsonProperty("couponNo", Required =Required.Always)]
        public string CouponNo { get; set; }

        [JsonProperty("discountDetail", Required = Required.Always)]
        public CouponReqDiscountDetailModel DiscountDetail { get; set; }

    }
}
