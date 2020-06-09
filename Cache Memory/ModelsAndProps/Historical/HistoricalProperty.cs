using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalProperty
    {
        private string id;
        private Codes code;
        private Value historicalValue;
        private DateTime time;
        public Codes Code { get => code; set => code = value; }
        public Value HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Id { get => id; set => id = value; }
        
       
        public int? HistoricalDescriptionId { get; set; }
        [ForeignKey("HistoricalDescriptionId")]
        public HistoricalDescription historicalDescription { get; set; }

        public HistoricalProperty ()
        {

        }

        public HistoricalProperty(Codes codes, Value value)
        {
            this.code = codes;
            this.historicalValue = value;
        }
    }
}
