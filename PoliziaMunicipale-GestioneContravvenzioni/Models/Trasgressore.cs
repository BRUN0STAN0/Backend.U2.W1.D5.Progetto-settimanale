using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale_GestioneContravvenzioni.Models
{
    public class Trasgressore
    {
        public int ID_Anagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        [Display(Name = "Cod Fiscale")]
        public string CodiceFiscale { get; set; }
        [Display(Name = "Violazioni")]
        public int TotaleViolazioni { get; set; }
        [Display(Name = "Punti decurtati")]
        public int TotaleDecurtamentoPunti { get; set; }

        public static List<Trasgressore> ListaTrasgressori = new List<Trasgressore>();
    }
}