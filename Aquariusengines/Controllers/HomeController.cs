using System;
using Aquariusengines.ViewModels;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Aquariusengines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISignal _signal;
        private readonly IConfiguration _configuration;
        public HomeController(ISignal signal, IConfiguration configuration)
        {
            _signal = signal;
            _configuration = configuration;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([FromBody] JObject collection)
        {
            try
            {
                //get configuration
                double amplitude = double.Parse(_configuration["amplitude"]);
                double max_range = double.Parse(_configuration["max_range"]);
                double min_range = double.Parse(_configuration["min_range"]); 
                //Check Sine Validation
                Sine _sine = new Sine();

                Models.Signal sine = new Models.Signal
                {
                    Type = "Sine",
                    TimeStamp = (DateTime)collection["Time"],
                    Time = ((DateTime)(collection["Time"])).TimeOfDay.ToString(),
                    Date = ((DateTime)(collection["Time"])).Date.ToString(),
                    Value = (double)collection["Sine"],
                    Error = _sine.Validation((double)collection["Sine"],0.0, amplitude)
                };
                //Insert Sine to Data base
                _signal.AddSignal(sine);

                //Check State Validation
                State _state = new State();
                Models.Signal state = new Models.Signal
                {
                    Type = "State",
                    TimeStamp = (DateTime)collection["Time"],
                    Time = ((DateTime)(collection["Time"])).TimeOfDay.ToString(),
                    Date = ((DateTime)(collection["Time"])).Date.ToString(),
                    Value = (double)collection["State"],
                    Error = _state.Validation((double)collection["State"], min_range, max_range)
                };
                //Insert State to Data Base
                _signal.AddSignal(state);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      

       
    }
}