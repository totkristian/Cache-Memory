using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class DumpingPropertyCollection
    {
        private List<DumpingProperty> dumpingProperties = new List<DumpingProperty>(); //hash koristim da ne bi imao duplikate nigde

        public List<DumpingProperty> DumpingProperties { get => dumpingProperties; set => dumpingProperties = value; }

        public DumpingPropertyCollection()
        {

        }
    }
}
