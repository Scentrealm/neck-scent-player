using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllTestForCsharp
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if(Init() > -1)
            {
                MessageBox.Show("初始化成功！");
            }
        }

        public int Init()
        {
            int ret = -1;

            ret = SKII_AutoConnect();
            this.Log("AutoConnect = " + ret);
            //ret = Scentrealm_WakeUp(false);
            //this.Log("Scentrealm_WakeUp = " + ret);


            return ret;
        }

        public int PlaySmell(byte smell, int duration)
        {
            int ret = -1;
            ret = SKII_StartPlaySmell(smell, (UInt32)duration * 1000, 0);
            this.Log("PlaySmell smell = " + smell + " duration = " + duration + " ret = " + ret);
            return ret;
        }

        public int StopSmell()
        {
            int ret = -1;
            ret = SKII_StopPlaySmell();
            this.Log("StopPlaySmell = " + ret);
            return ret;
        }

        public int DisConnect()
        {
            int ret = -1;
            ret = SKII_DisConnect();
            this.Log("DisConnect = " + ret);
            return ret;
        }

        #region SKII_DLL
        [DllImport("scentrealm_bcc.dll",EntryPoint = "SKII_AutoConnect", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        static extern private int SKII_AutoConnect();

        [DllImport("scentrealm_bcc.dll",EntryPoint = "SKII_StartPlaySmell", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        static extern private int SKII_StartPlaySmell(byte SmellID, UInt32 DurationInMilliSecond, byte scriptId);

        [DllImport("scentrealm_bcc.dll",EntryPoint = "SKII_ReadUUID", CallingConvention = CallingConvention.StdCall)]
        static extern private int SKII_ReadUUID();

        [DllImport("scentrealm_bcc.dll",EntryPoint = "SKII_DisConnect", CallingConvention = CallingConvention.StdCall)]
        static extern private int SKII_DisConnect();

        [DllImport("scentrealm_bcc.dll",EntryPoint = "SKII_StopPlaySmell", CallingConvention = CallingConvention.StdCall)]
        static extern private int SKII_StopPlaySmell();

        void Log(string msg)
        {
            var msgsss = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + msg;

            Console.WriteLine(msgsss);
        }
        #endregion End

        private void btnClose_Click(object sender, EventArgs e)
        {
            DisConnect();
        }

        private void btnPlaySmell_Click(object sender, EventArgs e)
        {
            byte scentid = byte.Parse(txtSmellId.Text);
            uint scenttime = uint.Parse(txtDurationTime.Text) * 1000;
            SKII_StartPlaySmell(scentid, scenttime, 0);
        }

        private void btnStopPlay_Click(object sender, EventArgs e)
        {
            SKII_StopPlaySmell();
        }
    }
}
