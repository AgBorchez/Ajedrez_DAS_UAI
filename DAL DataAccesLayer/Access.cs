using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Ajedrez_DAS.DAL
{
    internal class Access
    {
        private SqlConnection connection;
        private void Open()
        {
            if(connection == null) { 
                connection = new SqlConnection(@"Data Source=.\SQLSERVERFACU;Initial Catalog=Chess#;Integrated Security=SSPI;");
            }
            if (connection.State == ConnectionState.Closed) { connection.Open(); }
        }

        private void Close()
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection = null;
            }
            GC.Collect();
        }

        private SqlCommand createCommand(string sql, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null) 
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            return cmd;
        }
        public SqlParameter createParameter(string name, string value)
        {
            SqlParameter p = new SqlParameter(name, value);
            p.DbType = DbType.String;
            return p;
        }

        public SqlParameter createParameter(string name, int value)
        {
            SqlParameter p = new SqlParameter(name, value);
            p.DbType = DbType.Int32;
            return p;
        }

        public SqlParameter createParameter(string name, DateTime value)
        {
            SqlParameter p = new SqlParameter(name, value);
            p.DbType = DbType.DateTime;
            return p;
        }

        public SqlParameter createParameter(string name, bool value)
        {
            SqlParameter p = new SqlParameter(name, value);
            p.DbType = DbType.Boolean;
            return p;
        }

        public int Write(string sql, List<SqlParameter> parameters = null)
        {
            Open();
            int rows;
            using (SqlCommand cmd = createCommand(sql, parameters))
            {
                rows = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
            }

            Close();
            return rows;
        }

        public DataTable Read(string sql, List<SqlParameter> parameters = null)
        {
            Open();
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = createCommand(sql, parameters);
                da.Fill(dt);
            }

            Close();
            return dt;
        }

        public string ReadScalar(string sql, List<SqlParameter> parameters = null)
        {
            Open();
            string outcome;
            using(SqlCommand cmd = createCommand(sql, parameters))
            {
                outcome = cmd.ExecuteScalar().ToString();
                cmd.Parameters.Clear();
            }

            Close();
            return outcome;
        }
    }
}
