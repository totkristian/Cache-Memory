using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class DumpingPropertyCollection
    {
        private HashSet<DumpingProperty> dumpingProperties = new HashSet<DumpingProperty>(); //hash koristim da ne bi imao duplikate nigde

        public HashSet<DumpingProperty> DumpingProperties { get => dumpingProperties; set => dumpingProperties = value; }

        public DumpingPropertyCollection()
        {

        }
    }
}
