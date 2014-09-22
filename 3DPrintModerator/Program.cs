using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.Runtime.InteropServices;

namespace _3DPrintModerator
{
    class Program
    {
        

        static void Main(string[] args)
        {

            PrintSpoolerAPI.Structures.PRINTER_INFO_2[] Orinters = PrintSpoolerAPI.Functions.Printer.enumPrinters((PrintSpoolerAPI.Enumerations.PrinterEnumFlags.PRINTER_ENUM_LOCAL | PrintSpoolerAPI.Enumerations.PrinterEnumFlags.PRINTER_ENUM_CATEGORY_3D));
            Console.WriteLine("Printers: " + Orinters.Length);
            foreach (PrintSpoolerAPI.Structures.PRINTER_INFO_2 Printer in Orinters)
            {
                Console.WriteLine(Printer.pPrinterName);
                Console.WriteLine(Printer.pPortName);
                if ((Printer.Status & (uint)PrintSpoolerAPI.Enumerations.PrinterStatusCode.PRINTER_STATUS_OFFLINE) == (uint)PrintSpoolerAPI.Enumerations.PrinterStatusCode.PRINTER_STATUS_OFFLINE)
                Console.WriteLine(Printer.Status);
            }


            Console.ReadLine();
        }

    }
}
