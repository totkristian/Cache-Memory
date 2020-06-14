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
            //call logger
            ListDescription list1 = database.ListDescriptions.Where(x => x.Id == dataset).FirstOrDefault();
            list1.HistoricalDescriptions.Add(hd);
            database.SaveChanges();
        }

        public ListDescription ReadListDescription(int dataset)
        {
            ListDescription ld = new ListDescription();
            List<HistoricalDescription> list = database.HistoricalDescriptions.Where(x => x.ListDescriptionId == dataset).ToList();
            ld.HistoricalDescriptions = list;

            for (int i = 0; i < ld.HistoricalDescriptions.Count; i++)
            {
                int id = ld.HistoricalDescriptions[i].Id;
                List<HistoricalProperty> hpList = database.HistoricalProperties.Where(x => x.HistoricalDescriptionId == id).ToList();
                ld.HistoricalDescriptions[i].HistoricalProperties = hpList;
            }

            return ld;
        }
    }
}
