using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Web.ModelBinding;

namespace DndHelper.Account
{
    public partial class CampaignPage : System.Web.UI.Page
    {
        private Campaign currentCampaign;
        private Player DM;
        private Character currentCharacter;
        private SiteMaster master;
        private string user;
        private Player playerPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;
            user = Request.QueryString["user"];
            playerPage = master.GetPlayer(user);
            setUp();
        }

        public void setUp()
        {
            try
            {
                currentCampaign = master.GetCampaign(int.Parse(Request.QueryString["id"]));
                DM = master.GetPlayer(currentCampaign.DungeonMasterID);
                Edit.Visible = false;
                Join.Visible = false;
                Delete.Visible = false;
                Leave.Visible = false;
                Start.Visible = false;

                if (DM.PlayerName.Equals(user) || !master.inCampaign(Context.User.Identity.Name, currentCampaign.CampaignId))
                {
                    currentCharacter = null;

                    if((!DM.PlayerName.Equals(Context.User.Identity.Name) && !DM.PlayerName.Equals(user)))
                    {
                        activeChar.Visible = false;
                        Join.Visible = true;
                    }
                    else if(DM.PlayerName.Equals(Context.User.Identity.Name))
                    {
                        Edit.Visible = true;
                        Delete.Visible = true;
                        Start.Visible = true;
                    }
                }
                else
                {
                    currentCharacter = master.GetCharacter(currentCampaign.CampaignId, master.GetPlayer(user).PlayerId);

                    if(user.Equals(Context.User.Identity.Name))
                    {
                        Leave.Visible = true;
                    }
                  
                }
               
            }
            catch (Exception e)
            {
                Response.Redirect("../Account/ErrorPage.html");
            }

        }
        
        public void joinCampaign(object sender, System.EventArgs e)
        {
            List<string> keys = new List<string>();
            List<string> vals = new List<string>();

            keys.Add("id");
            keys.Add("pid");
            keys.Add("cname");
            keys.Add("user");
            string currentUser = Context.User.Identity.Name;
            Player p = master.GetPlayer(currentUser);

            vals.Add(currentCampaign.CampaignId.ToString());
            vals.Add(p.PlayerId.ToString());
            vals.Add(currentCampaign.CampaignName);
            vals.Add(currentUser);

            Response.Redirect(master.getURL("../SearchAndForms/JoinCampaign.aspx", keys, vals));
        }

        public void leaveCampaign(object sender, System.EventArgs e)
        {
            using (var context = new PlayerContext())
            {
                CampaignsToPlayers rel = context.CampaignsToPlayers.Where(x => x.CampaignId == currentCampaign.CampaignId && x.PlayerName.Equals(Context.User.Identity.Name)).First<CampaignsToPlayers>();

                context.CampaignsToPlayers.Attach(rel);
                context.CampaignsToPlayers.Remove(rel);
                context.SaveChanges();
            }

            Response.Redirect(master.getURL("../Account/UserPage.aspx", playerPage.PlayerId, playerPage.PlayerName));
        }

        public void editCampaign(object sender, System.EventArgs e)
        {
            List<string> keys = new List<string>();
            List<string> vals = new List<string>();

            keys.Add("id");
            keys.Add("cid");
            keys.Add("user");

            vals.Add(playerPage.PlayerId.ToString());
            vals.Add(currentCampaign.CampaignId.ToString());
            vals.Add(playerPage.PlayerName);

            Response.Redirect(master.getURL("../SearchAndForms/CreateCampaign.aspx", keys, vals));
        }

        public void deleteCampaign(object sender, System.EventArgs e)
        {
            using (var context = new PlayerContext())
            {
                CampaignsToPlayers rel = context.CampaignsToPlayers.Where(x => x.CampaignId == currentCampaign.CampaignId && x.PlayerName.Equals(Context.User.Identity.Name)).First<CampaignsToPlayers>();
                context.Campaigns.Attach(currentCampaign);
                context.Campaigns.Remove(currentCampaign);

                context.CampaignsToPlayers.Attach(rel);
                context.CampaignsToPlayers.Remove(rel);
                context.SaveChanges();
            }

            Response.Redirect(master.getURL("../Account/UserPage.aspx", playerPage.PlayerId, playerPage.PlayerName));
        }

        public void startCampaign(object sender, System.EventArgs e)
        {
            if(DM.PlayerName.Equals(Context.User.Identity.Name))
            {
                Response.Redirect(master.getURL("../SearchAndForms/RunCampaign.aspx", currentCampaign.CampaignId, Context.User.Identity.Name));
            }
        }

        public Campaign getCurrentCampaign()
        {
            return currentCampaign;
        }

        public Character getActiveCharacter()
        {
            return currentCharacter;
        }

        public Player getDM()
        {
            return DM;
        }

        public IQueryable<Player> getParticipants()
        {
            return master.GetPlayersInCampaign(currentCampaign.CampaignId, DM.PlayerId);
        }

        public string getURL(String baseURL, int id)
        {
            return master.getURL(baseURL, id, user);
        }

        public string getURL(String baseURL, int id, String u)
        {
            return master.getURL(baseURL, id, u);
        }

        public Player getPageOwner()
        {
            return playerPage;
        }
        
    }
}