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
        private double consumption;

        public DateTime? Timestamp { get => timestamp; set => timestamp = value; }
        public string GeographicalLocationId { get => geographicalLocationId; set => geographicalLocationId = value; }
        public double Consumption { get => consumption; set => consumption = value; }

        public Value()
        {

        }
        public Value(DateTime? timestamp, string geoId, double usage)
        {
            if (timestamp == null || string.IsNullOrWhiteSpace(geoId) || usage == 0.0)
            {
                throw new ArgumentNullException("Arguments cannot be null");
            }
            if (usage <= 0.0)
            {
                throw new ArgumentException("Usage cannot be negative");
            }

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
