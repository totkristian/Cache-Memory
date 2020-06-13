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

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static readonly object syncLock = new object();
        private Database database = new Database();
        private static IQueryable<ListDescription> listDescriptions;


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

        public List<HistoricalProperty> GetChangesForInterval(Codes code)
        {
            switch (code)
            {
                case Codes.CODE_ANALOG:
                case Codes.CODE_DIGITAL:
                    return GetCgangesForAnalogOrDigital(code);

                case Codes.CODE_CUSTOM:
                case Codes.CODE_LIMITSET:
                    return GetChangesForCustomOrLimitset(code);

                case Codes.CODE_SINGLENODE:
                case Codes.CODE_MULTIPLENODE:
                    return GetChangesForSinglenodeOrMultiplenode(code);

                case Codes.CODE_CONSUMER:
                case Codes.CODE_SOURCE:
                    return GetChangesForConsumerOrSource(code);

                case Codes.CODE_MOTION:
                case Codes.CODE_SENSOR:
                    return GetChangesForMotionOrSensor(code);
                default:
                    return null;
            }
            
        }

        private List<HistoricalProperty> GetChangesForMotionOrSensor(Codes code)
        {
            ReadFromDatabase();
            List<HistoricalProperty> ret = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescriptions.Where(x=>x.Id == 5).Select(x => x.HistoricalDescriptions))
            {
                foreach(HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (code.Equals(hp.Code))
                    {
                        ret.Add(hp);
                    }
                }
            }
            return ret;
        }

        private List<HistoricalProperty> GetChangesForConsumerOrSource(Codes code)
        {
            ReadFromDatabase();
            List<HistoricalProperty> ret = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescriptions.Where(x => x.Id == 4).Select(x => x.HistoricalDescriptions))
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (code.Equals(hp.Code))
                    {
                        ret.Add(hp);
                    }
                }
            }
            return ret;
        }

        private List<HistoricalProperty> GetChangesForSinglenodeOrMultiplenode(Codes code)
        {
            ReadFromDatabase();
            List<HistoricalProperty> ret = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescriptions.Where(x => x.Id == 3).Select(x => x.HistoricalDescriptions))
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (code.Equals(hp.Code))
                    {
                        ret.Add(hp);
                    }
                }
            }
            return ret;
        }

        private List<HistoricalProperty> GetChangesForCustomOrLimitset(Codes code)
        {
            ReadFromDatabase();
            List<HistoricalProperty> ret = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescriptions.Where(x => x.Id == 2).Select(x => x.HistoricalDescriptions))
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (code.Equals(hp.Code))
                    {
                        ret.Add(hp);
                    }
                }
            }
            return ret;
        }

        private List<HistoricalProperty> GetCgangesForAnalogOrDigital(Codes code)
        {
            ReadFromDatabase();
            List<HistoricalProperty> ret = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescriptions.Where(x => x.Id == 1).Select(x => x.HistoricalDescriptions))
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (code.Equals(hp.Code))
                    {
                        ret.Add(hp);
                    }
                }
            }
            return ret;
        }

        public void ReadFromDatabase()
        {
            try
            {
                listDescriptions = database.ListDescriptions;
                Logger.WriteLog("Successfully read from database", "Historical", "ReadFromDatabase");
            }
            catch(Exception ex)
            {
               // Debug.WriteLine(ex.Message);
                Logger.WriteLog(ex.Message, "Historical", "ReadFromDatabase");
            }
        }

        public void ManualWriteToHistory(Codes code, Value val)
        {
            HistoricalProperty hProp = new HistoricalProperty();
            
            hProp.Code = code;
            hProp.HistoricalValue = val;
            hProp.Time = DateTime.Now;
            hProp.Id = Guid.NewGuid().ToString();

            HistoricalDescription hDesc = new HistoricalDescription();
            hDesc.HistoricalProperties.Add(hProp);
            int dataset = CheckDataset(code);
            
            
            if(dataset == -1)
            {
                Console.WriteLine("Dataset parsing went wrong!");
                return;
            }

            hDesc.Dataset = dataset;

            ReadFromDatabase();

            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            list1.HistoricalDescriptions.Add(hDesc);
            database.SaveChanges();
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


            List<HistoricalDescription> l = (List<HistoricalDescription>)listDescriptions.Where(x => x.Id == dataset).Select(x => x.HistoricalDescriptions);
            foreach (HistoricalDescription hd in l)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if(hp.Id == hprop.Id)
                    {
                        if (hprop.HistoricalValue.Consumption < (hp.HistoricalValue.Consumption - (hp.HistoricalValue.Consumption / 100) * 2) ||
                                hprop.HistoricalValue.Consumption > (hp.HistoricalValue.Consumption + (hp.HistoricalValue.Consumption / 100) * 2))
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
            ListDescription ld = new ListDescription();
            List<HistoricalDescription> list = database.HistoricalDescriptions.Where(x => x.ListDescriptionId == dataset).ToList();
            ld.HistoricalDescriptions = list;

            for (int i = 0; i < ld.HistoricalDescriptions.Count; i++)
            {
                int id = ld.HistoricalDescriptions[i].Id;
                List<HistoricalProperty> hpList = database.HistoricalProperties.Where(x => x.HistoricalDescriptionId == id).ToList();
                ld.HistoricalDescriptions[i].HistoricalProperties = hpList;
            }

            return ld;
        }

        public List<HistoricalProperty> GetHistoricalProperties()
        {
            List<HistoricalProperty> list = database.HistoricalProperties.ToList();
            return list;
        }

        public void ReadFromDumpingBuffer(DeltaCD deltaCD)
        {
            for (int i = 1; i < 6; i++)
            {
                CollectionDescription add = deltaCD.Add[i];
                CollectionDescription update = deltaCD.Update[i];
                CollectionDescription remove = deltaCD.Remove[i];

                
            }
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
                //setuj id
                hp.HistoricalValue = dp.DumpingValue;
                histProp.Add(hp);

            }
            hd.HistoricalProperties = histProp;
            hd.Dataset = cd.Dataset;
            //hd.Id = 

            // list1.HistoricalDescriptions = database.HistoricalDescriptions.Where(x => x.Dataset == dataset).ToList();
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }
    }
}
