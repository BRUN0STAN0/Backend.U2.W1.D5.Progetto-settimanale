using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale_GestioneContravvenzioni.Controllers
{
    public class ViolazioneController : Controller
    {
        // GET: Violazione
        public ActionResult Index()
        {
            return View();
        }
    }
}