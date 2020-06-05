﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache_Memory;
using Cache_Memory.Database;
using ModelsAndProps.Historical;
using LoggerComponent;

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static object syncLock = new object();
        private Database database = new Database();
        private int dataset;
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
                }
            }

            return instance;
        }

        public void AddToDatabase(ListDescription lista)
        {
            try
            {
                database.ListDescriptions.Add(lista);
                database.SaveChanges();
                Logger.WriteLog("Successfully added to database", "Historical", "AddToDatabase");
            }
            catch(Exception ex)
            {
                Logger.WriteLog(ex.Message, "Historical", "AddToDatabase");
            }
           
        }

       
        public bool CheckDataset(Codes code)
        {
            if ((int)code < 0 || (int)code > 9)
                throw new ArgumentException("Code must be in interval 0-9!");
           switch(code)
            {
                case Codes.CODE_ANALOG:
                case Codes.CODE_DIGITAL:
                    dataset = 1;
                    return true;
                case Codes.CODE_CUSTOM:
                case Codes.CODE_LIMITSET:
                    dataset = 2;
                    return true;
                case Codes.CODE_SINGLENOE:
                case Codes.CODE_MULTIPLENODE:
                    dataset = 3;
                    return true;
                case Codes.CODE_CONSUMER:
                case Codes.CODE_SOURCE:
                    dataset = 4;
                    return true;
                case Codes.CODE_MOTION:
                case Codes.CODE_SENSOR:
                    dataset = 5;
                    return true;
                default:
                    dataset = -1;
                    return false;
            }

        }

        

    }
}
