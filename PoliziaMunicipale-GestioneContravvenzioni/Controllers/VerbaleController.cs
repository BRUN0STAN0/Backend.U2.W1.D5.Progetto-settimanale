using PoliziaMunicipale_GestioneContravvenzioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PoliziaMunicipale_GestioneContravvenzioni.Controllers
{
    public class VerbaleController : Controller
    {
        // GET: Verbale
        public ActionResult Lista()
        {
            SqlConnection con = Shared.getConToDB();
            try
            {
                Verbale.ListaVerbali.Clear();
                con.Open();
                string tsql = "SELECT * FROM Verbale AS V JOIN TipoViolazione AS tV " +
                    "ON tV.ID_Violazione = V.ID_Violazione JOIN Anagrafica AS A ON A.ID_Anagrafica = V.ID_Anagrafica " +
                    "ORDER BY DataTrascrizioneVerbale DESC";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Verbale v = new Verbale();
                        v.ID_Verbale = Convert.ToInt32(reader["ID_Verbale"]);
                        v.NominativoAgente = reader["NominativoAgente"]+"";
                        v.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        v.IndirizzoViolazione = reader["IndirizzoViolazione"]+"";
                        v.DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                        v.Importo = Convert.ToDecimal(reader["importo"]);
                        v.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        v.ID_Violazione = Convert.ToInt32(reader["ID_Violazione"]);
                        v.ID_Anagrafica = Convert.ToInt32(reader["ID_Anagrafica"]);
                        v.TipoViolazione = reader["Descrizione"]+"";
                        v.NomeCognomeTrasgressore = reader["Nome"] + " " + reader["Cognome"];

                        Verbale.ListaVerbali.Add(v);
                        
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return View(Verbale.ListaVerbali);
        }
        public ActionResult Archivia()
        {
            SqlConnection con = Shared.getConToDB();
            List<SelectListItem> listaViolazioni = new List<SelectListItem>();
            try
            {
                con.Open();
                string tsql = "SELECT V.ID_Violazione, Descrizione FROM Verbale AS V JOIN TipoViolazione AS tV "+
                    " ON tV.ID_Violazione = V.ID_Violazione JOIN Anagrafica AS A ON A.ID_Anagrafica = V.ID_Anagrafica "+
                    " GROUP BY  V.ID_Violazione, Descrizione";
                SqlDataReader reader = Shared.getReader(tsql, con);

                
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                       SelectListItem item = new SelectListItem();
                        item.Text = reader["Descrizione"].ToString();
                        item.Value = reader["ID_Violazione"].ToString();
                        listaViolazioni.Add(item);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            ViewBag.TipoViolazioni = listaViolazioni;
            ViewBag.ListaTrasgressoriCercarti = "";


            return View();
        }

        public ActionResult Cerca(string Cerca)
        {
            SqlConnection con2 = Shared.getConToDB();
            List<SelectListItem> listaTrasgressoriCercati = new List<SelectListItem>();
            try
            {
                con2.Open();

                string tsql = $"SELECT * FROM Anagrafica WHERE Nome = '%{Cerca}%' OR Cognome = '%{Cerca}%'";
                SqlDataReader reader = Shared.getReader(tsql, con2);


                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = reader["Descrizione"].ToString();
                        item.Value = reader["ID_Violazione"].ToString();
                        listaTrasgressoriCercati.Add(item);
                    }
                }
                con2.Close();
            }
            catch (Exception ex)
            {
                con2.Close();
            }
            ViewBag.ListaTrasgressoriCercarti = listaTrasgressoriCercati;

            return View();

        }
    } 
}