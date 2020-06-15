using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponent
{
    public class ConverterFromCdToHistorical
    {
        public HistoricalDescription ConvertCollectionDescription(CollectionDescription cd, int dataset)
        {
            HistoricalDescription hd = new HistoricalDescription();
            List<HistoricalProperty> histProp = new List<HistoricalProperty>();

            foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
            {
                HistoricalProperty hp = new HistoricalProperty();
                hp.Code = dp.Code;
                hp.Time = DateTime.Now;
                hp.Id = Guid.NewGuid().ToString();
                hp.HistoricalValue = dp.DumpingValue;
                histProp.Add(hp);
            }
            hd.HistoricalProperties = histProp;
            hd.Dataset = cd.Dataset;

            return hd;
        }
    }
}
