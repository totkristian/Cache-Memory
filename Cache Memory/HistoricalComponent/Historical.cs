using HistoricalComponent.DatabaseConn;
using LoggerComponent;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static readonly object syncLock = new object();
        private Database database = new Database();
        private DatabaseOperations databaseOperations = new DatabaseOperations();
        private ConverterFromCdToHistorical converter = new ConverterFromCdToHistorical();


        private static List<HistoricalProperty> lista;
        public Historical()
        {

        }

        public static Historical GetInstance()
        {
            lock (syncLock)
            {
                if (instance == null)
                {
                    instance = new Historical();
                    lista = new List<HistoricalProperty>();
                }
            }

            return instance;
        }


        public int CheckDataset(Codes code)
        {
            if ((int)code < 0 || (int)code > 9)
                throw new ArgumentException("Code must be in interval 0-9!");

            //lock (syncLock)
            //{
            //    Logger.WriteLog("Checking dataset", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            //}

            switch (code)
            {
                case Codes.CODE_ANALOG:
                case Codes.CODE_DIGITAL:
                    return 1;
                case Codes.CODE_CUSTOM:
                case Codes.CODE_LIMITSET:
                    return 2;
                case Codes.CODE_SINGLENODE:
                case Codes.CODE_MULTIPLENODE:
                    return 3;
                case Codes.CODE_CONSUMER:
                case Codes.CODE_SOURCE:
                    return 4;
                case Codes.CODE_MOTION:
                case Codes.CODE_SENSOR:
                    return 5;
                default:
                    return -1;
            }

        }

        public void ManualWriteToHistory(Codes code, Value val)
        {
            if((int)code < 0 || (int)code > 9)
            {
                throw new ArgumentException("Something wront with code");
            }
            HistoricalProperty hProp = new HistoricalProperty(code, val);

            HistoricalDescription hDesc = new HistoricalDescription();
            hDesc.HistoricalProperties.Add(hProp);
            int dataset = CheckDataset(code);


            if (dataset == -1)
            {
                Console.WriteLine("Dataset parsing went wrong!");
                return;
            }

            hDesc.Dataset = dataset;

            lock (syncLock)
            {
                Logger.WriteLog("Writing to history", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            databaseOperations.AddHistoricalDescription(hDesc, dataset);
        }

        public ListDescription ReadOneLDFromDB(int dataset)
        {
            if(dataset < 1 || dataset > 5)
            {
                throw new ArgumentException("Something wrong with dataset");
            }
            return databaseOperations.ReadListDescription(dataset);
        }
        public List<HistoricalProperty> GetHistoricalProperties()
        {
            return databaseOperations.ReadHistoricalProperties();
        }

        public void ReadFromDumpingBuffer(DeltaCD deltaCD)
        {
            if(deltaCD == null || deltaCD.Add == null || deltaCD.Remove == null || deltaCD.Update == null || deltaCD.TransactionID == null)
            {
                throw new ArgumentNullException("parameters cannot be null");
            }
            lock (syncLock)
            {
                Logger.WriteLog("Reading from Dumping buffer", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            for (int i = 1; i < 6; i++)
            {
                //check if i have data in any of these
                if (checkIfTheresDataInCollectionDescription(deltaCD.Add[i]))
                {
                    HistoricalDescription hd = converter.ConvertCollectionDescription(deltaCD.Add[i]);
                    databaseOperations.AddHistoricalDescription(hd, i);
                }

                if (checkIfTheresDataInCollectionDescription(deltaCD.Update[i]))
                {
                    HistoricalDescription hd = converter.ConvertCollectionDescription(deltaCD.Update[i]);
                    databaseOperations.UpdateHistoricalDescriptions(hd, i);
                }

                if (checkIfTheresDataInCollectionDescription(deltaCD.Remove[i]))
                {
                    HistoricalDescription hd = converter.ConvertCollectionDescription(deltaCD.Remove[i]);
                    databaseOperations.RemoveHistoricalProperties(hd, i);
                }
            }
        }
        public bool checkIfTheresDataInCollectionDescription(CollectionDescription cd)
        {
            try
            {
                if (cd == null || cd.Dataset == 0 || cd.Id == 0 || cd.DumpingPropertyCollection.DumpingProperties.Count == 0)
                {
                    throw new ArgumentNullException("Everything is null");
                }
                return true;
            }
            catch{
                return false;
            }
            
        }
        public bool CheckIfIdIsUnique(string id)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentNullException("ID cannot be null");
                }
                return databaseOperations.CheckGeoId(id);
            }
            catch
            {
                return true; //so it does not add are hProp to database
            }
           
        }
    }
}
