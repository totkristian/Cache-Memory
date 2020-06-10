using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class CollectionDescription
    {
        private string id;
        private int dataset;
        private DumpingPropertyCollection dumpingPropertyCollection = new DumpingPropertyCollection();

        public string Id { get => id; set => id = value; }
        public int Dataset { get => dataset; set => dataset = value; }
        public DumpingPropertyCollection DumpingPropertyCollection { get => dumpingPropertyCollection; set => dumpingPropertyCollection = value; }

        public CollectionDescription()
        {

        }
        public CollectionDescription(string id, int dataset)
        {
            this.id = id;
            this.dataset = dataset;
        }

    }
}
