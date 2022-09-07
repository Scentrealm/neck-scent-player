using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void VoidNoParamsDelegate();
        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]
        public delegate void VoidIntIntDelegate(int power,int status);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ManualConnectCTL(byte COMID);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_QueryDevState(UInt32 DeviceID, VoidIntIntDelegate Callback, VoidNoParamsDelegate TimeoutCallback, bool sync);


        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_WakeUp(bool sync);


        static void Main(string[] args)
        {
            Scentrealm_ManualConnectCTL(5);

            Scentrealm_WakeUp(true);

            Scentrealm_QueryDevState(7, (power, status) => {
                Console.WriteLine("p=" + power + ",s=" + status);
            }, () => {
                Console.WriteLine("Error");
            },true);

            Console.ReadKey();
        }
    }
}
