using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SKII.Broadcast
{
    public class RemoteControl1
    {

        public RemoteControl1()
        {
            
        }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DevId
        {
            get;
            set;
        }
        /// <summary>
        /// 通道
        /// </summary>
        public byte Channel
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Connceted
        {
            get;
            set;
        }
        /// <summary>
        /// 唤醒
        /// </summary>
        public void WakeUp()
        {
            Scentrealm_WakeUp(true);
        }

        public void AutoConnect()
        {
            int rtnNum = Scentrealm_AutoConnectCTL();
            if(rtnNum > -1)
            {
                Connceted = true;
            }
            else
            {
                Connceted = false;
            }
        }

        public void PlaySmell(int chl, uint duration)
        {
            Scentrealm_PlaySmell((byte)chl, duration * 1000, Channel);
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopPlay()
        {
            Scentrealm_StopPlaySmell(Channel);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            Scentrealm_DisConnect();
        }

        public int GetControllerChannel()
        {
            try
            {
                int rtnNum = Scentrealm_GetControllerPC();
                return rtnNum;
            }
            catch
            { }
            return -1;
        }

        #region Basic APIs

        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_WakeUp(bool sync);

        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern int Scentrealm_AutoConnectCTL();

        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern int Scentrealm_PlaySmell(byte SmellID, uint DurationInMilliSecond, byte Channel);

        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_DisConnect();

        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_StopPlaySmell(byte Channel);


        [DllImport("scentrealm_bcc1.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_GetControllerPC();

        #endregion Basic APIs

    }
}
