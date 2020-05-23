using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalProperty
    {
        private Codes codes;
        private float historicalValue;
        private DateTime time;

        public Codes Codes { get => codes; set => codes = value; }
        public float HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }


        public HistoricalProperty ()
        {

        }

        public HistoricalProperty(Codes codes, float value)
        {
            this.codes = codes;
            this.historicalValue = value;
        }
    }
}
