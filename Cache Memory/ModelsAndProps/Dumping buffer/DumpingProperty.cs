using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class DumpingProperty
    {
        private Codes code;
        private Value dumpingValue;

        public Codes Code { get => code; set => code = value; }
        public Value DumpingValue { get => dumpingValue; set => dumpingValue = value; }

        public DumpingProperty()
        {

        }

        public DumpingProperty(Codes code, Value dumpValue)
        {
            this.code = code;
            this.dumpingValue = dumpValue;
        }
    }
}
