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
        private Codes? code;
        private Value historicalValue;
        private DateTime time;
        public Codes? Code { get => code; set => code = value; }
        public Value HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Id { get => id; set => id = value; }
        
       
        public int? HistoricalDescriptionId { get; set; }
        [ForeignKey("HistoricalDescriptionId")]
        public HistoricalDescription historicalDescription { get; set; }

        public HistoricalProperty ()
        {

        }

        public HistoricalProperty(Codes? code, Value value)
        {
            if(code == null || value == null)
            {
                throw new ArgumentNullException("Parameters cannot be null");
            }
            if((int)code < 0 || (int)code > 9)
            {
                throw new ArgumentException("Something wrong with code");
            }
            this.code = code;
            this.historicalValue = value;
            this.time = DateTime.Now;
            this.id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"ID:{id}\nCODE:{code}\nTIME:{time}\nVALUE:{historicalValue.ToString()}";
        }
    }
}
