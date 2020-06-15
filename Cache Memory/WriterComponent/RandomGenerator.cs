using ModelsAndProps.Historical;
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
        public Value RandomNewValueGenerator()
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

        public Operations GenerateRandomOperation()
        {
            return (Operations)random.Next(0, 3);
        }

        public HistoricalProperty getRandomPropertyForUpdateOrRemove(List<HistoricalProperty> hp)
        {

                if (hp.Count > 0)
                {
                    return hp[random.Next(0, hp.Count)];
                }
                else
                    return null;
        }
    }
}
