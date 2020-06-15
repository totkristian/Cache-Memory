﻿using HistoricalComponent.DatabaseConn;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponent
{
    public class DatabaseOperations
    {
        private Database database = new Database();

        public void AddHistoricalDescription(HistoricalDescription hd, int dataset)
        {
            //call logger
            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }

        public ListDescription ReadListDescription(int dataset)
        {
            //call logger
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

        public List<HistoricalProperty> ReadHistoricalProperties()
        {
            //call logger
            List<HistoricalProperty> list = database.HistoricalProperties.ToList();
            return list;
        }

        public void UpdateHistoricalDescriptions(HistoricalDescription hd, int dataset)
        {
            List<HistoricalProperty> historicalProperties = ReadHistoricalProperties();
            foreach (HistoricalProperty hp in historicalProperties)
            {
                foreach (HistoricalProperty hpTemp in hd.HistoricalProperties)
                {
                    if(hp.HistoricalValue.GeographicalLocationId == hpTemp.HistoricalValue.GeographicalLocationId && hp.Code.Equals(hpTemp.Code))
                    {
                        hpTemp.Id = hp.Id;
                        if(CheckDeadband(hp, hpTemp))
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
            foreach(HistoricalProperty hpTemp in hd.HistoricalProperties)
            {
                foreach(HistoricalProperty hp in historicalProperties)
                {
                    if(hpTemp.HistoricalValue.GeographicalLocationId == hp.HistoricalValue.GeographicalLocationId && hp.Code.Equals(hpTemp.Code))
                    {
                        database.HistoricalProperties.Remove(hp);
                        break;
                    }
                }
            }
            database.SaveChanges();
        }

        private bool CheckDeadband(HistoricalProperty hp, HistoricalProperty hpTemp)
        {
            if (hp == null || hpTemp == null)
            {
                throw new ArgumentNullException("You need to have historical property!");
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
            HistoricalProperty hp = database.HistoricalProperties.Where(x => x.HistoricalValue.GeographicalLocationId.Equals(id)).FirstOrDefault();
            if (hp != null)
                return false;
            return true;
        }
    }
}