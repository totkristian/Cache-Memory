using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Dumping_buffer
{
    public class CollectionDescription
    {
        private int id;
        private int dataset;
        private DumpingPropertyCollection dumpingPropertyCollection = new DumpingPropertyCollection();

        public int Id { get => id; set => id = value; }
        public int Dataset { get => dataset; set => dataset = value; }
        public DumpingPropertyCollection DumpingPropertyCollection { get => dumpingPropertyCollection; set => dumpingPropertyCollection = value; }

        public CollectionDescription()
        {

        }
        public CollectionDescription(int dataset)
        {
            if (dataset < 1 || dataset > 5)
                throw new ArgumentException("Dataset must be in interval from 1-5!");
            this.dataset = dataset;
        }

    }
}
