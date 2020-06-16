using HistoricalComponent;
using LoggerComponent;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DumpingBufferComponent
{
    public class DumpingBuffer
    {
        private static DumpingBuffer instance;
        private static readonly object syncLock = new object();
        private static Historical historical = Historical.GetInstance();
        private static Dictionary<int, CollectionDescription> collectionDescriptions = new Dictionary<int, CollectionDescription>(5);
        private static Dictionary<int, List<Operations>> operationAndId = new Dictionary<int, List<Operations>>();
        private static DeltaCD deltaCD = new DeltaCD();
        private ConvertToDeltaCD converter = new ConvertToDeltaCD();
        private static int counter = 0;

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
                    InitalizeCollectionDescriptions();
                }
            }

            return instance;
        }

        public void WriteToDumpingBuffer(Operations op, Codes code, Value val)
        {

            if (val == null)
            {
                throw new ArgumentNullException("Value cannot be null");
            }
            if ((int)code < 0 || (int)code > 9)
                throw new ArgumentException("Code must be in interval 0-9!");
            if((int)op < 0 || (int)op > 2)
            {
                throw new ArgumentException("Operation invalid");
            }
            
            //check Val.Consumption and the resto of the properties if they are valid
            DumpingProperty dp = new DumpingProperty(code, val);
            int dataset = historical.CheckDataset(code);

            if (dataset == -1)
            {
                //something wrong with dataset
            }
            bool updated = false;
            lock (syncLock)
            {
                Logger.WriteLog("Writing to dumping buffer", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            updated = CheckUpdate(dataset, dp);

            if (!updated)
            {//data does not exist, i need to add id 
                collectionDescriptions[dataset].Dataset = dataset;
                collectionDescriptions[dataset].DumpingPropertyCollection.DumpingProperties.Add(dp); //i can make a new dp here also
                collectionDescriptions[dataset].Id = dataset;
                AddToOperationsAndId(collectionDescriptions[dataset].Id, op); //operation can be add,remove or update 
                counter++;
                //UPDATE HAS HAPPEND in the CHECKUPDATE function


            }
            //data added need to check dp.COunt;
            if (checkDumpingPropertyCount() && counter < 3)
            {

                // FillDeltaCD(); //pack data into deltaCD component
                deltaCD = converter.FillDeltaCD(operationAndId, collectionDescriptions);
                //send data to historical (make a converter or something)
                SendToHistorical();
                //clear dictonarys
                ClearStructures();


            }
            else if (counter >= 10)
            {
                //we have enough data, add into deltaCD and send and clear
                /*FillDeltaCD(); */
                deltaCD = converter.FillDeltaCD(operationAndId, collectionDescriptions);

                //send data to historical (make a converter or something)
                SendToHistorical();
                //clear dictonarys
                ClearStructures();
            }
        }
        public void AddToOperationsAndId(int id, Operations operation)
        {
            if(id < 1 || id > 5)
            {
                throw new ArgumentException("Something wrong with id");
            }
            if ((int)operation < 0 || (int)operation > 2)
            {
                throw new ArgumentException("Operation invalid");
            }

            if (operationAndId.ContainsKey(id))
            {
                operationAndId[id].Add(operation);
            }
            else
            {
                operationAndId.Add(id, new List<Operations>());
                operationAndId[id].Add(operation);
            }
        }

        private bool CheckUpdate(int dataset, DumpingProperty tempDp)
        {
            if (dataset < 1 || dataset > 5)
            {
                //baci exception
            }



            foreach (DumpingProperty dp in collectionDescriptions[dataset].DumpingPropertyCollection.DumpingProperties)
            {
                if (dp.DumpingValue.GeographicalLocationId == tempDp.DumpingValue.GeographicalLocationId)
                {
                    dp.DumpingValue = tempDp.DumpingValue;
                    return true;
                }
            }

            return false;
        }

        private bool checkDumpingPropertyCount()
        {

            for (int i = 1; i < 6; i++)
            {
                if (collectionDescriptions[i].DumpingPropertyCollection.DumpingProperties.Count >= 2)
                {
                    return true;
                }
            }
            return false;
        }


        private void ClearStructures()
        {

            collectionDescriptions.Clear();
            operationAndId.Clear();
            InitalizeCollectionDescriptions();
            deltaCD = new DeltaCD();
            counter = 0;
        }

        private static void InitalizeCollectionDescriptions()
        {

            for (int i = 1; i < 6; i++)
            {
                collectionDescriptions.Add(i, new CollectionDescription());
                operationAndId.Add(i, new List<Operations>());
            }
        }

        private void SendToHistorical()
        {
            lock (syncLock)
            {
                Logger.WriteLog("Sending to historical from dumbing buffer", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            historical.ReadFromDumpingBuffer(deltaCD);
        }
    }
}
