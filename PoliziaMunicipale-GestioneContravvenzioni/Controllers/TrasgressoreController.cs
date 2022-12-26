using PoliziaMunicipale_GestioneContravvenzioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale_GestioneContravvenzioni.Controllers
{
    public class TrasgressoreController : Controller
    {
        // GET: Trasgressore
        public ActionResult Lista()
        {
            SqlConnection con = Shared.getConToDB();
            try
            {
                Trasgressore.ListaTrasgressori.Clear();
                con.Open();
                string tsql = "SELECT A.ID_Anagrafica, Cognome, Nome, Indirizzo, Citta, Cap,CodiceFiscale, Count(ID_Violazione) as TotaleViolazioni, " +
                                "Sum(DecurtamentoPunti) as TotaleDecurtamentoPunti" +
                                " From Anagrafica as A full join Verbale as V ON A.ID_Anagrafica = V.ID_Anagrafica" +
                                " Group by A.ID_Anagrafica, Cognome, Nome, Indirizzo, Citta, Cap,CodiceFiscale";
                SqlDataReader reader = Shared.getReader(tsql, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trasgressore t = new Trasgressore();
                        t.ID_Anagrafica = Convert.ToInt32(reader["ID_Anagrafica"]);
                        t.Cognome = reader["Cognome"].ToString();
                        t.Nome = reader["Nome"].ToString();
                        t.Indirizzo = reader["Indirizzo"].ToString();
                        t.Citta = reader["Citta"].ToString();
                        t.CAP = reader["Cap"].ToString();
                        t.CodiceFiscale = reader["CodiceFiscale"].ToString();
                        t.TotaleViolazioni = Convert.ToInt32(reader["TotaleViolazioni"]);
                        t.TotaleDecurtamentoPunti = Convert.ToInt32(reader["TotaleDecurtamentoPunti"]);

                        Trasgressore.ListaTrasgressori.Add(t);

                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return View(Trasgressore.ListaTrasgressori);
        }
    }
}