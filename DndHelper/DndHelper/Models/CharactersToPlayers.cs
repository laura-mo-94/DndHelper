using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DndHelper.Models
{
    public class CharactersToPlayers
    {
        [Key, ScaffoldColumn(false)]
        public int RelationId { get; set; }

        [Required, Display(Name = "PlayerID")]
        public int PlayerId { get; set; }

        [Required, StringLength(100), Display(Name = "PlayerName")]
        public string PlayerName { get; set; }

        [Required, Display(Name = "CharacterID")]
        public int CharacterId { get; set; }

        [Required, StringLength(100), Display(Name = "CharacterName")]
        public string CharacterName { get; set; }
    }
}