using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;

namespace DndHelper.SearchAndForms
{
    public partial class JoinCampaign : System.Web.UI.Page
    {
        List<Character> characters;
        SiteMaster master;
        int cid;
        int pid;
        public string cname;
        string pname;

        Dictionary<string, Character> nameToChar;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = Page.Master as SiteMaster;
            string player = Request.QueryString["user"];
          
            Join.Visible = true;

            if(!Page.IsPostBack)
            {
                IQueryable<Character> chars = master.GetCharacters(player);
                CharacterList.Items.Clear();

                if (chars != null && chars.Count() > 0)
                {
                    characters = chars.ToList();
                    for (int i = 0; i < characters.Count(); i++)
                    {
                        CharacterList.Items.Add(characters[i].CharacterName);
                    }
                }

                CharacterList.Items.Insert(0, "--Select a character--");
            }

            Message.Visible = false;
           
            cid = Int32.Parse(Request.QueryString["id"]);
            pid = Int32.Parse(Request.QueryString["pid"]);
            cname = Request.QueryString["cname"];
            pname = Request.QueryString["user"];
        }

        public void joinGame(object sender, System.EventArgs e)
        {

            string charaName = CharacterList.SelectedValue;

            if (!charaName.Equals("--Select a character--"))
            {
                using (var context = new PlayerContext())
                {
                    CampaignsToPlayers join = new CampaignsToPlayers();
                    join.CampaignId = cid;
                    join.CampaignName = cname;
                    join.PlayerId = pid;
                    join.PlayerName = pname;
                    join.CharacterID = master.GetCharacters(pname).Where(x => x.CharacterName.Equals(charaName)).First<Character>().CharacterID;
                    context.CampaignsToPlayers.Add(join);

                    context.SaveChanges();
                }

                Server.Transfer(master.getURL("../Account/UserPage.aspx", pid, pname));
            }
            else
            {
                Message.Text = "No character has been selected.";
                Message.Visible = true;
            }
            
        }
    }
}