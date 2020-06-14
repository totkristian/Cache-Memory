using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsAndProps.Historical;
using LoggerComponent;
using System.Diagnostics;
using HistoricalComponent.DatabaseConn;
using ModelsAndProps.ValueStructure;
using System.Xml;
using ModelsAndProps.Dumping_buffer;
using System.Runtime.CompilerServices;

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static readonly object syncLock = new object();
        private Database database = new Database();
        private DatabaseOperations databaseOperations = new DatabaseOperations();


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
           switch(code)
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
            HistoricalProperty hProp = new HistoricalProperty(code,val);

            HistoricalDescription hDesc = new HistoricalDescription();
            hDesc.HistoricalProperties.Add(hProp);
            int dataset = CheckDataset(code);
            
            
            if(dataset == -1)
            {
                Console.WriteLine("Dataset parsing went wrong!");
                return;
            }

            hDesc.Dataset = dataset;

            //call logger
            databaseOperations.AddHistoricalDescription(hDesc, dataset);
           /* ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            list1.HistoricalDescriptions.Add(hDesc);
            database.SaveChanges();*/
        }



        private bool CheckDeadband(HistoricalProperty hprop, int dataset)
        {
            if(hprop == null)
            {
                throw new ArgumentNullException("You need to have historical property!");
            }

            
            if (hprop.Code.Equals(Codes.CODE_DIGITAL))
            {
                return true;
            }

            ListDescription list = ReadOneLDFromDB(dataset);
            foreach (HistoricalDescription hd in list.HistoricalDescriptions)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if(hp.Id == hprop.Id)
                    {
                        if (hprop.HistoricalValue.Consumption < (hp.HistoricalValue.Consumption - hp.HistoricalValue.Consumption * 0.02) ||
                                hprop.HistoricalValue.Consumption > (hp.HistoricalValue.Consumption + hp.HistoricalValue.Consumption * 0.02))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        public ListDescription ReadOneLDFromDB(int dataset)
        {
            return databaseOperations.ReadListDescription(dataset);
        }
        public List<HistoricalProperty> GetHistoricalProperties()
        {
            return databaseOperations.ReadHistoricalProperties();
        }

        public void ReadFromDumpingBuffer(DeltaCD deltaCD)
        {
            for (int i = 1; i < 6; i++)
            {
                //check if i have data in any of these
                if(checkIfTheresDataInCollectionDescription(deltaCD.Add[i]))
                {
                    AddCollectionDescription(deltaCD.Add[i], i);
                }
                
                if (checkIfTheresDataInCollectionDescription(deltaCD.Update[i]))
                {
                    UpdateCollectionDescription(deltaCD.Update[i], i);
                }
                
                if (checkIfTheresDataInCollectionDescription(deltaCD.Remove[i]))
                {
                    RemoveCollectionDescription(deltaCD.Remove[i], i);
                }
                
                
            }
        }
        private bool checkIfTheresDataInCollectionDescription(CollectionDescription cd)
        {
            if(cd.Dataset == 0 || cd.Id == 0 || cd.DumpingPropertyCollection.DumpingProperties.Count == 0)
            {
                return false;
            }
            return true;
        }

        public void AddCollectionDescription(CollectionDescription cd,int dataset)
        {
            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
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
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }
        public void UpdateCollectionDescription(CollectionDescription cd, int dataset)
        {
            List<HistoricalProperty> historicalProperties = database.HistoricalProperties.ToList();
            foreach(DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
            {
                
                foreach(HistoricalProperty hp in historicalProperties)
                {
                    if(hp.HistoricalValue.GeographicalLocationId == dp.DumpingValue.GeographicalLocationId && hp.Code.Equals(dp.Code))
                    {
                        CheckDeadband(hp, dataset);
                        hp.HistoricalValue = dp.DumpingValue;
                        break;
                    }
                }
            }
            database.SaveChanges();
        }

        public void RemoveCollectionDescription(CollectionDescription cd, int dataset)
        {
            List<HistoricalProperty> historicalProperties = database.HistoricalProperties.ToList();
            foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
            {
                for(int i = 0; i < historicalProperties.Count; i++)
                {
                    if (historicalProperties[i].HistoricalValue.GeographicalLocationId == dp.DumpingValue.GeographicalLocationId && historicalProperties[i].Code.Equals(dp.Code))
                    {
                        database.HistoricalProperties.Remove(historicalProperties[i]);
                        break;
                    }
                }
            }
            database.SaveChanges();
        }
    }
}
