using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Linq;
using DndHelper.Models;
using System.Diagnostics;
using System.Collections.Specialized;

namespace DndHelper
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IQueryable<Campaign> GetCampaigns()
        {
            IQueryable<Campaign> query;

            var context = new PlayerContext();
            
            query = context.Campaigns;
            return query;
        }

        public Campaign GetCampaign(int id)
        {
            var context = new PlayerContext();
            IQueryable<Campaign> c = context.Campaigns.Where(x => x.CampaignId == id);
            return c.First<Campaign>();
        }

        public IQueryable<Character> GetCharacters()
        {
            IQueryable<Character> query;
            var context = new PlayerContext();
            query = context.Characters;
            return query;
        }

        public Character GetCharacter(int id)
        {
            Character chara;
            var context = new PlayerContext();
            chara = context.Characters.Where(x => x.CharacterID == id).First<Character>();
            return chara;
        }

        public Character GetCharacter(int campaignID, int playerID)
        {
            var context = new PlayerContext();
            return GetCharacter(context.CampaignsToPlayers.Where(x => x.CampaignId == campaignID && x.PlayerId == playerID).First<CampaignsToPlayers>().CharacterID);
        }

        public IQueryable<Player> GetPlayers()
        {
            IQueryable<Player> query;
            var context = new PlayerContext();
            
            query = context.Players;
            return query;
        }

        public Player GetPlayer(int id)
        {
            var context = new PlayerContext();
            IQueryable<Player> players = context.Players.Where(x => x.PlayerId == id);

            if (players.Count() > 0)
            {
                return players.First<Player>();
            }

            return null;
        }

        public Player GetPlayer(String player)
        {
            var context = new PlayerContext();

            IQueryable<Player> players = context.Players.Where(x => x.PlayerName.Equals(player));

            if(players.Count() > 0)
            {
                return players.First<Player>();
            }

            return null; 
        }

        public IQueryable<Campaign> GetCampaigns(String player)
        {
            IQueryable<CampaignsToPlayers> res;

            var context = new PlayerContext();
            
            res = context.CampaignsToPlayers.Where(x => x.PlayerName.Equals(player));
            List<int> campaignIDs = new List<int>();
            if (res.Count() > 0)
            {
                foreach (CampaignsToPlayers rel in res)
                {
                    campaignIDs.Add(rel.CampaignId);
                }

                return context.Campaigns.Where(x => campaignIDs.Contains(x.CampaignId));
            }
            
            return null;
        }

        public IQueryable<Campaign> GetCampaigns(int characterID)
        {
            IQueryable<CampaignsToPlayers> res;

            var context = new PlayerContext();

            res = context.CampaignsToPlayers.Where(x => x.CharacterID == characterID);
            List<int> campaignIDs = new List<int>();
            if (res.Count() > 0)
            {
                foreach (CampaignsToPlayers rel in res)
                {
                    campaignIDs.Add(rel.CampaignId);
                }

                return context.Campaigns.Where(x => campaignIDs.Contains(x.CampaignId));
            }

            return null;
        }

        public IQueryable<Player> GetPlayersInCampaign(int campaignId, int excludeId = 0)
        {
            IQueryable<CampaignsToPlayers> res;

            var context = new PlayerContext();
            res = context.CampaignsToPlayers.Where(x => x.CampaignId == campaignId && x.PlayerId != excludeId);
            List<int> playerIDs = new List<int>();

            if (res.Count() > 0)
            {
                foreach (CampaignsToPlayers rel in res)
                {
                    playerIDs.Add(rel.PlayerId);
                }

                return context.Players.Where(x => playerIDs.Contains(x.PlayerId));
            }

            return null;
        }


        public IQueryable<Character> GetCharacters(String player)
        {
            IQueryable<CharactersToPlayers> res;

            var context = new PlayerContext();
            
            res = context.CharactersToPlayers.Where(x => x.PlayerName.Equals(player));
            List<int> characterIDs = new List<int>();
            if (res.Count() > 0)
            {
                foreach (CharactersToPlayers rel in res)
                {
                    characterIDs.Add(rel.CharacterId);
                }

                return context.Characters.Where(x => characterIDs.Contains(x.CharacterID));
            }
            
            return null;
        }
        
        public bool inCampaign(string player, int campaignId)
        {
            IQueryable<CampaignsToPlayers> res;
            var context = new PlayerContext();

            res = context.CampaignsToPlayers.Where(x => x.PlayerName.Equals(player) && x.CampaignId == campaignId);

            if(res.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public string getURL(string baseURL, int id, string user)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["id"] = id.ToString();
            queryString["user"] = user;
            return baseURL + "?" + queryString.ToString();
        }

        public string getURL(string baseURL, List<string> keys, List<string> vals)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            for(int i = 0; i < keys.Count; i++)
            {
                if(i < vals.Count)
                {
                    queryString[keys[i]] = vals[i];
                }
                else
                {
                    break;
                }
            }

            return baseURL + "?" + queryString.ToString();
        }
    }

}