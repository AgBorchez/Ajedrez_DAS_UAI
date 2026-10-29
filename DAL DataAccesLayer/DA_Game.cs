using Ajedrez_DAS.DAL;
using BE_BusinessEntities;
using DAL_DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DA_DataAccesLayer
{
    public class DA_Game : IDA_Game
    {
        private Access access = new Access();

        private BE_GameRecord Maper(DataRow row)
        {
            int durationSeconds = (row["GameDuration"] != DBNull.Value) ? Convert.ToInt32(row["GameDuration"]) : 0;
            TimeSpan duration = TimeSpan.FromSeconds(durationSeconds);
            int GameId = Convert.ToInt32(row["IDGame"]); 
            int IdWhite = Convert.ToInt32(row["IDPlayerWhites"]); 
            int IdBlack = Convert.ToInt32(row["IDPlayerBlacks"]); 

            bool Draw = row["Draw"] != DBNull.Value && Convert.ToBoolean(row["Draw"]); 

            DateTime? Ended = (row["EndDate"] != DBNull.Value) ? Convert.ToDateTime(row["EndDate"]) : (DateTime?)null; 

            DateTime InitDate = Convert.ToDateTime(row["GameDate"]);

            return new BE_GameRecord(GameId, InitDate, IdWhite, IdBlack, Draw, Ended, duration, new Stack<BE_MovementRecord>());
        }
        public BE_GameRecord Newgame(List<BE_User> Players)
        {
            BE_User WhitePlayer = Players.FirstOrDefault(p => p.PlayerColor == BE_GameColor.White);
            BE_User BlackPlayer = Players.FirstOrDefault(p => p.PlayerColor == BE_GameColor.Black);
            DateTime DateNow = DateTime.Now;

            int LastEventId = Convert.ToInt32(access.ReadScalar("SP_MAXID_Event"));
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@Id1", LastEventId),
                access.createParameter("@Id2", LastEventId+1),
                access.createParameter("@UserIdWhite", WhitePlayer.Id),
                access.createParameter("@UserIdBlack", BlackPlayer.Id),
                access.createParameter("@Date", DateNow)
            };

            access.Write("SP_Register_Event_NewGame", parameters);

            int NewGameId = Convert.ToInt32(access.ReadScalar("SP_MAXID_Game"));

            parameters = new List<SqlParameter>
            {
                access.createParameter("@IdNewGame", NewGameId),
                access.createParameter("@UserIdWhite", WhitePlayer.Id),
                access.createParameter("@UserIdBlack", BlackPlayer.Id),
                access.createParameter("@Date", DateNow)
            };

            access.Write("SP_NewGame", parameters);

            return new BE_GameRecord(NewGameId, DateNow, WhitePlayer.Id, BlackPlayer.Id, new Stack<BE_MovementRecord>());
        }

        public void GameOverCheck(BE_GameRecord gamerecord, int idWinner)
        {
            DateTime date = DateTime.Now;

            List<SqlParameter> parameters = new List<SqlParameter>{
                access.createParameter("@IdGame", gamerecord.IdGame),
                access.createParameter("@EndDate", date),
                access.createParameter("@IdWinner", idWinner),
                access.createParameter("@IdLoser", (gamerecord.IdPlayerWhite == idWinner) ? gamerecord.IdPlayerBlack : gamerecord.IdPlayerWhite),
                access.createParameter("@Duration", (int)gamerecord.Duration.TotalSeconds)
            };

            access.Write("SP_EndGameCheck", parameters);

            int idEvent = Convert.ToInt32(access.ReadScalar("SP_MAXID_Event"));
            
            parameters = new List<SqlParameter>
            {
                access.createParameter("@idEvent", idEvent),
                access.createParameter("@UserId1", gamerecord.IdPlayerWhite),
                access.createParameter("@UserId2", gamerecord.IdPlayerBlack),
                access.createParameter("@Date", date)
            };

            access.Write("SP_Event_GameEnded", parameters);
        }

        public void GameOverDraw(BE_GameRecord gamerecord)
        {
            DateTime date = DateTime.Now;

            List<SqlParameter> parameters = new List<SqlParameter>{
                access.createParameter("@IdGame", gamerecord.IdGame),
                access.createParameter("@EndDate", date),
                access.createParameter("@Duration", (int)gamerecord.Duration.TotalSeconds)
            };

            access.Write("SP_EndGameDraw", parameters);

            int idEvent = Convert.ToInt32(access.ReadScalar("SP_MAXID_Event"));

            parameters = new List<SqlParameter>
            {
                access.createParameter("@idEvent", idEvent),
                access.createParameter("@UserId1", gamerecord.IdPlayerWhite),
                access.createParameter("@UserId2", gamerecord.IdPlayerBlack),
                access.createParameter("@Date", date)
            };

            access.Write("SP_Event_GameEnded", parameters);
        }

        public List<BE_GameRecord> ListWinnedGamesByUserId(int UserId)
        {
            List<BE_GameRecord> WinnedGames = new List<BE_GameRecord>();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@IdPlayer", UserId)
            };

            DataTable dt = access.Read("SP_ListWinnedGames_ByUserId", parameters);

            foreach (DataRow dr in dt.Rows) 
            {
                WinnedGames.Add(Maper(dr));
            }

            return WinnedGames;
        }

        public List<BE_GameRecord> ListLostGamesByUserId(int UserId) 
        {
            List<BE_GameRecord> LostGames = new List<BE_GameRecord>();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@IdPlayer", UserId)
            };

            DataTable dt = access.Read("SP_ListLostGames_ByUserId", parameters);

            foreach (DataRow dr in dt.Rows)
            {
                LostGames.Add(Maper(dr));
            }

            return LostGames;
        }

        public List<BE_GameRecord> ListAllGamesByUserId(int UserId)
        {
            List<BE_GameRecord> games = new List<BE_GameRecord>();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@IdPlayer", UserId)
            };

            DataTable dt = access.Read("SP_ListAllGames_ByUserId", parameters);

            foreach (DataRow dr in dt.Rows)
            {
                games.Add(Maper(dr));
            }

            return games;
        }

        public List<BE_GameRecord> ListUnendedGames()
        {
            List<BE_GameRecord> unEndedGames = new List<BE_GameRecord>();

            DataTable dt = access.Read("SP_ListUnEndedGames");

            foreach(DataRow dr in dt.Rows)
            {
                unEndedGames.Add(Maper(dr));
            }

            return unEndedGames;
        }

        public List<BE_GameRecord> ListALlGames()
        {
            List<BE_GameRecord> Games = new List<BE_GameRecord>();

            DataTable dt = access.Read("SP_ListAllGames");

            foreach (DataRow dr in dt.Rows)
            {
                Games.Add(Maper(dr));
            }

            return Games;
        }

        public void SaveGameState(BE_GameRecord gamerecord)
        {
            List<SqlParameter> parameters = new List<SqlParameter>{
                access.createParameter("@IdGame", gamerecord.IdGame),
                access.createParameter("@Duration", (int)gamerecord.Duration.TotalSeconds)
            };

            access.Write("SP_SaveGameState", parameters);
        }

    }
}