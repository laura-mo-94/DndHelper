using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DndHelper.Models
{
    [Serializable]
    public class Character
    {
        [ScaffoldColumn(false)]
        public int CharacterID { get; set; }

        [Required, Display(Name ="OwnerID")]
        public int PlayerID { get; set; }

        [Required, StringLength(100), Display(Name = "Character Name")]
        public string CharacterName { get; set; }

        [Required, Display(Name = "Specie")]
        public string Specie { get; set; }

        [Required, Display(Name = "Class")]
        public string Class { get; set; }

        [Required, Display(Name = "Level")]
        public int Level { get; set; }

        [Required, Display(Name = "Current Health")]
        public int CurrentHealth { get; set; }

        [Required, Display(Name = "Max Health")]
        public int MaxHealth { get; set; }

        [Required, Display(Name = "Status")]
        public string Status { get; set; }

        [Required, Display(Name = "Alive")]
        public bool Alive { get; set; }

        [Display(Name = "BaseStrength")]
        public int BaseStrength { get; set; }

        [Display(Name ="BaseDexterity")]
        public int BaseDexterity { get; set; }

        [Display(Name = "BaseConstitution")]
        public int BaseConstitution { get; set; }

        [Display(Name = "BaseIntelligence")]
        public int BaseIntelligence { get; set; }

        [Display(Name ="BaseWisdom")]
        public int BaseWisdom { get; set; }

        [Display(Name = "BaseCharisma")]
        public int BaseCharisma { get; set; }

        [Display(Name = "StrengthModifier")]
        public int StrengthModifier { get; set; }

        [Display(Name = "DexterityModifier")]
        public int DexterityModifier { get; set; }

        [Display(Name = "ConstitutionModifier")]
        public int ConstitutionModifier { get; set; }

        [Display(Name = "IntelligenceModifier")]
        public int IntelligenceModifier { get; set; }

        [Display(Name = "WisdomModifier")]
        public int WisdomModifier { get; set; }

        [Display(Name = "CharismaModifier")]
        public int CharismaModifier { get; set; }

        public void ChangeHealth(int dif)
        {
            CurrentHealth += dif;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Status = "DEAD";
                Alive = false;
            }
            else if (!Alive)
            {
                Status = "Normal";
                Alive = true;
            }

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void AdjustLevel(int adjustment)
        {
            if(Level + adjustment > 0)
            {
                Level += adjustment;
            }
        }

        public void ChangeMaxHealth(int dif)
        {
            if(MaxHealth + dif > 0)
            {
                MaxHealth += dif;
                CurrentHealth = MaxHealth;
            }
        }

        public void ChangeStatus(string value)
        {
            Status = value;
        }
    }
}