using LoggerComponent;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HistoricalComponent
{
    public class ConverterFromCdToHistorical
    {
        private static readonly object syncLock = new object();
        public HistoricalDescription ConvertCollectionDescription(CollectionDescription cd)
        {
            if(cd == null)
            {
                throw new ArgumentNullException("Parameters cannot be null");
            }
            HistoricalDescription hd = new HistoricalDescription();
            List<HistoricalProperty> histProp = new List<HistoricalProperty>();

            lock (syncLock)
            {
                Logger.WriteLog("Converting CollectionDescriprion", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
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
