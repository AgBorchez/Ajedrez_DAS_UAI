using BE_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE_BusinessEntities
{
    public class BE_GameRecord
    {
		public BE_GameRecord() { }
		public BE_GameRecord(int GameId, DateTime startDate, int white, int black) 
		{
			this.IdGame = GameId;
			this.IdPlayerWhite = white;
			this.IdPlayerBlack = black;
			this.StartDate = startDate;
		}

		public BE_GameRecord(int GameId, DateTime startDate, int white, int black, bool Draw, DateTime? Ended) 
			: this(GameId, startDate, white, black) 
		{
			this.Draw = Draw;
			this.Ended = Ended;
		}

		public int IdGame { get; set; }

        public int IdPlayerWhite {  get; set; }

        public int IdPlayerBlack { get; set; }

        public DateTime StartDate { get; set; }

        public bool Draw {  get; set; }
        public DateTime? Ended { get; set; }

		public TimeSpan Duration { get; set; }

    }
}