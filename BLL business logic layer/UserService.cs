using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class UserService
    {

        private DA_DataAccesLayer.DA_User userDAL = new DA_DataAccesLayer.DA_User();
        private string Hasher(string password)
        {

            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // 2. Crear la instancia de SHA256 y calcular el hash
            byte[] hashBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(bytes);
            }

            return BitConverter.ToString(hashBytes);
        }

        public void Register(BE_User user)
        {
            BE_User NewUser = userDAL.SearchByName(user.Username);
            if(NewUser == null)
            {
                string hashedpass = Hasher(user.Password);

                userDAL.Register(user.Username, hashedpass);
            }
            else { throw new Exception("el usuario que intenta regsitrar ya existe"); }
        }

        public BE_User Login(BE_User user)
        {
            BE_User DbUser = userDAL.SearchByName(user.Username);
            if (DbUser != null)
            {
                string hashedpass = Hasher(user.Password);

                if (hashedpass == DbUser.Password)
                {
                    userDAL.IncrementLoginCount(DbUser);
                    return DbUser;
                }
                else 
                {
                    throw new Exception("La contrase;a no coincide");
                }
            }
            throw new Exception("No se ha encontrado a ningun usuario con ese nombre, registrese");
        }

        public List<BE_User> ListPlayers()
        {
            List<BE_User> Players = userDAL.List();

            return Players;

        }

        public string SearchUsernameById(int UserID)
        {
            string Player = userDAL.SearchUsernameById(UserID);

            return Player;
        }
    }
}