using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponent
{
    public class Historical
    {
        private static Historical instance;
        private static object syncLock = new object();

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


    }
}
