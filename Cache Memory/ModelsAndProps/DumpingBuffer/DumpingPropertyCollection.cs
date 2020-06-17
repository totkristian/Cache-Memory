using System.Collections.Generic;

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
