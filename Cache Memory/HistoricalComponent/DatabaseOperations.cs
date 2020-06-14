using HistoricalComponent.DatabaseConn;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalComponent
{
    public class DatabaseOperations
    {
        private Database database = new Database();

        public void AddHistoricalDescription(HistoricalDescription hd, int dataset)
        {
            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }
    }
}
