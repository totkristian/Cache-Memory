using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.ValueStructure
{
    public class Value
    {
        private DateTime timestamp;
        private int geographicalLocationId;
        private double consumption;

        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public int GeographicalLocationId { get => geographicalLocationId; set => geographicalLocationId = value; }
        public double Consumption { get => consumption; set => consumption = value; }

        public Value()
        {

        }
        public Value(DateTime timestamp, int geoId,double usage)
        {
            this.timestamp = timestamp;
            this.geographicalLocationId = geoId;
            this.consumption = usage;
        }
       
    }
}
