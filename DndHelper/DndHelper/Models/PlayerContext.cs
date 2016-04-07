using System.Data.Entity;
using System.Diagnostics;
using System.Collections.Generic;

namespace DndHelper.Models
{
    public class PlayerContext: DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CharactersToPlayers> CharactersToPlayers { get; set; }
        public DbSet<CampaignsToPlayers> CampaignsToPlayers { get; set; }

        public PlayerContext() : base("DndHelper")
        {
        }
        
    }
}