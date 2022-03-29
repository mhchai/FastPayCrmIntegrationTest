using Newtonsoft.Json;

namespace FastPayCrmIntegrationTest
{
    public class ValidateCouponReqModel
    {
        [JsonProperty("superksId")]
        public string SuperksId { get; set; }

        [JsonProperty("terminalAccountId", Required = Required.Always)]
        public string TerminalAccountId { get; set; }

        [JsonProperty("partnerCode", Required = Required.Always)]
        public string PartnerCode { get; set; }

        [JsonProperty("couponDetail", Required = Required.Always)]
        public CouponDetailModel CouponDetail { get; set; }


    }
}
