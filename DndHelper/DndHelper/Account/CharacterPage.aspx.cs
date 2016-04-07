using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Web.ModelBinding;
using System.Web.Services;
using System.Text;

namespace DndHelper.Account
{
    public partial class CharacterPage : System.Web.UI.Page
    {
        public Character currentCharacter;
        private SiteMaster master;
        private string user;
        private Player playerPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;
            user = Request.QueryString["user"];
            playerPage = master.GetPlayer(user);
          
            setUp();

            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseStrength.ToString());
            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseDexterity.ToString());
            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseConstitution.ToString());
            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseIntelligence.ToString());
            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseWisdom.ToString());
            ClientScript.RegisterArrayDeclaration("baseStats", currentCharacter.BaseCharisma.ToString());

            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseStrength + currentCharacter.StrengthModifier).ToString());
            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseDexterity + currentCharacter.DexterityModifier).ToString());
            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseConstitution + currentCharacter.ConstitutionModifier).ToString());
            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseIntelligence + currentCharacter.IntelligenceModifier).ToString());
            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseWisdom + currentCharacter.WisdomModifier).ToString());
            ClientScript.RegisterArrayDeclaration("modifiedStats", (currentCharacter.BaseCharisma + currentCharacter.CharismaModifier).ToString());

            ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "loadChart();", true);
        }

        private string getArrayString(int[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i] + ",");
            }
            string arrayStr = string.Format("[{0}]", sb.ToString().TrimEnd(','));
            return arrayStr;
        }

        public void setUp()
        {
            try
            {
                currentCharacter = master.GetCharacter(int.Parse(Request.QueryString["id"]));       
            } catch(Exception e)
            {
                Response.Redirect("../Account/ErrorPage.html");
            }

            Edit.Visible = false;
            Delete.Visible = false;
            
            if(user.Equals(Context.User.Identity.Name))
            {
                Edit.Visible = true;
                Delete.Visible = true;
            }

        }

        [WebMethod]
        public Character getCurrentCharacter()
        {
            return currentCharacter;
        }

        public IQueryable<Campaign> getActiveCampaigns()
        {
            return master.GetCampaigns(currentCharacter.CharacterID);
        }

        public string getURL(String baseURL, int id)
        {
            return master.getURL(baseURL, id, user);
        }

        public Player getPageOwner()
        {
            return playerPage;
        }

        public void deleteCharacter(object sender, System.EventArgs e)
        {
            using (var context = new PlayerContext())
            {
                CharactersToPlayers rel = context.CharactersToPlayers.Where(x => x.CharacterId == currentCharacter.CharacterID && x.PlayerName.Equals(Context.User.Identity.Name)).First<CharactersToPlayers>();
                IQueryable<Campaign> campaigns = master.GetCampaigns(currentCharacter.CharacterID);
                List<CampaignsToPlayers> campRels = new List<CampaignsToPlayers>();

               foreach(Campaign cam in campaigns)
                {
                    campRels.AddRange(context.CampaignsToPlayers.Where(x => x.CharacterID == currentCharacter.CharacterID && cam.CampaignId == x.CampaignId));
                }
                context.Characters.Attach(currentCharacter);
                context.Characters.Remove(currentCharacter);

                context.CharactersToPlayers.Attach(rel);
                context.CharactersToPlayers.Remove(rel);

                for(int i = 0; i < campRels.Count; i++)
                {
                    context.CampaignsToPlayers.Attach(campRels[i]);
                    context.CampaignsToPlayers.Remove(campRels[i]);
                }

                context.SaveChanges();
            }

            Response.Redirect(master.getURL("../Account/UserPage.aspx", playerPage.PlayerId, playerPage.PlayerName));
        }

        public void editCharacter(object sender, System.EventArgs e)
        {
            List<string> keys = new List<string>();
            List<string> vals = new List<string>();

            keys.Add("cid");
            vals.Add(currentCharacter.CharacterID.ToString());

            Response.Redirect(master.getURL("../SearchAndForms/CreateCharacter.aspx", keys, vals));
        }
    }
}