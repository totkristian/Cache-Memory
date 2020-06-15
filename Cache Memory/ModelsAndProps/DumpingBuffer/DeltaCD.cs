using System;
using System.Collections.Generic;

namespace ModelsAndProps.Dumping_buffer
{
    public class DeltaCD
    {
        private string transactionID;
        private Dictionary<int, CollectionDescription> add;
        private Dictionary<int, CollectionDescription> update;
        private Dictionary<int, CollectionDescription> remove;

        public string TransactionID { get => transactionID; set => transactionID = value; }
        public Dictionary<int, CollectionDescription> Add { get => add; set => add = value; }
        public Dictionary<int, CollectionDescription> Update { get => update; set => update = value; }
        public Dictionary<int, CollectionDescription> Remove { get => remove; set => remove = value; }

        public DeltaCD()
        {
            TransactionID = Guid.NewGuid().ToString();
            add = new Dictionary<int, CollectionDescription>();
            update = new Dictionary<int, CollectionDescription>();
            remove = new Dictionary<int, CollectionDescription>();
            for (int i = 1; i < 6; i++)
            {
                add.Add(i, new CollectionDescription());
                update.Add(i, new CollectionDescription());
                remove.Add(i, new CollectionDescription());
            }
        }
    }
}
