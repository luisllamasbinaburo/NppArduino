using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NppArduino.Domain
{
    public class CompileOption
    {
        public string Option { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Option}={Value}";
        }
    }
}
