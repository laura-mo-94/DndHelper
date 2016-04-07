using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Web.ModelBinding;
using System.Data.Entity;

namespace DndHelper.SearchAndForms
{
    public partial class RunCampaign : System.Web.UI.Page
    {
        SiteMaster master;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;
            
            if (!Page.IsPostBack)
            {
                Dictionary < int, Character > idToCharacterMap;
                List<Character> activePlayers = new List<Character>();
                Campaign session = master.GetCampaign(Int32.Parse(Request.QueryString["id"]));
                idToCharacterMap = new Dictionary<int, Character>();

                IQueryable<Player> p = master.GetPlayersInCampaign(session.CampaignId, session.DungeonMasterID);

                foreach (Player pl in p)
                {
                    Character c = master.GetCharacter(session.CampaignId, pl.PlayerId);
                    activePlayers.Add(c);

                    idToCharacterMap.Add(c.CharacterID, c);
                }
                
                ViewState["characterMap"] = idToCharacterMap;
                ViewState["session"] = session;

                if (p != null && p.Count() > 0)
                {
                    CharacterStats.DataSource = activePlayers;
                    CharacterStats.DataBind();
                }

            }

            if (!Context.User.Identity.Name.Equals(Request.QueryString["user"]))
            {
                Response.Redirect("../Account/ErrorPage.html");
            }
        }
        
        public void ChangeStats(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Character target = (ViewState["characterMap"] as Dictionary<int, Character>) [Int32.Parse(b.Attributes["owner"])];
           
            string field = b.Attributes["field"];

            if(field.Equals("Level"))
            {
                target.AdjustLevel(Int32.Parse(b.Attributes["value"]));
            }
            else if(field.Equals("Health"))
            {
                target.ChangeHealth(Int32.Parse(b.Attributes["value"]));
            }
            else if(field.Equals("MaxHealth"))
            {
                target.ChangeMaxHealth(Int32.Parse(b.Attributes["value"]));
            }
            else if(field.Equals("Status"))
            {
                int index = (b.NamingContainer as ListViewDataItem).DataItemIndex;
                string val = ((TextBox)CharacterStats.Items[index].FindControl("NewStatus")).Text;
                target.ChangeStatus(val);
            }
            else if(field.Equals("BaseStrength"))
            {
                target.BaseStrength += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("StrengthModifier"))
            {
                target.StrengthModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("BaseDexterity"))
            {
                target.BaseDexterity += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("DexterityModifier"))
            {
                target.DexterityModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("BaseConstitution"))
            {
                target.BaseConstitution += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("ConstitutionModifier"))
            {
                target.ConstitutionModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("BaseIntelligence"))
            {
                target.BaseIntelligence += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("IntelligenceModifier"))
            {
                target.IntelligenceModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("BaseWisdom"))
            {
                target.BaseWisdom += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("WisdomhModifier"))
            {
                target.WisdomModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("BaseCharisma"))
            {
                target.BaseCharisma += Int32.Parse(b.Attributes["value"]);
            }
            else if (field.Equals("CharismaModifier"))
            {
                target.CharismaModifier += Int32.Parse(b.Attributes["value"]);
            }
            else if(field.Equals("GoodVal"))
            {
                target.GoodVals += Int32.Parse(b.Attributes["value"]);
                target.checkAlignment();
            }
            else if(field.Equals("EvilVal"))
            {
                target.EvilVals += Int32.Parse(b.Attributes["value"]);
                target.checkAlignment();
            }
            else if(field.Equals("ChaoticVal"))
            {
                target.ChaoticVals += Int32.Parse(b.Attributes["value"]);
                target.checkAlignment();
            }
            else if(field.Equals("LawfulVal"))
            {
                target.LawfulVals += Int32.Parse(b.Attributes["value"]);
                target.checkAlignment();
            }

            CharacterStats.DataSource = (ViewState["characterMap"] as Dictionary<int, Character>).Values.ToList<Character>();
            CharacterStats.DataBind();
        }

        public void SaveProgress(object sender, EventArgs e)
        {
            using (var context = new PlayerContext())
            {
                List<Character> characters = (ViewState["characterMap"] as Dictionary<int, Character>).Values.ToList<Character>();
                
                for(int i = 0; i < characters.Count; i++)
                {
                    context.Characters.Attach(characters[i]);
                    context.Entry(characters[i]).State = EntityState.Modified;
                }
               
                context.SaveChanges();

                Server.Transfer(master.getURL("../Account/UserPage.aspx", 0, Context.User.Identity.Name));
            }
        }

        public Campaign getCurrentCampaign()
        {
            return ViewState["session"] as Campaign;
        }
    }
}