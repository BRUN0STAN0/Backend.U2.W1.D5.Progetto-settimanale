using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale_GestioneContravvenzioni.Models
{
    public class Verbale
    {
        [Display(Name = "Nr")]
        public int ID_Verbale { get; set; }
        [Display(Name = "Agente")]
        public string NominativoAgente { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Data violazione")]
        public DateTime DataViolazione { get; set; }
        [Display(Name = "Indirizzo")]
        public string IndirizzoViolazione { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Data trascrizione")]
        public DateTime DataTrascrizioneVerbale { get; set; }
        [DataType(DataType.Currency)]
        public decimal Importo { get; set; }
        [Display(Name = "Decurtamento punti")]
        public int DecurtamentoPunti { get; set; }
        public int ID_Violazione { get; set; }
        [Display(Name = "TipoViolazione")]
        public string TipoViolazione { get; set; }
        public int ID_Anagrafica { get; set; }
        [Display(Name = "Trasgressore")]
        public string NomeCognomeTrasgressore { get; set; }

        public static List<Verbale> ListaVerbali = new List<Verbale>();
    }
}