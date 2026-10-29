using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE_BusinessEntities
{
    public class BE_User
    {
		public BE_User() { }

        public BE_User(string name, string password)
        {
            this.Username = name;
            this.Password = password;
        }

        public BE_User(int id, string name, string password) 
		{
			this.Id = id;
			this.Username = name;
			this.Password = password;
		}
		public int Id { get; set; }

		public string Username { get; set; }

        public string Password { get; set; }

		private BE_GameColor playerColor;

		public BE_GameColor PlayerColor { get { return playerColor; } }

		public void SetColor(BE_GameColor color)
		{
			if (playerColor == BE_GameColor.None) 
			{
				playerColor = color;
			}
		}


    }
}