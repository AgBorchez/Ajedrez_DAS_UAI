using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL_BusinessLogic
{
    public struct BE_MoveResult
    {        
        public BE_Position Position { get; set; }
        public BE_MovType MovType { get; set; }

        public BE_MoveResult(BE_Position pos, BE_MovType movType)
        {
            Position = pos;
            MovType = movType;
        }        
    }
}