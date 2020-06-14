using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps
{
    public interface IWriter
    {
        void SendToDumpingBuffer();
        void SendToHistorical();
    }
}
