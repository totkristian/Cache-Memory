using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class Historical
    {
        private Codes codes;
        private float historicalValue;
        private DateTime time;

        public Codes Codes { get => codes; set => codes = value; }
        public float HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }


        public Historical()
        {

        }

        public Historical(Codes codes, float value)
        {
            this.codes = codes;
            this.historicalValue = value;
        }
    }
}
