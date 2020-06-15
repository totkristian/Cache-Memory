using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterComponent;

namespace Cache_Memory
{
    public class Implementation
    {
        Writer w = new Writer();
        Reader r = new Reader();
        public void MenuChoise()
        {
            Console.WriteLine("1. Write data automatically (dumpingBuffer)");
            Console.WriteLine("2. Write data manually (direct to historical)");
            Console.WriteLine("3. Read data");
            Console.WriteLine("4. Exit app");
        }
        public int MenuChoiseInput()
        {
            int number = 0;
            bool isOk = false;
            while (!isOk)
            {
                MenuChoise();
                try
                {
                    number = int.Parse(Console.ReadLine());
                    if (number >= 1 && number <= 4)
                    {
                        isOk = true;
                    }
                    else
                    {
                        Console.WriteLine("Choise does not exist!");
                    }
                }
                catch
                {
                    Console.WriteLine("\nIt needs to be a number!\n");
                }
            }
            return number;
        }


        public void ReadMenu()
        {
            while (true)
            {
                switch (MenuChoiseInput())
                {
                    case 1:
                        //dumping buffer
                        SendToDumpingBuffer();
                        break;
                    case 2:
                        //manualWrite
                        ManualWriteToHistory();
                        break;
                    case 3:
                        //read
                        ReadData();
                        break;
                    case 4:
                        return;
                }
            }
        }

        public void ManualWriteToHistory()
        {
            ConsoleKeyInfo cki;
            do
            {
                w.SendToHistorical();
                Console.Write("\nPress 'Escape' to exit reader");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
        }

        public void ReadData()
        {
            ConsoleKeyInfo cki;
            do
            {
                r.Meni();
                Console.Write("\nPress 'Escape' to exit reader");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
        }

        public void SendToDumpingBuffer()
        {
            Console.Write("\nPress any key to exit dumping buffer");
            while (!Console.KeyAvailable)
            {
                w.SendToDumpingBuffer();
            }
        }











    }
}
