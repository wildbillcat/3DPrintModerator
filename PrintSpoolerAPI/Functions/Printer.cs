using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PrintSpoolerAPI.Structures;
using PrintSpoolerAPI.Enumerations;


namespace PrintSpoolerAPI.Functions
{
    public static class Printer
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumPrinters(PrinterEnumFlags Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);

        private const int ERROR_INSUFFICIENT_BUFFER = 122;

        public static PRINTER_INFO_2[] enumPrinters(PrinterEnumFlags Flags)
        {
            uint cbNeeded = 0;
            uint cReturned = 0;
            if (EnumPrinters(Flags, null, 2, IntPtr.Zero, 0, ref cbNeeded, ref cReturned))
            {
                return null;
            }
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error == ERROR_INSUFFICIENT_BUFFER)
            {
                IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                if (EnumPrinters(Flags, null, 2, pAddr, cbNeeded, ref cbNeeded, ref cReturned))
                {
                    PRINTER_INFO_2[] printerInfo2 = new PRINTER_INFO_2[cReturned];
                    int offset = pAddr.ToInt32();
                    Type type = typeof(PRINTER_INFO_2);
                    int increment = Marshal.SizeOf(type);
                    for (int i = 0; i < cReturned; i++)
                    {
                        printerInfo2[i] = (PRINTER_INFO_2)Marshal.PtrToStructure(new IntPtr(offset), type);
                        offset += increment;
                    }
                    Marshal.FreeHGlobal(pAddr);
                    return printerInfo2;
                }
                lastWin32Error = Marshal.GetLastWin32Error();
            }
            throw new Exception();
        }

    }
}
