using PoliziaMunicipale_GestioneContravvenzioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale_GestioneContravvenzioni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // PARTIAL PAGES
        public ActionResult _ContravvenzioniPerTrasgressore()
        {
            
            SqlConnection con = Shared.getConToDB();
            try {
                Contravvenzione.ListaContravvenzioni.Clear();
                con.Open();
                string tsql = "SELECT Cognome, Nome, Sum(Importo) AS TotaleImporto, count(*) AS NumeroVerbali "+
                              "FROM Anagrafica AS A join Verbale AS V  ON A.ID_Anagrafica = V.ID_Anagrafica "+
                              "GROUP BY Cognome, Nome ORDER BY NumeroVerbali DESC";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_TotaleImporto = Convert.ToDecimal(reader["TotaleImporto"]);
                        c.V_NumeroVerbali = Convert.ToInt32(reader["NumeroVerbali"]);

                        Contravvenzione.ListaContravvenzioni.Add(c);
                    }
                }
                con.Close();
            } catch (Exception ex)
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniPerTrasgressore", Contravvenzione.ListaContravvenzioni);
        }

        public ActionResult _ContravvenzioniSuperano10Punti()
        {

            SqlConnection con = Shared.getConToDB();
            try
            {
                Contravvenzione.ListaContravvenzioni.Clear();
                con.Open();
                string tsql = "SELECT Cognome, Nome, Importo, DataViolazione, DecurtamentoPunti " +
                    "FROM Anagrafica AS A join Verbale AS V  ON A.ID_Anagrafica = V.ID_Anagrafica  WHERE DecurtamentoPunti > 5 ";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_Importo = Convert.ToDecimal(reader["Importo"]);
                        c.V_DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);

                        Contravvenzione.ListaContravvenzioni.Add(c);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniSuperano10Punti", Contravvenzione.ListaContravvenzioni);
        }

        public ActionResult _ContravvenzioniImportoMaggiore400()
        {

            SqlConnection con = Shared.getConToDB();
            try
            {
                Contravvenzione.ListaContravvenzioni.Clear();
                con.Open();
                string tsql = "SELECT Cognome, Nome, DataViolazione, DecurtamentoPunti, Importo FROM Anagrafica AS A join Verbale AS V  ON A.ID_Anagrafica = V.ID_Anagrafica  WHERE Importo > 400";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_Importo = Convert.ToDecimal(reader["Importo"]);
                        c.V_DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);

                        Contravvenzione.ListaContravvenzioni.Add(c);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniImportoMaggiore400", Contravvenzione.ListaContravvenzioni);
        }

        public ActionResult _PuntiDecurtatiPerOgniTrasgressore()
        {

            SqlConnection con = Shared.getConToDB();
            try
            {
                Contravvenzione.ListaContravvenzioni.Clear();
                con.Open();
                string tsql = "SELECT Cognome, Nome, Sum(DecurtamentoPunti) AS DecurtamentoPunti" +
                    " FROM Anagrafica AS A  join Verbale AS V  ON A.ID_Anagrafica = V.ID_Anagrafica GROUP BY Cognome, Nome";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);

                        Contravvenzione.ListaContravvenzioni.Add(c);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return PartialView("_PuntiDecurtatiPerOgniTrasgressore", Contravvenzione.ListaContravvenzioni);
        }
    }
}