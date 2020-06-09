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

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static object syncLock = new object();
        private Database database = new Database();
        private static IQueryable<ListDescription> listDescription1;
        private static IQueryable<ListDescription> listDescription2;
        private static IQueryable<ListDescription> listDescription3;
        private static IQueryable<ListDescription> listDescription4;
        private static IQueryable<ListDescription> listDescription5;

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

        public void AddToDatabase()
        {
            try
            {
               // database.ListDescriptions.Add(lista);
                database.SaveChanges();
                Logger.WriteLog("Successfully added to database", "Historical", "AddToDatabase");
            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex.Message, "Historical", "AddToDatabase");
            }
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
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescription5.Select(x => x.HistoricalDescriptions))
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
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescription4.Select(x => x.HistoricalDescriptions))
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
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescription3.Select(x => x.HistoricalDescriptions))
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
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescription2.Select(x => x.HistoricalDescriptions))
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
            foreach (HistoricalDescription hd in (List<HistoricalDescription>)listDescription1.Select(x => x.HistoricalDescriptions))
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
                listDescription1 = database.ListDescription1;
                listDescription2 = database.ListDescription2;
                listDescription3 = database.ListDescription3;
                listDescription4 = database.ListDescription4;
                listDescription5 = database.ListDescription5;
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
                //throw new exception
            }

            hDesc.Dataset = dataset;
            hDesc.Id = dataset; //so that we can query our list
            ListDescription listDescription = new ListDescription();
            listDescription.Id = dataset;
            listDescription.HistoricalDescriptions.Add(hDesc);

            switch(dataset)
            {
                case 1:
                    //checkDeadBand
                    database.ListDescription1.Add(listDescription);
                    database.SaveChanges();
                    break;
                case 2:
                    //checkDeadBand
                    database.ListDescription2.Add(listDescription);
                    database.SaveChanges();
                    break;
                case 3:
                    //checkDeadBand
                    database.ListDescription3.Add(listDescription);
                    database.SaveChanges();
                    break;
                case 4:
                    //checkDeadBand
                    database.ListDescription4.Add(listDescription);
                    database.SaveChanges();
                    break;
                case 5:
                    //checkDeadBand
                    database.ListDescription5.Add(listDescription);
                    database.SaveChanges();
                    break;
            }





        }
        

    }
}
