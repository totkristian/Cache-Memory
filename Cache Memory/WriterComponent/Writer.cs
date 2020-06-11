﻿using DumpingBufferComponent;
using HistoricalComponent;
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
    public class Writer
    {
        private Historical historical = Historical.GetInstance();
        private DumpingBuffer dumpingBuffer = DumpingBuffer.GetInstance();
        public Writer()
        {
           
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
            
            dumpingBuffer.WriteToDumpingBuffer(GenerateRandomCode(), GenerateRandomValue());
            Thread.Sleep(2000);
        }

        public Codes GenerateRandomCode()
        {
             Random random = new Random();
             return (Codes)random.Next(0, 10);
        }
        public Value GenerateRandomValue()
        {
            Random random = new Random();
            Value val = new Value();
            val.Consumption =(float)random.NextDouble() * 10;
            val.GeographicalLocationId = Guid.NewGuid().ToString();
            val.Timestamp = DateTime.Now;
            return val;
        }
    }
}
