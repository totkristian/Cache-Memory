using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriterComponent
{
    public class RandomGenerator
    {
        Random random = new Random();
        public RandomGenerator()
        {

        }
        public Value RandomValueGenerator()
        {
            Value val = new Value();
            val.Consumption = (float)random.NextDouble() * 10;
            val.GeographicalLocationId = Guid.NewGuid().ToString();
            val.Timestamp = DateTime.Now;
            return val;
        }
        public Codes GenerateRandomCode()
        {
            return (Codes)random.Next(0, 10);
        }
    }
}
