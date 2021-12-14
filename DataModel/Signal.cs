using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Signal
    {
        public long Time { get; set; }

        public  Sine Sine { get; set; }

        public State State { get; set; }

        public Signal()
        {
            Sine = new Sine();
            State = new State();
        }

        //public override string ToString()
        //{
        //    JObject json = new JObject(
        //               new JProperty("Time", ""),
        //               new JProperty("Sine", ""),
        //               new JProperty("State", "")
        //               );
        //    return json.ToString();
        //}
    }
}
