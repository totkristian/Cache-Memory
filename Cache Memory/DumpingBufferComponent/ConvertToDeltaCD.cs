using ModelsAndProps.Dumping_buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBufferComponent
{
    public class ConvertToDeltaCD
    {
        public DeltaCD FillDeltaCD(Dictionary<int, List<Operations>> operationAndId, Dictionary<int, CollectionDescription> collectionDescriptions)
        {
            int cnt;
            DeltaCD deltaCD = new DeltaCD();
            deltaCD.TransactionID = Guid.NewGuid().ToString();
            for (int i = 1; i < 6; i++)
            {
                cnt = 0;
                List<Operations> ops = operationAndId[i];
                foreach (DumpingProperty dp in collectionDescriptions[i].DumpingPropertyCollection.DumpingProperties)
                {
                    switch (ops[cnt++])
                    {
                        case Operations.ADD:
                            deltaCD.Add[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Add[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            deltaCD.Add[i].Id = collectionDescriptions[i].Id;
                            break;
                        case Operations.UPDATE:
                            deltaCD.Update[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Update[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            deltaCD.Update[i].Id = collectionDescriptions[i].Id;
                            break;
                        case Operations.REMOVE:
                            deltaCD.Remove[i].Dataset = collectionDescriptions[i].Dataset;
                            deltaCD.Remove[i].DumpingPropertyCollection.DumpingProperties.Add(dp);
                            deltaCD.Remove[i].Id = collectionDescriptions[i].Id;
                            break;
                    }
                }
            }
            return deltaCD;
        }
    }
}
