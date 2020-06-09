using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalDescription
    {
        private int id;
        private int dataset;
        private List<HistoricalProperty> historicalProperties = new List<HistoricalProperty>();

        public HistoricalDescription()
        {

        }

        public HistoricalDescription(int id, int dataset)
        {
            if (dataset < 1 || dataset > 5)
                throw new ArgumentException("Dataset must be in interval from 1-5!");
            this.Id = id;
            this.dataset = dataset;
        }

        public int Dataset { get => dataset; set => dataset = value; }
        public List<HistoricalProperty> HistoricalProperties { get => historicalProperties; set => historicalProperties = value; }
        public int Id { get => id; set => id = value; }

        public int? ListDescriptionId { get; set; }
        [ForeignKey("ListDescriptionId")]
        public ListDescription listDescription { get; set; }
    }
}
