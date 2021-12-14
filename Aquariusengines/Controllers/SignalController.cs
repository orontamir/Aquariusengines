using Aquariusengines.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aquariusengines.Controllers
{
    public class SignalController : Controller
    {

        private readonly ISignal _signal;

        public SignalController(ISignal signal)
        {
            _signal = signal;

        }

        [HttpGet]
        public string GetSine()
        {
            JArray rowArray = new JArray();
            var signalsSine = _signal.GetSineSignals(500);
           
            foreach(var sine in signalsSine)
            {
                // dataQuery.Rows[i][0], dataQuery.Rows[i][1]
                JObject row = new JObject(
                    new JProperty("c", new JArray(
                            new JObject(
                                new JProperty("v", sine.Time),
                                new JProperty("f", null)),
                            new JObject(
                                new JProperty("v", sine.Value),
                                new JProperty("f", null)
                                )
                            )
                    )
                );
                rowArray.Add(row);
            }
            JArray colArray = new JArray();

            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "time"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "number"));
            colArray.Add(jObject1);
            colArray.Add(jObject2);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", rowArray));


            return JsonConvert.SerializeObject(json);
        }

        [HttpGet]
        public string GetState()
        {
            JArray rowArray = new JArray();
            var signalsSine = _signal.GetStateSignal(500);

            foreach (var sine in signalsSine)
            {
                // dataQuery.Rows[i][0], dataQuery.Rows[i][1]
                JObject row = new JObject(
                    new JProperty("c", new JArray(
                            new JObject(
                                new JProperty("v", sine.Time),
                                new JProperty("f", null)),
                            new JObject(
                                new JProperty("v", sine.Value),
                                new JProperty("f", null)
                                )
                            )
                    )
                );
                rowArray.Add(row);
            }
            JArray colArray = new JArray();

            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "time"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "number"));
            colArray.Add(jObject1);
            colArray.Add(jObject2);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", rowArray));


            return JsonConvert.SerializeObject(json);
        }


        [HttpGet]
        public string GetError()
        {
            JArray rowArray = new JArray();
            var Errors = _signal.GetErrorSignal(500);
            foreach (var sine in Errors)
            {
                // dataQuery.Rows[i][0], dataQuery.Rows[i][1]
                JObject row = new JObject(
                    new JProperty("c", new JArray(
                            new JObject(
                                new JProperty("v", sine.Date),
                                new JProperty("f", null)),
                            new JObject(
                                new JProperty("v", sine.Type),
                                new JProperty("f", null)),
                            new JObject(
                                new JProperty("v", sine.Value),
                                new JProperty("f", null))
                            )
                    )
                );
                rowArray.Add(row);
            }
            JArray colArray = new JArray();
            JObject jObject1 = new JObject(new JProperty("id", ""), new JProperty("label", "Date"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject2 = new JObject(new JProperty("id", ""), new JProperty("label", "Type"), new JProperty("pattern", ""), new JProperty("type", "string"));
            JObject jObject3 = new JObject(new JProperty("id", ""), new JProperty("label", "Value"), new JProperty("pattern", ""), new JProperty("type", "string"));

            colArray.Add(jObject1);
            colArray.Add(jObject2);
            colArray.Add(jObject3);
            JObject json = new JObject(new JProperty("cols", colArray), new JProperty("rows", rowArray));


            return JsonConvert.SerializeObject(json);
        }

    }
}
