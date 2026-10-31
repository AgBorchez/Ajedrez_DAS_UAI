using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL_BusinessLogic
{
    public struct BE_GameResume
    {
        public BE_GameRecord gr { get; set; }
        public string WhiteP { get; set; }
        public string BlackP { get; set; }

    }
}