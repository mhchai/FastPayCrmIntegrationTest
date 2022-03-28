using System.Collections.Generic;

namespace FastPayCrmIntegrationTest
{
    public class CompaignListMessageModel
    {
        public string searchText { get; set; }
        public int itemsPerPage { get; set; }
        public int pagingNo { get; set; }
        public bool hasNext { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public List<FilterModel> filterList { get; set; }
        public List<CampaignDetailsModel> campaignDetailList { get; set; }
    }
}
