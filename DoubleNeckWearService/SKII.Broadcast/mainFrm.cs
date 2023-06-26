using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = SimpleTCP.Message;

namespace SKII.Broadcast
{
    public partial class mainFrm : Form
    {
        private SimpleTcpServer server;
        private string HostStr = string.Empty;
        private string PortStr = string.Empty;

        RemoteControl1 rc1Neck;

        RemoteControl rcNeck;

        public mainFrm()
        {
            InitializeComponent();
        }

        private void tsmServer_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void tsmQuit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定退出服务？","提示",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            DeviceManager.GetInstance().NeckWearWireless.Clear();

            //RemoteControl remote = new RemoteControl();
            //remote.DevId = "dev1";
            //DeviceManager.GetInstance().NeckWearWireless.Add(remote);

            rc1Neck = new RemoteControl1();
            rc1Neck.DevId = "dev0";
            cboDev.Items.Add("dev0");
            rc1Neck.AutoConnect();
            Thread.Sleep(1000);

            rcNeck = new RemoteControl();
            rcNeck.DevId = "dev1";
            cboDev.Items.Add("dev1");
            rcNeck.AutoConnect();
            Thread.Sleep(1000);

            this.server = new SimpleTcpServer();
            this.server.Delimiter = 19;
            this.server.StringEncoder = Encoding.UTF8;
            this.server.DataReceived += new EventHandler<Message>(this.Server_DataReceived);

            IniFile ini = new IniFile(Application.StartupPath + "\\config.ini");
            string hostStr = ini.ReadContentValue("Setting", "IPAddr").ToString();
            if(string.IsNullOrEmpty(hostStr))
            {
                txtHost.Text = "127.0.0.1";
                HostStr = "127.0.0.1";
            }
            else
            {
                txtHost.Text = hostStr;
                HostStr = hostStr;
            }
            string portStr = ini.ReadContentValue("Setting", "ServerPort").ToString();
            if (string.IsNullOrEmpty(portStr))
            {
                txtPort.Text = "10086";
                PortStr = "10086";
            }
            else
            {
                txtPort.Text = portStr;
                PortStr = portStr;
            }
            string autoStr = ini.ReadContentValue("Setting", "AutoStart").ToString();
            if(autoStr == "1")
            {
                btnServer_Click(null, null);
            }
            this.WindowState = FormWindowState.Minimized;

            try
            {
                if(rc1Neck.Connceted)
                {
                    txtStatus.Text += rc1Neck.DevId + "连接成功\r\n";
                    int rtnNum = rc1Neck.GetControllerChannel();
                    txtStatus.Text += rc1Neck.DevId + "物理通道："+ rtnNum.ToString() + "\r\n";
                }
                else
                {
                    txtStatus.Text += rc1Neck.DevId + "连接失败\r\n";
                }

                if (rcNeck.Connceted)
                {
                    txtStatus.Text += rcNeck.DevId + "连接成功\r\n";
                    int rtnNum1 = rcNeck.GetControllerChannel();
                    txtStatus.Text += rcNeck.DevId + "物理通道：" + rtnNum1.ToString() + "\r\n";
                }
                else
                {
                    txtStatus.Text += rcNeck.DevId + "连接失败\r\n";
                }
            }
            catch
            { }
        }

        // ScentRealm.Form1
        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.txtStatus.Text += e.MessageString + "\n\r";
                if (e.Data != null)
                {
                    string messageString = e.MessageString;
                    if (messageString.Contains(":"))
                    {
                        string[] array = messageString.Split(new char[]{ ':'});
                        int num = mainFrm.Scentrealm_PlaySmell((byte)int.Parse(array[0]), (uint)(int.Parse(array[1]) * 1000), 0);
                    }
                }
                e.ReplyLine(string.Format("You reply: {0}", e.MessageString));
            }));
        }

        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.server?.Stop();
            }
            catch
            { }

            try
            {
                foreach (var item in DeviceManager.GetInstance().NeckWearWireless)
                {
                    item.Close();
                }
            }
            catch
            { }
        }

        #region Basic APIs

        [DllImport("scentrealm_bcc.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_WakeUp(bool sync);

        [DllImport("scentrealm_bcc.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        private static extern int Scentrealm_AutoConnectCTL();

        [DllImport("scentrealm_bcc.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern int Scentrealm_PlaySmell(byte SmellID, uint DurationInMilliSecond, byte Channel);

        [DllImport("scentrealm_bcc.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_DisConnect();

        [DllImport("scentrealm_bcc.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int Scentrealm_StopPlaySmell(byte Channel);

        #endregion Basic APIs

        #region Script APIs

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptLoad(string file);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptRun(int start_position_mse);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptRunScript(byte[] file, int start_position_ms);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptChangePosition(int start_position_ms);

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptPause();

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptContinue();

        [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
        static extern private int Scentrealm_ScriptStop();
        #endregion Script APIs

        private void btnAutoWakeUp_Click(object sender, EventArgs e)
        {
            try
            {
                int iDev = cboDev.SelectedIndex;
                if (iDev < 0)
                {
                    return;
                }

                try
                {
                    int chl = int.Parse(txtChl.Text);
                    uint dura = uint.Parse(txtDuration.Text);
                    if(iDev == 0)
                    {
                        rcNeck.WakeUp();
                    }
                    else
                    {
                        rc1Neck.WakeUp();
                    }
                }
                catch
                { }
            }
            catch
            { }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnServer.Text == "Start")
                {
                    txtStatus.Text += "Server starting...\r\n";
                    IPAddress ipAddress = IPAddress.Parse(this.txtHost.Text);
                    this.server.Start(ipAddress, Convert.ToInt32(this.txtPort.Text));
                    int rtnNum = mainFrm.Scentrealm_AutoConnectCTL();
                    if (rtnNum > -1)
                    {
                        txtStatus.Text += "ScentRealm Device started successfully...\r\n";
                        btnServer.Text = "Stop";
                    }
                    else
                    {
                        txtStatus.Text += "ScentRealm Device connection failed ...\r\n";
                    }
                }
                else
                {
                    if (this.server.IsStarted)
                    {
                        this.server.Stop();
                        txtStatus.Text += "Server Stopped...\r\n";
                        int rtnNum = mainFrm.Scentrealm_StopPlaySmell(0);
                        if (rtnNum > -1)
                        {
                            txtStatus.Text += "ScentRealm Device disconnected successfully...\r\n";
                        }
                        else
                        {
                            txtStatus.Text += "ScentRealm Device disconnection failed ...\r\n";
                        }
                    }
                    btnServer.Text = "Start";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("发生错误，请工作人员协助：" + ex.Message);
            }
        }
        Random rd = new Random();
        private void btnTest_Click(object sender, EventArgs e)
        {
            //int chl = rd.Next(1, 12);
            //int num = mainFrm.Scentrealm_PlaySmell((byte)chl, 3000, 0);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            int iDev = cboDev.SelectedIndex;
            if (iDev < 0)
            {
                return;
            }

            try
            {
                int chl = int.Parse(txtChl.Text);
                uint dura = uint.Parse(txtDuration.Text);
                if (iDev == 0)
                {
                    rcNeck.PlaySmell(chl, dura);
                }
                else
                {
                    rc1Neck.PlaySmell(chl, dura);
                }
            }
            catch
            { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            int iDev = cboDev.SelectedIndex;
            if (iDev < 0)
            {
                return;
            }

            try
            {
                if (iDev == 0)
                {
                    rcNeck.StopPlay();
                }
                else
                {
                    rc1Neck.StopPlay();
                }
            }
            catch
            { }
        }
    }
}
