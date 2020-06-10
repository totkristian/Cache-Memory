using HistoricalComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpingBufferComponent
{
    public class DumpingBuffer
    {
        private static DumpingBuffer instance;
        private static object syncLock = new object();
        private static Historical historical = Historical.GetInstance();
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
                }
            }

            return instance;
        }


    }
}
