using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale_GestioneContravvenzioni.Models
{
    public class Shared
    {
        public static SqlConnection getConToDB()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connessioneDB"].ToString();
            return con;
        }

        public static SqlDataReader getReader(string tsql, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = tsql;
            return cmd.ExecuteReader();
        }
    }
}