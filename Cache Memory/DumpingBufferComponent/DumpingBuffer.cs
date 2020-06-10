using HistoricalComponent;
using ModelsAndProps;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.ValueStructure;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBufferComponent
{
    public class DumpingBuffer
    {
        private static DumpingBuffer instance;
        private static readonly object syncLock = new object();
        private static Historical historical = Historical.GetInstance();
        private static Dictionary<int,CollectionDescription> collectionDescriptions;
        public DumpingBuffer()
        {

        }

        public static DumpingBuffer GetInstance()
        {
            lock (syncLock)
            {
                if (instance == null)
                {
                    instance = new DumpingBuffer();
                    collectionDescriptions = new Dictionary<int, CollectionDescription>(5);
                    collectionDescriptions.Add(1, new CollectionDescription());
                    collectionDescriptions.Add(2, new CollectionDescription());
                    collectionDescriptions.Add(3, new CollectionDescription());
                    collectionDescriptions.Add(4, new CollectionDescription());
                    collectionDescriptions.Add(5, new CollectionDescription());
                }
            }

            return instance;
        }

        public void WriteToDumpingBuffer(Codes code, Value val)
        {
            if ((int)code < 0 || (int)code > 9)
                throw new ArgumentException("Code must be in interval 0-9!");
            //check Val.Consumption and the resto of the properties if they are valid
            DumpingProperty dp = new DumpingProperty(code, val);
            int dataset = historical.CheckDataset(code);

            if(dataset == -1)
            {
                //something wrong with dataset
            }
            bool updated = false;

            updated = CheckUpdate(dataset,dp);

            if(!updated)
            {
                collectionDescriptions[dataset].Dataset = dataset;
                collectionDescriptions[dataset].DumpingPropertyCollection.DumpingProperties.Add(dp); //i can make a new dp here also
                collectionDescriptions[dataset].Id = Guid.NewGuid().ToString();
            }
           
        }

        private bool CheckUpdate(int dataset,DumpingProperty tempDp)
        {
            if(dataset < 1 || dataset > 5)
            {
                //baci exception
            }
           
            foreach(DumpingProperty dp in collectionDescriptions[dataset].DumpingPropertyCollection.DumpingProperties)
            {
                if (dp.Code.Equals(tempDp.Code))
                {
                    dp.DumpingValue = tempDp.DumpingValue;
                    return true;
                }
            }

            return false;
        }
    }
}
