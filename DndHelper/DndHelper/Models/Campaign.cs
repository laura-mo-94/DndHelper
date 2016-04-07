using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DndHelper.Models
{
    [Serializable]
    public class Campaign
    {
        [ScaffoldColumn(false)]
        public int CampaignId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string CampaignName { get; set; }

        [Required, Display(Name = "Dungeon Master")]
        public int DungeonMasterID { get; set; }

        [Display(Name ="Description")]
        public string Description { get; set; }
    }
}