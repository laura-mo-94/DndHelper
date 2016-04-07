using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;

namespace DndHelper.SearchAndForms
{
    public partial class CreateCampaign : System.Web.UI.Page
    {
        int dm;
        SiteMaster master;
        bool editing;
        Campaign currentCampaign;

        protected void Page_Load(object sender, EventArgs e)
        {
            Message.Visible = false;
            master = Page.Master as SiteMaster;
            Int32.TryParse(Request.QueryString["id"], out dm);

            if (Request.QueryString.Get("cid") != null)
            {
                Execute.Text = "Save";
                editing = true;
                currentCampaign = master.GetCampaign(Int32.Parse(Request.QueryString["cid"]));

                if(!Page.IsPostBack)
                {
                    loadInfo();
                }
            }
            else
            {
                Execute.Text = "Create";
            }
        }

        public void loadInfo()
        {
            CampaignName.Text = currentCampaign.CampaignName;
            CampaignDescription.Text = currentCampaign.Description;
        }

        public void executeCampaignAction(object sender, System.EventArgs e)
        {
            if(!editing)
            {
                createCampaign();
            }
            else
            {
                saveResults();
            }
        }

        public void saveResults()
        {
            using (var context = new PlayerContext())
            {
                currentCampaign.CampaignName = CampaignName.Text;
                currentCampaign.Description = CampaignDescription.Text;
                context.Campaigns.Attach(currentCampaign);
           
                context.Entry(currentCampaign).Property(n => n.CampaignName).IsModified = true;
                context.Entry(currentCampaign).Property(n => n.Description).IsModified = true;
                context.SaveChanges();

                Server.Transfer(master.getURL("../Account/UserPage.aspx", dm, Context.User.Identity.Name));
            }
        }

        public void createCampaign()
        {
            if (string.IsNullOrWhiteSpace(CampaignName.Text))
            {
                Message.Visible = true;
                Message.Text = "Please give the campaign a name!";
            }
            else
            {
                using (var context = new PlayerContext())
                {
                    Campaign campaign = new Campaign();
                    campaign.CampaignName = CampaignName.Text;
                    campaign.Description = CampaignDescription.Text;

                    campaign.DungeonMasterID = dm;
                    context.Campaigns.Add(campaign);
                    context.SaveChanges();

                    CampaignsToPlayers rel = new CampaignsToPlayers();
                    rel.CampaignId = campaign.CampaignId;
                    rel.CampaignName = campaign.CampaignName;
                    rel.PlayerId = dm;
                    rel.PlayerName = Context.User.Identity.Name;
                    context.CampaignsToPlayers.Add(rel);
                    context.SaveChanges();
                    Server.Transfer(master.getURL("../Account/UserPage.aspx", dm, Context.User.Identity.Name));
                }
            }
        }
    }
}