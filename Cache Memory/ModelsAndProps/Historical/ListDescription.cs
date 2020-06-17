using System.Collections.Generic;

namespace ModelsAndProps.Historical
{
    public class ListDescription
    {
        private int id;
        List<HistoricalDescription> historicalDescriptions = new List<HistoricalDescription>();

        public List<HistoricalDescription> HistoricalDescriptions { get => historicalDescriptions; set => historicalDescriptions = value; }
        public int Id { get => id; set => id = value; }

        public ListDescription()
        {

        }
    }
}
