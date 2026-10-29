using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL_business_logic_layer
{
    public class GameService
    {
        DA_DataAccesLayer.DA_Game DA_Game = new DA_DataAccesLayer.DA_Game();

        public List<BE_GameRecord> ListWinnedGamesByUserId(int UserId)
        {
            List<BE_GameRecord> Games = DA_Game.ListWinnedGamesByUserId(UserId);

            return Games;
        }

        public List<BE_GameRecord> ListLostGamesByUserId(int UserId)
        {
            List<BE_GameRecord> Games = DA_Game.ListLostGamesByUserId(UserId);

            return Games;
        }

        public List<BE_GameRecord> ListAllGamesByUserId(int UserId)
        {
            List<BE_GameRecord> Games = DA_Game.ListAllGamesByUserId(UserId);

            return Games;
        }

        public List<BE_GameRecord> ListUnEndedGames()
        {
            List<BE_GameRecord> Games = DA_Game.ListUnendedGames();

            return Games;
        }

        public List<BE_GameRecord> ListAllGames()
        {
            List<BE_GameRecord> Games = DA_Game.ListALlGames();

            return Games;
        }
    }
}