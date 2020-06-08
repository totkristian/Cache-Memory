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
        public Writer()
        {
            Console.WriteLine("CASDASDASDASD");
        }
        public void ReadConsole(string asd)
        {
            Debug.WriteLine("Read console executed" + asd);
            
        }

        public int Meni()
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
            

            switch (number)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 3;
                case 5:
                    return 4;
                case 6:
                    return 5;
                case 7:
                    return 6;
                case 8:
                    return 7;
                case 9:
                    return 9;
                case 10:
                    return 10;

                default:
                    Console.WriteLine("Error\n");
                    return -1;
            }
        }
    }
}
