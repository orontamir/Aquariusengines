using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquariusengines.Models
{
    public class SQLSignal : ISignal
    {
        private readonly AppDbContext context;
        public SQLSignal(AppDbContext _context)
        {
            this.context = _context;
        }
        public Signal AddSignal(Signal signal)
        {
            context.Signals.Add(signal);
            context.SaveChanges();
            return signal;
        }

        public IEnumerable<Signal> GetErrorSignal(int number)
        {
            return context.Signals.Where(o => o.Error == true).Take(number).ToList<Signal>();
        }


        public IEnumerable<Signal> GetSineSignals(int number)
        {
            return context.Signals.Where(o => o.Type == "Sine" && o.Error == false).OrderByDescending(o => o.TimeStamp).Take(number).ToList<Signal>();
        }

        public IEnumerable<Signal> GetStateSignal(int number)
        {
            return context.Signals.Where(o => o.Type == "State" && o.Error == false).OrderByDescending(o => o.TimeStamp).Take(number).ToList<Signal>();
        }
    }
}
