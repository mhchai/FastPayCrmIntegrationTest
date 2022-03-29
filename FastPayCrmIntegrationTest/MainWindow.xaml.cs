using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastPayCrmIntegrationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnListCampaign_Click(object sender, RoutedEventArgs e)
        {
            string apiendpoint = "ss_controller_war/apiController/generic/campaign/getCampaignList";
            //string timestamp = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond).ToString();
            string timestamp = "1613642850926";
            string messagestr = GenerateListCampaignJsonString();

            string privatekeystr = File.ReadAllText(@"C:\Temp\Clientprivate.pem");

            string signature = SignatureHelper.ComputeSHA256withRSA3(timestamp + PmkTextBox.Text + messagestr, privatekeystr);
            //string signature = "bb7bb5828554cdfde041c9c7cb7de3a3cf3f3c8f18d84657e3430d529135df25dea3d11897957da7f7dbb4fd4a7ada6b252dfb38dd2882a02d32bc60ae4bd27ad8f7cc1e0a4678c42ca282be798830a799a332dae07cdbc9a51c0475133ee096f79495961fa2fd46f0bc5d37bc80b48a6d2a1778c71befc17967e136ad93f84a2f1d8ffbbf7ca65e3166633ab6603fdf32241a60042cdc8c3655a400972167572dcfd3109f05eaebc3424efd9e12a3380b969602dbc08803ed7ec776f5cfb93ab6e4d636ffe2e6a1a8d62410f307ba0067dbe48881bdeb9478c5ede4f5cbc2cedb79fe85ddffa90722ab9a13a047c38defb44181f46ed9342e89e133eede4416";

            //string payload = GeneratePayLoadJsonStr(signature, timestamp, PmkTextBox.Text, messagestr);
            string payload = GeneratePayLoadJsonStr(signature, timestamp, "38303032304b51345a4a3935304c4246", messagestr);

            var httpClient = new HttpClient();

            StringContent jsoncontent = new StringContent(payload);
            jsoncontent.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            jsoncontent.Headers.Add("pid", "80020");
            jsoncontent.Headers.Add("i_pid", "80020WEB");
            jsoncontent.Headers.Add("i_mode", "INTEGRATE");

            var result = await httpClient.PostAsync(ApiUrlTextBox.Text + apiendpoint, jsoncontent);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)3072;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var test = await result.Content.ReadAsStringAsync();

                var jobj = JsonConvert.DeserializeObject<object>(test);
            }

            //HttpWebRequest webreq = WebRequest.Create(ApiUrlTextBox.Text + apiendpoint) as HttpWebRequest;
            //webreq.Headers.Add("pid", PidTextBox.Text);
            //webreq.Headers.Add("i_pid", $"{PidTextBox.Text}WEB");
            //webreq.Headers.Add("i_mode", "INTEGRATE");
            //webreq.ContentType = "application/json";
            //webreq.Accept = "*/*";
            //webreq.Method = "POST";

            //using (StreamWriter streamWritter = new(webreq.GetRequestStream()))
            //{
            //    streamWritter.WriteLine(payload);
            //}


            //HttpWebResponse webresp = webreq.GetResponse() as HttpWebResponse;
            
            //using StreamReader streamReader = new(webresp.GetResponseStream());
            //string resultstring = streamReader.ReadToEnd();
        }

        private void BtnViewCampaign_Click(object sender, RoutedEventArgs e)
        {
            string apiendpoint = "ss_controller_war/apiController/generic/campaign/getCampaignList";
            string timestamp = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond).ToString(); 
            string messagestr = GenerateListCampaignJsonString();
            string privatekeystr = File.ReadAllText(PrivateKeyFileLocation.Text);

            string signature = SignatureHelper.ComputeSHA256withRSA3(timestamp + PmkTextBox.Text + messagestr, privatekeystr);

            HttpWebRequest webreq = WebRequest.Create(ApiUrlTextBox.Text + apiendpoint) as HttpWebRequest;
            webreq.Headers.Add("pid", PidTextBox.Text);
            webreq.Headers.Add("i_pid", $"{PidTextBox.Text}WEB");
            webreq.Headers.Add("i_mode", "INTEGRATE");
            webreq.ContentType = "application/json";
            webreq.Accept = "*/*";
            webreq.Method = "POST";

            string payload = GeneratePayLoadJsonStr(signature, timestamp, PmkTextBox.Text, messagestr);

            using (StreamWriter streamWritter = new(webreq.GetRequestStream()))
            {
                streamWritter.WriteLine(payload);
            }


            HttpWebResponse webresp = webreq.GetResponse() as HttpWebResponse;

            using StreamReader streamReader = new(webresp.GetResponseStream());
            string resultstring = streamReader.ReadToEnd();
        }

        private async void BtnListCampaign2_Click(object sender, RoutedEventArgs e)
        {
            string apiendpoint = "ss_controller_war/apiController/generic/campaign/getCampaignList";
            string timestamp = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond).ToString();
            string messagestr = GenerateListCampaignJsonString();
            string privatekeystr = File.ReadAllText(PrivateKeyFileLocation.Text);

            string signature = SignatureHelper.ComputeSHA256withRSA3(timestamp + PmkTextBox.Text + messagestr, privatekeystr);
            string payload = GeneratePayLoadJsonStr(signature, timestamp, PmkTextBox.Text, messagestr);

            HttpClient httpClient = new HttpClient();

            StringContent jsoncontent = new StringContent(payload);
            jsoncontent.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            jsoncontent.Headers.Add("pid", "80020");
            jsoncontent.Headers.Add("i_pid", "80020WEB");
            jsoncontent.Headers.Add("i_mode", "INTEGRATE");

            HttpResponseMessage result = await httpClient.PostAsync(ApiUrlTextBox.Text + apiendpoint, jsoncontent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                string test = await result.Content.ReadAsStringAsync();

                ResponseModel responsedata = JsonConvert.DeserializeObject<ResponseModel>(test);

                var responsemessage = responsedata.message;

                CompaignListMessageModel message = JsonConvert.DeserializeObject<CompaignListMessageModel>(responsemessage);
            }
        }


        private string GenerateListCampaignJsonString()
        {

            string jsonstr = JsonConvert.SerializeObject(new
            {                
                filterList = new List<object>
                {
                    new {filterTypeId = "CAMPAIGN_TYPE",filterValues = new List<string>{ "TRANSACTIONAL_PROMOTION", "CAMPAIGN" } }
                },
                hasNext = false,
                itemsPerPage = 10,
                pagingNo = 1
            });

            return jsonstr;
        }       


        private static string GeneratePayLoadJsonStr(string signaturestr, string timestampstr,string pmkstr, string messagestr)
        {
            string jsonstr = JsonConvert.SerializeObject(new
            {
                signature = signaturestr,
                timestamp = timestampstr,
                pmk = pmkstr,
                message = messagestr
            });

            return jsonstr;
        }

        private async void BtnValidateCoupon_Click(object sender, RoutedEventArgs e)
        {
            string apiendpoint = "ss_controller_war/apiController/generic/coupon/doValidateCoupon";
            string timestamp = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond).ToString();
            string messagestr = GenerateValidateCouponMessageString();
            string privatekeystr = File.ReadAllText(PrivateKeyFileLocation.Text);

            string signature = SignatureHelper.ComputeSHA256withRSA3(timestamp + PmkTextBox.Text + messagestr, privatekeystr);

            string payload = GeneratePayLoadJsonStr(signature, timestamp, PmkTextBox.Text, messagestr);

            HttpClient httpClient = new HttpClient();

            StringContent jsoncontent = new StringContent(payload);
            jsoncontent.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            jsoncontent.Headers.Add("pid", PidTextBox.Text);
            jsoncontent.Headers.Add("i_pid", $"{PidTextBox.Text}WEB");
            jsoncontent.Headers.Add("i_mode", "INTEGRATE");

            HttpResponseMessage result = await httpClient.PostAsync(ApiUrlTextBox.Text + apiendpoint, jsoncontent);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                string test = await result.Content.ReadAsStringAsync();

                ResponseModel responsedata = JsonConvert.DeserializeObject<ResponseModel>(test);


            }

        }

        private void BtnRedeemCoupon_Click(object sender, RoutedEventArgs e)
        {

        }

        private string GenerateValidateCouponMessageString()
        {
            ValidateCouponReqModel validateCouponRequest = new ValidateCouponReqModel
            {
                SuperksId = SuperksIdTextBox.Text,
                TerminalAccountId = TerminalAccountIdTextBox.Text,
                PartnerCode = PidTextBox.Text,
                CouponDetail = new CouponDetailModel
                {
                    CouponNo = CouponNumberTextBox.Text,
                    DiscountDetail = new CouponReqDiscountDetailModel
                    {
                        Amount = CouponAmountTextBox.Text,
                        CurrencyCode = "458"
                    }
                }
            };

            return JsonConvert.SerializeObject(validateCouponRequest);
        }
    }
}
