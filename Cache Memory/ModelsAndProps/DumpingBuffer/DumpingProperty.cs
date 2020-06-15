using ModelsAndProps.ValueStructure;
using System;

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
            if (dumpValue == null)
            {
                throw new ArgumentNullException("Arguments cannot be null");
            }
            if ((int)code < 0 || (int)code > 9)
            {
                throw new ArgumentException("Something wrong with code");
            }
            this.code = code;
            this.dumpingValue = dumpValue;
        }
    }
}
