using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService_roulette.Database;
using WebService_roulette.Models;

namespace WebService_roulette.Controllers
{
    public class RouletteController : Controller
    {
        // GET: Roulette
        public ActionResult Index()
        {
            return View();
        }

       
       
        [HttpPost]
        public ActionResult PlaceBet(Bet bet)
        {
            try
            {

                DB_Service.PlaceBet(bet.bet);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

                return View();
            }
        }

       
    }
}
