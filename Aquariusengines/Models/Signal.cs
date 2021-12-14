using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquariusengines.Models
{
    public class Signal
    {
        public int Id { get; set; }
        public DateTime TimeStamp {get; set;}

        public string Time { get; set; }
        public string Date { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }

        public bool Error { get; set; }
    }
}
