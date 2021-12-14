using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Sine : BaseSignal
    {
        private static readonly int _timeToRun = int.Parse(ConfigurationManager.AppSettings["TimeToRun"]);
        private static readonly int _everyMsc = int.Parse(ConfigurationManager.AppSettings["everySec"]);
        private static readonly double SampleRate = _timeToRun * _everyMsc;
        private static readonly double amplitude = double.Parse(ConfigurationManager.AppSettings["amplitude"]);
        private static readonly double frequency = double.Parse(ConfigurationManager.AppSettings["frequency"]) ;
        private static readonly double bad_amplitude = amplitude * 2;
        public override double GetBadSignal(double tict)
        {
            return GetRandomNumber(amplitude, bad_amplitude);
        }

        public override double GetGoodSignal(double tict)
        {
            return ((amplitude /2.0) *( Math.Sin((2 * Math.PI * tict * frequency) / SampleRate))) + (amplitude / 2.0);           
        }

        public override bool Validation(double num, double min, double max)
        {
            return (num > max) || (num < min);
        }

    }
}
