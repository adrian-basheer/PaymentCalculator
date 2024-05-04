using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.OutputGenerators
{
    public interface IOutputGenerator
    {
        public string GenerateOutput(Response response);
        public string[] GenerateOutput(Response[] responses);
    }
}
