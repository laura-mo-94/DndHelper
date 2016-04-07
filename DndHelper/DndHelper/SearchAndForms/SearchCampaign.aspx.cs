using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Web.ModelBinding;

namespace DndHelper.SearchAndForms
{
    public partial class SearchCampaign : System.Web.UI.Page
    {
        private SiteMaster master;
        private Player pagePlayer;
        private List<Campaign> results;
        private List<Campaign> currentResults;

        private int currentPage = 1;
        private int itemsPerPage = 10;

        public string resultSummaryString = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;
            results = new List<Campaign>();
            
            if (String.IsNullOrEmpty(Request.QueryString["user"]))
            {
                pagePlayer = master.GetPlayer(Context.User.Identity.Name);
            }
            else
            {
                pagePlayer = master.GetPlayer(Request.QueryString["user"]);
            }
        }

        public void getSearchResults(object sender, System.EventArgs e)
        {
            string modeName = Mode.SelectedValue;

            string keywords = SearchBox.Text;
            string sumText = "";

            if(keywords.Length > 0)
            {
                if (modeName.Equals("By Name"))
                {
                    results = master.GetCampaigns().Where(x => keywords.Contains(x.CampaignName) || x.CampaignName.Contains(keywords)).ToList<Campaign>();
                    sumText = " containing \"" + keywords + "\" in their name";
                }
                else if (modeName.Equals("By Dungeon Master"))
                {
                    IQueryable<Campaign> initial = master.GetCampaigns(keywords);
                    if (initial != null && initial.Count() > 0)
                    {
                        int dmId = master.GetPlayer(keywords).PlayerId;
                        results = initial.Where(x => x.DungeonMasterID == dmId).ToList<Campaign>();
                        sumText = " with " + keywords + " as the dungeon master";
                    }
                    else
                    {
                        results.Clear();
                    }
                }
                else if (modeName.Equals("By Participant"))
                {
                    results = master.GetCampaigns(keywords).ToList<Campaign>();
                    sumText = " with " + keywords + " as a participant";
                }
                else
                {
                    results.Clear();
                }

                if (results.Count() <= 0)
                {
                    results.Clear();
                    resultSummaryString = "";
                }
                else
                {
                    resultSummaryString = results.Count() + " campaigns" + sumText;
                }

                //SearchResults.DataSource = results;
                SearchResults.DataBind();
                searchSummary.DataBind();
            }
           
        }

        public void paginate(int page)
        {
            int startIndex = (page - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage - 1;

        }

        public List<Campaign> getResults()
        {
            return results;
        }

        public Player getPagePlayer()
        {
            return pagePlayer;
        }

        public string getURL(String baseURL, int id, string user)
        {
            return master.getURL(baseURL, id, user);
        }
    }

    public partial class CampaignHelper
    {
        public string CampaignName;
        public string DungeonMaster;
    }
}