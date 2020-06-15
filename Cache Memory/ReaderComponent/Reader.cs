using HistoricalComponent;
using ModelsAndProps.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderComponent
{
    public class Reader
    {
        private Historical historical = Historical.GetInstance();
        private Codes code;
        private ListDescription listDescription = new ListDescription();

        public Codes Code { get => code; set => code = value; }

        public void Meni()
        {
            int number = 0;
            bool isOk = false;


            while (!isOk)
            {
                Console.WriteLine("Which one do you want?: \n");
                Console.WriteLine("1. CODE_ANALOG");
                Console.WriteLine("2. CODE_DIGITAL");
                Console.WriteLine("3. CODE_CUSTOM");
                Console.WriteLine("4. CODE_LIMITSET");
                Console.WriteLine("5. CODE_SINGLENODE");
                Console.WriteLine("6. CODE_MULTIPLENODE");
                Console.WriteLine("7. CODE_CONSUMER");
                Console.WriteLine("8. CODE_SOURCE");
                Console.WriteLine("9. CODE_MOTION");
                Console.WriteLine("10. CODE_SENSOR\n");
                try
                {
                    number = int.Parse(Console.ReadLine());
                    if (number >= 1 && number <= 10)
                    {
                        isOk = true;
                    }
                }
                catch
                {
                    Console.WriteLine("\nIt needs to be a number!\n");
                }

            }
            List<HistoricalProperty> hp = GetChangesForInterval((Codes)(number - 1));

            foreach (HistoricalProperty hps in hp)
            {
                Console.WriteLine(hps.ToString());
            }
        }

        public List<HistoricalProperty> GetChangesForInterval(Codes code)
        {
            //call logger
            int dataset = historical.CheckDataset(code);
            ListDescription listDescription = historical.ReadOneLDFromDB(dataset);

            return ReadCode(code, listDescription);
        }

        private List<HistoricalProperty> ReadCode(Codes code, ListDescription listDescription)
        {
            List<HistoricalProperty> hps = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in listDescription.HistoricalDescriptions)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (hp.Code.Equals(code))
                    {
                        hps.Add(hp);
                    }
                }
            }
            return hps;
        }

    }
}
