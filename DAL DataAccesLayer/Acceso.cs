using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Ajedrez_DAS.DAL
{
    public class Acceso
    {
        private SqlConnection conexion;
        public void Abrir()
        {
            conexion = new SqlConnection("Data Source: /SQLSERVERFACU; initial catalog: Chess#; integrated security: SSPI");
            
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        private SqlCommand Crearcomando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parametros != null) 
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }

            return cmd;
        }
        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter p = new SqlParameter(nombre, valor);
            p.DbType = DbType.String;
            return p;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter p = new SqlParameter(nombre, valor);
            p.DbType = DbType.Int32;
            return p;
        }

        public int Escribir(string sql, List<SqlParameter> parametros = null)
        {
            int filas;
            using (SqlCommand cmd = Crearcomando(sql, parametros))
            {
                filas = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.Dispose();
            }

            return filas;
        }

        public DataTable Leer(string sql, List<SqlParameter> parametros = null)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = Crearcomando(sql, parametros);
                da.Fill(dt);
                da.Dispose();
            }

            return dt;
        }
    }
}
