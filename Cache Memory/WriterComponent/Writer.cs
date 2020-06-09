using HistoricalComponent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WriterComponent
{
    public class Writer
    {
        private Historical historical = Historical.GetInstance();
        public Writer()
        {
           
        }
       

        public int Menu()
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
                    if(number >= 1 && number <= 10)
                    {
                        isOk = true;
                    }
                }
                catch
                {
                    Console.WriteLine("\nIt needs to be a number!\n");
                }

            }
            return number - 1;
        }

        public void SendToHistorical()
        {

        }
    }
}
