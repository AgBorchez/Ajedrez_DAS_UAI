using Ajedrez_DAS.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace DA_DataAccesLayer
{
    public class DA_User
    {
        private Access access = new Access();
        public void Register(string username, string password)
        {
            int id = Convert.ToInt32(access.ReadScalar("SP_MAXID_User"));

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@Id", id),
                access.createParameter("@name", username),
                access.createParameter("@Pass", password)
            };
            
            access.Write("SP_User_Register", parameters); // no olvidarme de agregarlo

        }

        public List<BE_BusinessEntities.BE_User> List()
        {
            List<BE_BusinessEntities.BE_User> list = new List<BE_BusinessEntities.BE_User>();


            DataTable dt = access.Read("SP_List_Users"); //agregar 

            foreach (DataRow row in dt.Rows)
            {
                list.Add(Mapper(row));
            }

            return list;
        }

        public BE_BusinessEntities.BE_User SearchByName(string username) 
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@Username", username)
            };
                        
            DataTable dt = access.Read("SP_Search_By_Username", parameters); //agregar

            if (dt.Rows.Count > 0)
            {
                return Mapper(dt.Rows[0]);
            }
           
            return null;
        }

        public string SearchUsernameById(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@UserID", id)
            };

            string Username = access.ReadScalar("SP_Search_By_Id", parameters); 

            return Username;
        }



        private BE_BusinessEntities.BE_User Mapper(DataRow row)
        {
            int UserId = Convert.ToInt32(row["IdUser"]);
            string Username = row["Username"].ToString().Trim();
            string Password = row["Password"].ToString().Trim();
            return new BE_BusinessEntities.BE_User(UserId, Username, Password);
        }

        public void IncrementLoginCount(BE_BusinessEntities.BE_User user)
        {
            int EventId = Convert.ToInt32(access.ReadScalar("SP_MAXID_Event"));
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                access.createParameter("@Id", EventId),
                access.createParameter("@UserId", user.Id),
                access.createParameter("@Date", DateTime.Now)
            };
            
            access.Write("SP_Register_Login", parameters);
            
        }

    }
}