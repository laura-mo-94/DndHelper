using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.SqlClient;
using System;

//DropCreateDatabaseIfModelChanges
namespace DndHelper.Models
{
    public class DnD_DatabaseInitializer: DropCreateDatabaseIfModelChanges<PlayerContext>
    {
        protected override void Seed(PlayerContext context)
        {
            GetCampaigns().ForEach(c => context.Campaigns.Add(c));
            GetPlayers().ForEach(p => context.Players.Add(p));
            GetCharacters().ForEach(c => context.Characters.Add(c));
            GetCampToPlayerRelationships().ForEach(r => context.CampaignsToPlayers.Add(r));
            GetCharToPlayerRelationships().ForEach(r => context.CharactersToPlayers.Add(r));
        }

        public override void InitializeDatabase(PlayerContext context)
        {
            this.MurderAllConnections(context);
            try
            {
                base.InitializeDatabase(context);
      
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
           
        }

        private void MurderAllConnections(PlayerContext context)
        {
            try
            {
                // FIRST: Build a connection using the DB Context's current connection.
                SqlConnectionStringBuilder sqlConnBuilder = new SqlConnectionStringBuilder(context.Database.Connection.ConnectionString);
                Debug.WriteLine(context.Database.Connection.ConnectionString);
                // Set the catalog to master so that the DB can be dropped
                sqlConnBuilder.InitialCatalog = "master";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnBuilder.ConnectionString))
                {

                    sqlConnection.Open();
                    string dbName = context.Database.Connection.Database;

                    // Build up the SQL string necessary for dropping database connections. This statement is doing a couple of things:
                    // 1) Tests to see if the DB exists in the first place.
                    // 2) If it does, sets single user mode, which kills all connections.
                    string sql = @"IF EXISTS(SELECT NULL FROM sys.databases WHERE name = '" + dbName + "') BEGIN ALTER DATABASE [" + dbName + "] SET MULTI_USER WITH ROLLBACK IMMEDIATE END";
                    Debug.WriteLine(sql);
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConnection))
                    {
                        // Run and done.
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                // Something bad happened.
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }
        private static List<Campaign> GetCampaigns()
        {
            List<Campaign> campaigns = new List<Campaign>
            {
                new Campaign
                {
                    CampaignId = 0,
                    CampaignName = "TestCampaign",
                    DungeonMasterID = 1
                }
            };

            return campaigns;
        }

        private static List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>
            {
                new Player
                {
                    PlayerId = 1,
                    PlayerName = "TestDM"
                },
                
                new Player
                {
                    PlayerId = 2,
                    PlayerName = "TestParticipant"
                },
                new Player
                {
                    PlayerId = 3,
                    PlayerName = "TestParticipant2"
                }

            };

            return players;
        }

        private static List<Character> GetCharacters()
        {
            List<Character> characters = new List<Character>
            {
                new Character
                {
                   CharacterID = 1,
                   CharacterName = "Chara",
                   Class = "Little Punk",
                   Specie = "Human",
                   Alive = true,
                   Level = 1,
                   MaxHealth = 7,
                   CurrentHealth = 7,
                   Status = "Normal"
                },
                new Character
                {
                   CharacterID = 2,
                   CharacterName = "Ter",
                   Class = "Medium Punk",
                   Specie = "Human-Orc",
                   Alive = true,
                   Level = 1,
                   MaxHealth = 15,
                   CurrentHealth = 15,
                   Status = "Normal"
                },
                new Character
                {
                   CharacterID = 3,
                   CharacterName = "Holder",
                   Class = "Big Punk",
                   Specie = "Orc",
                   Alive = true,
                   Level = 1,
                   MaxHealth = 20,
                   CurrentHealth = 20,
                   Status = "Normal"
                }
            };

            return characters;
        }

        private static List<CharactersToPlayers> GetCharToPlayerRelationships()
        {
            List<CharactersToPlayers> charactersToPlayers = new List<CharactersToPlayers>
            {
                new CharactersToPlayers
                {
                    RelationId = 1,
                    CharacterId = 1,
                    CharacterName = "Chara",
                    PlayerId = 2,
                    PlayerName = "TestParticipant"
                },

                new CharactersToPlayers
                {
                    RelationId = 2,
                    CharacterId = 2,
                    CharacterName = "Ter",
                    PlayerId = 3,
                    PlayerName = "TestParticipant2"
                },

                new CharactersToPlayers
                {
                    RelationId = 3,
                    CharacterId = 3,
                    CharacterName = "Holder",
                    PlayerId = 3,
                    PlayerName = "TestParticipant2"
                }
            };

            return charactersToPlayers;
        }

        private static List<CampaignsToPlayers> GetCampToPlayerRelationships()
        {
            List<CampaignsToPlayers> campaignsToPlayers = new List<CampaignsToPlayers>
            {
                new CampaignsToPlayers
                {
                    RelationId = 1,
                    CampaignId = 1,
                    CampaignName = "TestCampaign",
                    PlayerId = 2,
                    PlayerName = "TestParticipant",
                    CharacterID = 1
                },

                new CampaignsToPlayers
                {
                    RelationId = 2,
                    CampaignId = 1,
                    CampaignName = "TestCampaign",
                    PlayerId = 3,
                    PlayerName = "TestParticipant2",
                    CharacterID = 3
                },

                new CampaignsToPlayers
                {
                    RelationId = 3,
                    CampaignId = 1,
                    CampaignName = "TestCampaign",
                    PlayerId = 1,
                    PlayerName = "TestDM"
                }
            };

            return campaignsToPlayers;
        }
    }
}