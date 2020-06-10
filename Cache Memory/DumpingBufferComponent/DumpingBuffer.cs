using HistoricalComponent;
using ModelsAndProps;
using ModelsAndProps.Dumping_buffer;
using ModelsAndProps.ValueStructure;
using System;
using System.CodeDom;
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

        public void WriteToDumpingBuffer(Codes code, Value val)
        {
            if ((int)code < 0 || (int)code > 9)
                throw new ArgumentException("Code must be in interval 0-9!");
            //check Val.Consumption and the resto of the properties if they are valid
            DumpingProperty dp = new DumpingProperty(code, val);
          //  int dataset = checkDataset()
        }

    }
}
