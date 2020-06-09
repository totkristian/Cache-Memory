using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalProperty
    {
        private int id;
        private Codes code;
        private Value historicalValue;
        private DateTime time;

        public Codes Code { get => code; set => code = value; }
        public Value HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }
        public int Id { get => id; set => id = value; }

        public HistoricalProperty ()
        {

        }

        public HistoricalProperty(Codes codes, Value value)
        {
            this.code = codes;
            this.historicalValue = value;
        }
    }
}
