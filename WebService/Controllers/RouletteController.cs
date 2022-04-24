using API.Classes;
using API.SQLliteService;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;


namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : Controller
    {

        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            SQLiteDataBase.Init();
            _logger = logger;
        }



        [HttpPost, ActionName("PlaceBet")]
        [Route("PlaceBet")]
        public string PlaceBet(int id)
        {
            try
            {
                var results = SQLiteDataBase.Place_Bet(id);

                if (results.IsSuccessfful)
                {
                    return results.ToString();
                }
                else
                {

                    return results.ToString();

                }

                return results.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }

        [HttpPost, ActionName("Spin")]
        [Route("Spin")]
        public string Spin()
        {
            try
            {
                int id = 1;

                var results = SQLiteDataBase.Spins(id);

                if (results.IsSuccessfful)
                {
                    return results.ToString();
                }
                else
                {

                    return results.ToString();

                }

                return results.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }


        //[HttpPost, ActionName("Payout")]
        //[Route("Payout")]
        //public void Payout()
        //{
        //    try
        //    {
        //        var results = SQLiteDataBase.Place_Bet(id);

        //        if (results.IsSuccessfful)
        //        {
        //            return results.ToString();
        //        }
        //        else
        //        {

        //            return results.ToString();

        //        }

        //        return results.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message.ToString();
        //    }
        //}

        [HttpPost, ActionName("ShowPreviousSpins")]

        [Route("ShowPreviousSpins")]
        public string ShowPreviousSpins()
        {
            try
            {
                var results = SQLiteDataBase.Previous_Spins();

                if (results.IsSuccessfful)
                {
                    return results.ToString();
                }
                else
                {

                    return results.ToString();

                }

                return results.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
