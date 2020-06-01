using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class ListDescription
    {
        private int id;
        HashSet<HistoricalDescription> historicalDescriptions = new HashSet<HistoricalDescription>();

        public HashSet<HistoricalDescription> HistoricalDescriptions { get => historicalDescriptions; set => historicalDescriptions = value; }
        public int Id { get => id; set => id = value; }

        public ListDescription()
        {

        }
    }
}
