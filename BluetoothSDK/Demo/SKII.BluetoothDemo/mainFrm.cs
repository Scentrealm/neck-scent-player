using SKII.Models;
using SKII.ScentRealmSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Bluetooth;

namespace SKII.BluetoothDemo
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        List<ScentEquip> MyDevices = new List<ScentEquip>();

        List<BluetoothLEDevice> DeviceList = new List<BluetoothLEDevice>();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.btnSearch.Text == "搜索设备")
            {
                ScentRealmSDK.BlueToothAdapter.CreateInstance().StartScanBleDevice();
                this.btnSearch.Text = "停止搜索";
            }
            else
            {
                try
                {
                    ScentRealmSDK.BlueToothAdapter.CreateInstance().StopScanBleDevice();
                    this.btnSearch.Text = "搜索设备";
                }
                catch
                {
                }
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            BlueToothAdapter.CreateInstance().MessageRoutedHandle += MainFrm_MessageRoutedHandle;
            BlueToothAdapter.CreateInstance().DeviceDiscovedHandle += this.MainFrm_DeviceDiscovedHandle;
        }

        int devCnt = 0;
        private void MainFrm_DeviceDiscovedHandle(Windows.Devices.Bluetooth.BluetoothLEDevice bluetoothLEDevice)
        {
            this.Invoke((EventHandler)delegate
            {
                string devName = bluetoothLEDevice.Name;
                //if (devName.Length > 5 && devName.Substring(0, 4) == "wear")
                if (devName.Length > 5 && devName.Substring(0, 5) == "scent")//
                {
                    var dev = DeviceList.Find(x => x.DeviceId == bluetoothLEDevice.DeviceId);
                    if (dev == null)
                    {
                        ListViewItem viewItem = new ListViewItem();
                        viewItem.ImageIndex = 0;
                        viewItem.Text = bluetoothLEDevice.Name.Replace("scent", "S=");
                        viewItem.Tag = bluetoothLEDevice;
                        viewItem.UseItemStyleForSubItems = false;

                        lvDevices.Items.Add(viewItem);
                        devCnt++;
                        DeviceList.Add(bluetoothLEDevice);
                    }
                }
            });
        }

        private void MainFrm_MessageRoutedHandle(string message, string MsgType = "Info")
        {
            this.Invoke((EventHandler)delegate {
                if (MsgType == "Info")
                {
                    txtShow.Text += message;
                }
                else
                {
                    Console.WriteLine("Msg:" + message);
                }
            });
        }

        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
            if (lvDevices.SelectedItems.Count > 0)
            {
                BluetoothLEDevice device = lvDevices.SelectedItems[0].Tag as BluetoothLEDevice;

                if (device != null)
                {
                    string devName = device.Name;
                    var item = MyDevices.Find(x => x.DevId == devName);
                    if(item != null)
                    {
                        if(!item.Connected)
                        {
                            if(item.BleBus == null)
                            {
                                item.BleBus = new BluetoothBus(device);
                                item.OnMessageRoutedHandle += MainFrm_MessageRoutedHandle;
                            }
                            item.Connect();
                        }
                    }
                    else
                    {
                        ScentEquip equip = new ScentEquip();
                        equip.OnMessageRoutedHandle += MainFrm_MessageRoutedHandle;
                        equip.ConncetStateHandle += Equip_ConncetStateHandle;
                        equip.BleBus = new BluetoothBus(device);
                        equip.Init();
                        equip.Connect();

                        MyDevices.Add(equip);

                        ListViewItem viewItem = new ListViewItem();
                        viewItem.ImageIndex = 0;
                        viewItem.Text = device.Name.Replace("scent", "S=");
                        viewItem.Tag = device;
                        viewItem.UseItemStyleForSubItems = false;

                        lvLink.Items.Add(viewItem);
                    }
                }
                else
                {
                    MessageBox.Show("未检测到有效设备！");
                }
            }
            else
            {
                MessageBox.Show("请选择连接的设备蓝牙。");
            }
        }

        private void Equip_ConncetStateHandle(bool state)
        {
            Console.WriteLine("Link Changed:" + state);
        }

        private void btnBuildPlay_Click(object sender, EventArgs e)
        {
            int iChl = int.Parse(txtChannel.Text);
            int iTime = int.Parse(txtTime.Text);
            if (iChl < 1 || iChl > 255)
            {
                MessageBox.Show("气味通道编号大于0且不超过255！");
                return;
            }

            foreach(var dev in MyDevices)
            {
                dev.PlayScent(iChl, iTime);
            }
        }

        private void btnBuildStop_Click(object sender, EventArgs e)
        {
            foreach (var dev in MyDevices)
            {
                dev.Stop();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCmd.Text.Trim()))
            {
                return;
            }

            byte[] bCmd = HexStr2Bytes(txtCmd.Text.Trim());

            if (bCmd == null || bCmd.Length == 0)
            {
                return;
            }

            foreach(var dev in MyDevices)
            {
                dev.Write(bCmd);
            }
        }

        private byte[] HexStr2Bytes(string dataStr)
        {
            if (string.IsNullOrEmpty(dataStr))
            {
                return new byte[] { };
            }
            List<byte> lStr = new List<byte>();
            List<Byte> lData = new List<byte>();
            string[] sData1 = dataStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return StrToByte(sData1);
        }

        private byte[] StrToByte(string[] d1)
        {
            if (d1 == null || d1.Length == 0)
            {
                return new byte[] { };
            }

            List<byte> lDa = new List<byte>();

            foreach (string str in d1)
            {
                lDa.Add(byte.Parse(str, System.Globalization.NumberStyles.HexNumber));
            }

            return lDa.ToArray();
        }
    }
}
