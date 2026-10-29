using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL_DataAccesLayer
{
    public interface IDA_Game
    {
        BE_GameRecord Newgame(List<BE_User> players);
        void GameOverDraw(BE_GameRecord record);
        void GameOverCheck(BE_GameRecord record, int winnerId);
        void SaveGameState(BE_GameRecord gamerecord);
    }
}