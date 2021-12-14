using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquariusengines.ViewModels
{
    public class SignalDetailsViewModel
    {
        public IEnumerable<Models.Signal> GetSineSignals { get; set; }
        public IEnumerable<Models.Signal> GetStateSignal { get; set; }
        public IEnumerable<Models.Signal> GetErrorSignal { get; set; }
    }
}
