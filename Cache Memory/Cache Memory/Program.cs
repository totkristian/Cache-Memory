﻿using ReaderComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriterComponent;

namespace Cache_Memory
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader r = new Reader();

            r.Meni();

            Console.ReadLine();
        }
    }
}
