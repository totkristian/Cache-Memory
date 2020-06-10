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

            foreach(HistoricalProperty hps in hp)
            {
                Console.WriteLine(hps.ToString());
            }
        }

        public List<HistoricalProperty> GetChangesForInterval(Codes code)
        {
            //treba kada pozivamo ovu metodu da samo ispisujemo hp.ToString();
            switch (code)
            {
                case Codes.CODE_ANALOG:
                    listDescription = historical.ReadOneLDFromDB(1);
                    return ReadCodeAnalog(code);

                case Codes.CODE_DIGITAL:
                    listDescription = historical.ReadOneLDFromDB(1);
                    return ReadCodeDigital(code);

                case Codes.CODE_CONSUMER:
                    listDescription = historical.ReadOneLDFromDB(4);
                    return ReadCodeConsumer(code);

                case Codes.CODE_CUSTOM:
                    listDescription = historical.ReadOneLDFromDB(2);
                    return ReadCodeCustom(code);

                case Codes.CODE_LIMITSET:
                    listDescription = historical.ReadOneLDFromDB(2);
                    return ReadCodeLimitset(code);

                case Codes.CODE_MOTION:
                    listDescription = historical.ReadOneLDFromDB(5);
                    return ReadCodeMotion(code);

                case Codes.CODE_MULTIPLENODE:
                    listDescription = historical.ReadOneLDFromDB(3);
                    return ReadCodeMultiplenode(code);

                case Codes.CODE_SENSOR:
                    listDescription = historical.ReadOneLDFromDB(5);
                    return ReadCodeSensor(code);

                case Codes.CODE_SINGLENODE:
                    listDescription = historical.ReadOneLDFromDB(3);
                    return ReadCodeSinglenode(code);

                case Codes.CODE_SOURCE:
                    listDescription = historical.ReadOneLDFromDB(4);
                    return ReadCodeSource(code);

                default:
                    return null;
            }
        }

        private List<HistoricalProperty> ReadCodeSource(Codes code)
        {
            List<HistoricalProperty> hps = new List<HistoricalProperty>();
            foreach (HistoricalDescription hd in listDescription.HistoricalDescriptions)
            {
                foreach(HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (hp.Code.Equals(code))
                    {
                        hps.Add(hp);
                    }
                }
            }
            return hps;
        }

        private List<HistoricalProperty> ReadCodeSinglenode(Codes code)
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

        private List<HistoricalProperty> ReadCodeSensor(Codes code)
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

        private List<HistoricalProperty> ReadCodeMultiplenode(Codes code)
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

        private List<HistoricalProperty> ReadCodeMotion(Codes code)
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

        private List<HistoricalProperty> ReadCodeLimitset(Codes code)
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

        private List<HistoricalProperty> ReadCodeCustom(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeConsumer(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeDigital(Codes code)
        {
            throw new NotImplementedException();
        }

        private List<HistoricalProperty> ReadCodeAnalog(Codes code)
        {
            throw new NotImplementedException();
        }
    }
}
