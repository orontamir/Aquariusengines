using Aquariusengines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquariusengines.ViewModels
{
    public interface ISignal
    {
        IEnumerable<Signal> GetSineSignals(int number);
        IEnumerable<Signal> GetStateSignal(int number);
        IEnumerable<Signal> GetErrorSignal(int number);


        Signal AddSignal(Signal task);
    }
}
