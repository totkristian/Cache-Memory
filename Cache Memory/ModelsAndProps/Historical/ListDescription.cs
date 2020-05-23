using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class ListDescription
    {
        HashSet<HistoricalDescription> historicalDescriptions = new HashSet<HistoricalDescription>();

        public HashSet<HistoricalDescription> HistoricalDescriptions { get => historicalDescriptions; set => historicalDescriptions = value; }

        public ListDescription()
        {

        }
    }
}
