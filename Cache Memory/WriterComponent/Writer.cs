using DumpingBufferComponent;
using HistoricalComponent;
using ModelsAndProps;
using ModelsAndProps.Historical;
using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WriterComponent
{
    public class Writer : IWriter
    {
        private Historical historical = Historical.GetInstance();
        private DumpingBuffer dumpingBuffer = DumpingBuffer.GetInstance();
        private RandomGenerator generator = new RandomGenerator();
        public Writer()
        {

        }


        private int Meni()
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
            return number - 1;
        }

        public void SendToHistorical()
        {
            bool isOk = false;
            Codes code = (Codes)Meni();
            string geographicalLocationId = "";
            float consumption = 0;
            while (!isOk)
            {
                try
                {
                    Console.WriteLine("Input stared...\n");
                    Console.WriteLine("Enter the geographical location id:");
                    geographicalLocationId = Console.ReadLine();
                    //unique
                    Console.WriteLine("Enter the consumption:");
                    consumption = float.Parse(Console.ReadLine());
                    isOk = true;
                    //callLogger
                }
                catch
                {
                    //callLogger
                    Console.WriteLine("Something went wrong with your input data. Please try again!");
                    isOk = false;
                }
            }
            Console.WriteLine("Sending to historical component...");
            historical.ManualWriteToHistory(code, new Value
            {
                Timestamp = DateTime.Now,
                GeographicalLocationId = geographicalLocationId,
                Consumption = consumption

            });
        }

        public void SendToDumpingBuffer()
        {
            //call logger
            Operations op = generator.GenerateRandomOperation();
            switch (op)
            {
                case Operations.ADD:
                    dumpingBuffer.WriteToDumpingBuffer(op,generator.GenerateRandomCode(), generator.RandomNewValueGenerator());
                    break;
                case Operations.UPDATE:
                    HistoricalProperty hp = GetRandomHistoricalProperty();
                    if (hp == null)
                        break;
                        
                        Value v = generator.RandomNewValueGenerator();
                        v.GeographicalLocationId = hp.HistoricalValue.GeographicalLocationId;
                    
                        dumpingBuffer.WriteToDumpingBuffer(op, hp.Code, v);
                    break;
                case Operations.REMOVE:
                    HistoricalProperty hp1 = GetRandomHistoricalProperty();
                    if (hp1 == null)
                        break;
                    dumpingBuffer.WriteToDumpingBuffer(op,hp1.Code, hp1.HistoricalValue);
                    //search through existing properties and remove a property
                    break;
            }

            Thread.Sleep(2000);
        }

        public HistoricalProperty GetRandomHistoricalProperty()
        {
           return  generator.getRandomPropertyForUpdateOrRemove(historical.GetHistoricalProperties());

        }

        
        
    }
}
