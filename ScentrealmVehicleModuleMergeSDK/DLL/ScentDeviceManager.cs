using SKII.Communication;
using SKII.Devices;
using SKII.Devices.Equips;
using SKII.OdorPlayer.ViewModels;
using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;

namespace SKII.OdorPlayer
{
    public class ScentDeviceManager
    {
        private static ScentDeviceManager instance = null;
        private static object singleLock = new object(); //锁同步

        private Device UsedDev = null;
        private bool connected = false;
        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns>返回单例对象</returns>
        public static ScentDeviceManager CreateInstance()
        {
            lock (singleLock)
            {
                if (instance == null)
                {
                    instance = new ScentDeviceManager();
                }
            }
            return instance;
        }

        public App app
        {
            get => Application.Current as App;
        }

        #region 属性
        /// <summary>
        /// 设备是否已连接
        /// </summary>
        public bool Connected
        {
            get
            {
                if(UsedDev != null)
                {
                    return UsedDev.Connected && connected;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                connected = value;
            }
        }
        #endregion End

        #region 操作

        public void Init()
        {
            foreach (BaseBus bus in PortsManager.GetInstance().Ports)
            {
                try
                {
                    bus.Init();
                }
                catch
                {
                }
            }
            Device usedDev = DeviceManager.GetInstance().GetDeviceById(AppManager.CreateInstance().UsedDevId);
            if (usedDev != null)
            {
                UsedDev = usedDev;
                UsedDev.Init();
                Task.Run(() =>
                {
                    string[] ports = SerialPort.GetPortNames();
                    int len = ports.Length;
                    while (len > 0)
                    {
                        if (UsedDev.AutoConnect())
                        {
                            Console.WriteLine("初始化成功！");
                            MainFrameViewModel.CreateInstance().ConnState = 1; //结构破坏
                            Connected = true;
                            return;
                        }
                        else
                        {
                            len--;
                        }
                    }
                });
            }
        }

        public void ReConnect()
        {
            Device usedDev = DeviceManager.GetInstance().GetDeviceById(AppManager.CreateInstance().UsedDevId);
            if (usedDev != null)
            {
                UsedDev = usedDev;
                UsedDev.Init();
                Task.Run(() =>
                {
                    string[] ports = SerialPort.GetPortNames();
                    int len = ports.Length;
                    int index = 0;
                    while (len > 0)
                    {
                        if (UsedDev.AutoConnect())
                        {
                            Console.WriteLine("初始化成功！");
                            MainFrameViewModel.CreateInstance().ConnState = 1; //结构破坏
                            Connected = true;
                            return;
                        }
                        else
                        {

                            //Console.WriteLine("当前串口：" + ports[index]);
                            len--;
                        }
                        index++;
                    }
                });
            }
        }

        /// <summary>
        /// 播放气味
        /// </summary>
        /// <param name="smell"></param>
        /// <param name="duration">秒</param>
        public void PlaySmell(int smellid, int duration)
        {
            if(UsedDev != null)
            {
                byte scentid = (byte)smellid;
                (UsedDev as SignalSynV103).PlaySmell(scentid, duration);
            }
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="channl"></param>
        public void StopSmell()
        {
            //SPController.G_StopSmell();
            if (UsedDev != null)
            {
                (UsedDev as SignalSynV103).StopPlay();
            }
        }

        public void Close()
        {
            if (UsedDev != null)
            {
                (UsedDev as SignalSynV103).Close();
            }
        }
        /// <summary>
        /// 唤醒
        /// </summary>
        public void WakeUp()
        {
            if (UsedDev != null)
            {
                (UsedDev as SignalSynV103).WakeUp();
            }
        }

        public void SetChannel(byte chlNum)
        {
            if (UsedDev != null)
            {
                (UsedDev as SignalSynV103).Channel = chlNum;
            }
        }

        #endregion End
    }
}
