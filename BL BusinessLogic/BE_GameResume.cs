using BE_BusinessEntities;

namespace BL_BusinessLogic
{
    public struct BE_GameResume
    {
        public BE_GameRecord gr { get; set; }
        public string WhiteP { get; set; }
        public string BlackP { get; set; }

    }
}