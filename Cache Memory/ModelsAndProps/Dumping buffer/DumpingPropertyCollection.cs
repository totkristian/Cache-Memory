using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class DumpingPropertyCollection
    {
        private List<DumpingProperty> dumpingProperties = new List<DumpingProperty>(); 

        public List<DumpingProperty> DumpingProperties { get => dumpingProperties; set => dumpingProperties = value; }

        public DumpingPropertyCollection()
        {

        }
    }
}
