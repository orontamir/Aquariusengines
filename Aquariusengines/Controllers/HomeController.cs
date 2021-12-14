using System;
using Aquariusengines.ViewModels;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Aquariusengines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISignal _signal;

        public HomeController(ISignal signal)
        {
            _signal = signal;
            
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([FromBody] JObject collection)
        {
            try
            {
                //Check Sine Validation
                Sine _sine = new Sine();

                Models.Signal sine = new Models.Signal
                {
                    Type = "Sine",
                    TimeStamp = (DateTime)collection["Time"],
                    Time = ((DateTime)(collection["Time"])).TimeOfDay.ToString(),
                    Date = ((DateTime)(collection["Time"])).Date.ToString(),
                    Value = (double)collection["Sine"],
                    Error = _sine.Validation((double)collection["Sine"],0.0,32.0)
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
                    Error = _state.Validation((double)collection["State"], 256, 4095)
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}