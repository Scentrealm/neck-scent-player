using System;
using System.IO;
using System.IO.Ports;
using System.Xml;

namespace SKII.PartyCelebrate
{
    public class PortManager
    {
        private static PortManager instance = null;
        private static object singleLock = new object();

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static PortManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new PortManager();
                }
            }
            return instance;
        }

        private ScentrealmBCC.SPController SPController = ScentrealmBCC.SPController.getInstance();

        private bool DevConnected = false;
        /// <summary>
        /// 设备连接
        /// </summary>
        public void DevLink()
        {
            SPController.VMType = ScentrealmBCC.VehicleMountedType.MultiWithMainBoard;
            SPController.ConnectVM((Boolean R) => {

                if (R)
                {
                    //UISync.Execute(() => {
                    //    tsmi.Text = "已连接";// + SP.PortName + ">";
                    //    tsmi.Enabled = true;

                    //});
                    DevConnected = true;
                }
                else
                {

                    //UISync.Execute(() => {
                    //    tsmi.Text = ButtonText[0];
                    //    tsmi.Enabled = true;
                    //});
                    DevConnected = false;
                }
            });
        }
        /// <summary>
        /// 播放气味
        /// </summary>
        /// <param name="smellid"></param>
        /// <param name="durationtime"></param>
        /// <returns></returns>
         public bool PlaySmell(int smellid, int durationtime)
         {
            try
            {
                if (SPController.IsVMConnected)
                {
                    int Duration = durationtime;
                    OnlyPlaySmell_AD(smellid, Duration);
                }
            }
            catch
            {
                return false;
            }
            return true;
         }
        ushort VM_ModuleID { get; set; } = 1; //
        private void OnlyPlaySmell_AD(Int32 SmellID, Int32 Duration)
        {
            if (SmellID == 0)
                return;
            if (Duration == 0)
                return;
            //Tools.TLog(String.Format("PlaySmell S={0} D={1}", SmellID, Duration));

            if (SPController.IsVMConnected)
            {
                VM_ModuleID = (UInt16)Math.Ceiling((SmellID / 4.0));
                UInt16 VM_SmellID = (UInt16)SmellID; //(Byte)(SmellID - (VM_ModuleID - 1) * 4);

                SPController.VehicleMounted_Play_Addr(VM_ModuleID, VM_SmellID, (uint)Duration * 1000);
            }
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <returns></returns>
        public bool StopPlaySmell()
        {
            try
            {
                if (SPController.IsVMConnected)
                {
                    SPController.VehicleMounted_Stop_Addr(VM_ModuleID);
                }
                else
                {
                    //未连接设备无法
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        #region 
        public void Init()
        {
            DevLink();
            //return;
        }

        public void DisConnect()
        {
            if (SPController.IsVMConnected)
            {
                SPController.Disconnect();
            }
        }

        #endregion End
    }
}
