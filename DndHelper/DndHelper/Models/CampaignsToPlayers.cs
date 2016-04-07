using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DndHelper.Models
{
    public class CampaignsToPlayers
    {
        [Key, ScaffoldColumn(false)]
        public int RelationId { get; set; }

        [Required, Display(Name = "PlayerID")]
        public int PlayerId { get; set; }

        [Required, StringLength(100), Display(Name = "PlayerName")]
        public string PlayerName { get; set; }

        [Display(Name ="CharacterID")]
        public int CharacterID { get; set; }

        [Required, Display(Name = "CampaignID")]
        public int CampaignId { get; set; }

        [Required, StringLength(100), Display(Name = "CampaignName")]
        public string CampaignName { get; set; }
    }
}