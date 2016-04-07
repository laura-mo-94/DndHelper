using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Web.ModelBinding;
using System.Diagnostics;
using System.Collections.Specialized;

namespace DndHelper.Account
{
    public partial class UserPage : System.Web.UI.Page
    {
        SiteMaster master;
        Player pagePlayer;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;

            if(String.IsNullOrEmpty(Request.QueryString["user"]))
            {
                pagePlayer = master.GetPlayer(Context.User.Identity.Name);
            }
            else
            {
                pagePlayer = master.GetPlayer(Request.QueryString["user"]);
            } 
        }

        public IQueryable<Campaign> LoadCampaigns()
        {
            IQueryable<Campaign> cam = master.GetCampaigns(pagePlayer.PlayerName);
            if(cam == null)
            {
                cam = Enumerable.Empty<Campaign>().AsQueryable();
            }

            return cam;
        }

        public IQueryable<Player> LoadPlayers([Control("CampaignID")] int campaignId)
        {
            IQueryable<Player> cam = master.GetPlayersInCampaign(campaignId, pagePlayer.PlayerId);
            if (cam == null)
            {
                cam = Enumerable.Empty<Player>().AsQueryable();
            }

            return cam;
        }

        public IQueryable<Character> LoadCharacters()
        {
            IQueryable<Character> chara = master.GetCharacters(pagePlayer.PlayerName);
            if (chara == null)
            {
                chara = Enumerable.Empty<Character>().AsQueryable();
            }

            return chara;
        }

        public string getURL(String baseURL, int id, string user)
        {
            return master.getURL(baseURL, id, user);
        }

        public Player getPagePlayer()
        {
            return pagePlayer;
        }

        public bool ownsPage()
        {
            return pagePlayer.PlayerName.Equals(Context.User.Identity.Name);
        }

        public string getCharacterOwner(int id)
        {
            return master.GetPlayer(id).PlayerName;
        }
        
    }
}