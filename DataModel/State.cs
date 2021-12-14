using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class State : BaseSignal
    {
        private static readonly double max_range = double.Parse(ConfigurationManager.AppSettings["max_range"]);
        private static readonly double min_range = double.Parse(ConfigurationManager.AppSettings["min_range"]);
        private static readonly double bad_range = max_range + min_range;
        public override double GetBadSignal(double tict)
        {
            return GetRandomNumber(max_range, bad_range);
        }

        public override double GetGoodSignal(double tict)
        {
            return GetRandomNumber(min_range,max_range);
        }

        public override bool Validation(double num, double min, double max)
        {
            return (num > max) || (num < min);
        }

    }
}
