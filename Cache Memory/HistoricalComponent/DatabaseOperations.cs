﻿using HistoricalComponent.DatabaseConn;
using LoggerComponent;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HistoricalComponent
{
    public class DatabaseOperations
    {
        private Database database = new Database();
        private static readonly object syncLock = new object();

        public void AddHistoricalDescription(HistoricalDescription hd, int dataset)
        {
            if (hd == null)
            {
                throw new ArgumentNullException("Arguments cannot be null");
            }
            if (dataset < 1 || dataset > 5)
            {
                throw new ArgumentException("Something wrong with dataset");
            }
            //call logger
            lock (syncLock)
            {
                Logger.WriteLog("Adding historical description", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            if(list1 == null)
            {
                throw new ArgumentNullException("Object reference not set to instance of an object(list null for db)");
            }
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }

        public ListDescription ReadListDescription(int dataset)
        {
            if (dataset < 1 || dataset > 5)
            {
                throw new ArgumentException("Something wrong with dataset");
            }
            //call logger
            ListDescription ld = new ListDescription();
            List<HistoricalDescription> list = database.HistoricalDescriptions.Where(x => x.ListDescriptionId == dataset).ToList();
            ld.HistoricalDescriptions = list;

            lock (syncLock)
            {
                Logger.WriteLog("Reading list description", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            for (int i = 0; i < ld.HistoricalDescriptions.Count; i++)
            {
                int id = ld.HistoricalDescriptions[i].Id;
                List<HistoricalProperty> hpList = database.HistoricalProperties.Where(x => x.HistoricalDescriptionId == id).ToList();
                ld.HistoricalDescriptions[i].HistoricalProperties = hpList;
            }
            return ld;
        }

        public List<HistoricalProperty> ReadHistoricalProperties()
        {
            //call logger
            lock (syncLock)
            {
                Logger.WriteLog("Reading historical properties", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            List<HistoricalProperty> list = database.HistoricalProperties.ToList();
            return list;
        }

        public void UpdateHistoricalDescriptions(HistoricalDescription hd, int dataset)
        {
            if (hd == null)
            {
                throw new ArgumentNullException("Arguments cannot be null");
            }
            if (dataset < 1 || dataset > 5)
            {
                throw new ArgumentException("Something wrong with dataset");
            }
            List<HistoricalProperty> historicalProperties = ReadHistoricalProperties();
            lock (syncLock)
            {
                Logger.WriteLog("Updating historical properties", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            foreach (HistoricalProperty hp in historicalProperties)
            {
                foreach (HistoricalProperty hpTemp in hd.HistoricalProperties)
                {
                    if (hp.HistoricalValue.GeographicalLocationId == hpTemp.HistoricalValue.GeographicalLocationId && hp.Code.Equals(hpTemp.Code))
                    {
                        hpTemp.Id = hp.Id;
                        if (CheckDeadband(hp, hpTemp))
                        {
                            hp.HistoricalValue = hpTemp.HistoricalValue;
                            break;
                        }
                    }
                }
            }
            database.SaveChanges();
        }

        public void RemoveHistoricalProperties(HistoricalDescription hd, int dataset)
        {
            List<HistoricalProperty> historicalProperties = ReadHistoricalProperties();

            if(hd == null)
            {
                throw new ArgumentNullException("Arguments cannot be null");
            }
            if(dataset < 1 || dataset > 5)
            {
                throw new ArgumentException("Something wrong with dataset");
            }

            lock (syncLock)
            {
                Logger.WriteLog("Removing historical properties", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            foreach (HistoricalProperty hpTemp in hd.HistoricalProperties)
            {
                foreach (HistoricalProperty hp in historicalProperties)
                {
                    if (hpTemp.HistoricalValue.GeographicalLocationId == hp.HistoricalValue.GeographicalLocationId && hp.Code.Equals(hpTemp.Code))
                    {
                        database.HistoricalProperties.Remove(hp);
                        break;
                    }
                }
            }
            database.SaveChanges();
        }

        public bool CheckDeadband(HistoricalProperty hp, HistoricalProperty hpTemp)
        {

            if (hp == null || hpTemp == null)
            {
                throw new ArgumentNullException("You need to have historical property!");
            }
            if (!hp.Code.Equals(hpTemp.Code))
            {
                throw new ArgumentException("The two codes arent a match!");
            }

            if (hpTemp.Code.Equals(Codes.CODE_DIGITAL) && hp.Code.Equals(Codes.CODE_DIGITAL))
            {
                return true;
            }

            if (hpTemp.HistoricalValue.Consumption < (hp.HistoricalValue.Consumption - (hp.HistoricalValue.Consumption * 0.02)) ||
                    hpTemp.HistoricalValue.Consumption > (hp.HistoricalValue.Consumption + (hp.HistoricalValue.Consumption * 0.02)))
            {
                return true;
            }
            return false;
        }

        public bool CheckGeoId(string id)
        {
            if(String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("Id cannot be null");
            }
            HistoricalProperty hp = database.HistoricalProperties.Where(x => x.HistoricalValue.GeographicalLocationId.Equals(id)).FirstOrDefault();
            if (hp != null)
                return false;
            return true;
        }
    }
}
