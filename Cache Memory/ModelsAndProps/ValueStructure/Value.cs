using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.ValueStructure
{
    public class Value
    {
        private DateTime? timestamp;
        private string geographicalLocationId;
        private float consumption;

        public DateTime? Timestamp { get => timestamp; set => timestamp = value; }
        public string GeographicalLocationId { get => geographicalLocationId; set => geographicalLocationId = value; }
        public float Consumption { get => consumption; set => consumption = value; }

        public Value()
        {

        }
        public Value(DateTime? timestamp, string geoId,float usage)
        {
            this.timestamp = timestamp;
            this.geographicalLocationId = geoId;
            this.consumption = usage;
        }

        public override string ToString()
        {
            return $"\n\tGEO ID:{geographicalLocationId}\n\tCONSUMPTION:{consumption}\n\tTIME STAMP:{timestamp} \n-----------------------------------------------";
        }

    }
}
