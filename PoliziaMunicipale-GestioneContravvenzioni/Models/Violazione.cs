using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale_GestioneContravvenzioni.Models
{
    public class Violazione
    {
        public int ID_Violazione { get; set; }
        public string Descrizione { get; set; }

        public static List<Violazione> ListaViolazioni = new List<Violazione>();

    }
}