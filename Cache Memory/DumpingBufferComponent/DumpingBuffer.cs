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
        private static Dictionary<int,List<Operations>> operationAndId;
        private static DeltaCD deltaCD;
        private static int counter;
        
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
                    operationAndId = new Dictionary<int, List<Operations>>();
                    counter = 0;
                    deltaCD = new DeltaCD();
                }
            }

            return instance;
        }

        public void WriteToDumpingBuffer(Operations op,Codes code, Value val)
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
            {//data does not exist, i need to add id 
                collectionDescriptions[dataset].Dataset = dataset;
                collectionDescriptions[dataset].DumpingPropertyCollection.DumpingProperties.Add(dp); //i can make a new dp here also
                collectionDescriptions[dataset].Id = dataset;
                AddToOperationsAndId(collectionDescriptions[dataset].Id, op); //operation can be add or remove, or update 
                //UPDATE HAS HAPPEND in the CHECKUPDATE function

                
            }
            //data added need to check dp.COunt;
            if(checkDumpingPropertyCount() && counter < 3)
            {
                
                FillDeltaCD(); //pack data into deltaCD component
                //clear dictonarys

            }
            else if(counter >= 10)
            {
                //we have enough data, add into deltaCD and send and clear
                FillDeltaCD();
            }
        }
        private void AddToOperationsAndId(int id, Operations operation)
        {
            if(operationAndId.ContainsKey(id))
            {
                operationAndId[id].Add(operation);
            }
            else
            {
                operationAndId.Add(id, new List<Operations>());
                operationAndId[id].Add(operation);
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

        private bool checkDumpingPropertyCount()
        {
            for(int i =1; i <6;i++)
            {
                if(collectionDescriptions[i].DumpingPropertyCollection.DumpingProperties.Count >=2)
                {
                    return true;
                }
            }
            return false;
        }

        private void FillDeltaCD()
        {
            int cnt;
            for(int i = 1; i < 6; i++)
            {
                cnt = 0;
                List<Operations> ops = operationAndId[i];
                foreach (DumpingProperty dp in collectionDescriptions[i].DumpingPropertyCollection.DumpingProperties)
                {
                    switch(ops[cnt++])
                    {
                        case Operations.ADD:
                            deltaCD.Add[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Add[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            break;
                        case Operations.UPDATE:
                            deltaCD.Update[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Update[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            break;
                        case Operations.REMOVE:
                            deltaCD.Remove[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Remove[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            break;
                    }
                    
                }
            }
        }
    }
}
