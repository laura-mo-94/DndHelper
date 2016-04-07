using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DndHelper.Models;
using System.Data.Entity;

namespace DndHelper.SearchAndForms
{
    public partial class CreateCharacter : System.Web.UI.Page
    {
        SiteMaster master;
        bool editing;
        Character currentCharacter;

        protected void Page_Load(object sender, EventArgs e)
        {
            Message.Visible = false;
            master = Page.Master as SiteMaster;

            if (Request.QueryString.Get("cid") != null)
            {
                Execute.Text = "Save";
                editing = true;
                currentCharacter = master.GetCharacter(Int32.Parse(Request.QueryString["cid"]));

                if (!Page.IsPostBack)
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
            CharacterName.Text = currentCharacter.CharacterName;
            CharacterClass.Text = currentCharacter.Class;
            CharacterHealth.Text = currentCharacter.MaxHealth.ToString();
            CharacterSpecie.Text = currentCharacter.Specie;
            CBaseAttack.Text = currentCharacter.BaseStrength.ToString();
            CAttackModifier.Text = currentCharacter.StrengthModifier.ToString();
            CBaseDexterity.Text = currentCharacter.BaseDexterity.ToString();
            CDexterityModifier.Text = currentCharacter.DexterityModifier.ToString();
            CBaseConstitution.Text = currentCharacter.BaseConstitution.ToString();
            CConstitutionModifier.Text = currentCharacter.ConstitutionModifier.ToString();
            CBaseIntelligence.Text = currentCharacter.BaseIntelligence.ToString();
            CIntelligenceModifier.Text = currentCharacter.IntelligenceModifier.ToString();
            CBaseWisdom.Text = currentCharacter.BaseWisdom.ToString();
            CWisdomModifier.Text = currentCharacter.WisdomModifier.ToString();
            CBaseCharisma.Text = currentCharacter.BaseCharisma.ToString();
            CCharismaModifier.Text = currentCharacter.CharismaModifier.ToString();
        }

        public void executeCharacter(object sender, System.EventArgs e)
        {
            if(!editing)
            {
                createCharacter();
            }
            else
            {
                saveCharacter();
            }
        }

        public void createCharacter()
        {
            Message.Visible = true;
         
            int health;

            if (String.IsNullOrWhiteSpace(CharacterName.Text))
            {
                Message.Text = "Please enter a name!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterClass.Text))
            {
                Message.Text = "Please enter a class!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterSpecie.Text))
            {
                Message.Text = "Please enter a specie!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterHealth.Text) || !Int32.TryParse(CharacterHealth.Text, out health))
            {
                Message.Text = "Please enter your health!";
            }
            else
            {
                using (var context = new PlayerContext())
                {
                    Player creator = master.GetPlayer(Context.User.Identity.Name);

                    Character chara = new Character();
                    chara.PlayerID = creator.PlayerId;
                    chara.CharacterName = CharacterName.Text;
                    chara.MaxHealth = health;
                    chara.CurrentHealth = health;
                    chara.Specie = CharacterSpecie.Text;
                    chara.Class = CharacterClass.Text;
                    chara.Alive = true;
                    chara.Level = 1;
                    chara.Status = "Normal";

                    chara.BaseStrength = Int32.Parse(CBaseAttack.Text);
                    chara.StrengthModifier = Int32.Parse(CAttackModifier.Text);
                    chara.BaseIntelligence = Int32.Parse(CBaseIntelligence.Text);
                    chara.IntelligenceModifier = Int32.Parse(CIntelligenceModifier.Text);
                    chara.BaseDexterity = Int32.Parse(CBaseDexterity.Text);
                    chara.DexterityModifier = Int32.Parse(CDexterityModifier.Text);
                    chara.BaseWisdom = Int32.Parse(CBaseWisdom.Text);
                    chara.WisdomModifier = Int32.Parse(CWisdomModifier.Text);
                    chara.BaseCharisma = Int32.Parse(CBaseCharisma.Text);
                    chara.CharismaModifier = Int32.Parse(CCharismaModifier.Text);
                    chara.BaseConstitution = Int32.Parse(CBaseConstitution.Text);
                    chara.ConstitutionModifier = Int32.Parse(CConstitutionModifier.Text);

                    context.Characters.Add(chara);
                    context.SaveChanges();

                    CharactersToPlayers rel = new CharactersToPlayers();
                    rel.CharacterId = chara.CharacterID;
                    rel.CharacterName = chara.CharacterName;
                    rel.PlayerId = creator.PlayerId;
                    rel.PlayerName = creator.PlayerName;

                    context.CharactersToPlayers.Add(rel);
                    context.SaveChanges();
                    Server.Transfer(master.getURL("../Account/UserPage.aspx", creator.PlayerId, creator.PlayerName));
                }

            }
        }

        public void saveCharacter()
        {
            int health;

            if (String.IsNullOrWhiteSpace(CharacterName.Text))
            {
                Message.Text = "Please enter a name!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterClass.Text))
            {
                Message.Text = "Please enter a class!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterSpecie.Text))
            {
                Message.Text = "Please enter a specie!";
            }
            else if (String.IsNullOrWhiteSpace(CharacterHealth.Text) || !Int32.TryParse(CharacterHealth.Text, out health))
            {
                Message.Text = "Please enter your health!";
            }
            else
            {
                using (var context = new PlayerContext())
                {
                    currentCharacter.CharacterName = CharacterName.Text;
                    currentCharacter.Class = CharacterClass.Text;
                    currentCharacter.Specie = CharacterSpecie.Text;
                    currentCharacter.MaxHealth = health;
                    context.Characters.Attach(currentCharacter);

                    currentCharacter.BaseStrength = Int32.Parse(CBaseAttack.Text);
                    currentCharacter.StrengthModifier = Int32.Parse(CAttackModifier.Text);
                    currentCharacter.BaseIntelligence = Int32.Parse(CBaseIntelligence.Text);
                    currentCharacter.IntelligenceModifier = Int32.Parse(CIntelligenceModifier.Text);
                    currentCharacter.BaseDexterity = Int32.Parse(CBaseDexterity.Text);
                    currentCharacter.DexterityModifier = Int32.Parse(CDexterityModifier.Text);
                    currentCharacter.BaseWisdom = Int32.Parse(CBaseWisdom.Text);
                    currentCharacter.WisdomModifier = Int32.Parse(CWisdomModifier.Text);
                    currentCharacter.BaseCharisma = Int32.Parse(CBaseCharisma.Text);
                    currentCharacter.CharismaModifier = Int32.Parse(CCharismaModifier.Text);
                    currentCharacter.BaseConstitution = Int32.Parse(CBaseConstitution.Text);
                    currentCharacter.ConstitutionModifier = Int32.Parse(CConstitutionModifier.Text);

                    context.Entry(currentCharacter).State = EntityState.Modified;
                    context.SaveChanges();

                    Server.Transfer(master.getURL("../Account/UserPage.aspx", 0, Context.User.Identity.Name));
                }
            }
        }

    }
}