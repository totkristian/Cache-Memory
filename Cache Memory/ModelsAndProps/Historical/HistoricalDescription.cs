using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalDescription
    {
        private int id;
        private int dataset;
        private HashSet<HistoricalProperty> historicalProperties = new HashSet<HistoricalProperty>();

        public HistoricalDescription()
        {

        }

        public HistoricalDescription(int id, int dataset)
        {
            this.Id = id;
            this.dataset = dataset;
        }

        public int Dataset { get => dataset; set => dataset = value; }
        public HashSet<HistoricalProperty> HistoricalProperties { get => historicalProperties; set => historicalProperties = value; }
        public int Id { get => id; set => id = value; }
    }
}
