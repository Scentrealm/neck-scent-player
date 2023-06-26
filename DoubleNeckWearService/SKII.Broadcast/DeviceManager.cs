using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKII.Broadcast
{
    public class DeviceManager
    {
        #region  初始化单例
        private static object syncObj = new object();
        private static DeviceManager instance = null;
        public static DeviceManager GetInstance()
        {
            lock (syncObj)
            {
                if (instance == null)
                {
                    // instance = Read();
                    if (instance == null)
                    {
                        instance = new DeviceManager();
                    }
                }
            }
            return instance;
        }

        DeviceManager()
        {

        }

        private string _DefaultFilePathString = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            lock (syncObj)
            {
                try
                {
                    string filepathstring = Path.Combine(_DefaultFilePathString, "Config") + "\\DeviceManager.json";
                    //为了断电等原因导致xml文件保存出错，文件损坏，采用先写副本再替换的方式。
                    using (TextWriter textWriter = File.CreateText(filepathstring))
                    {
                        string jsonsavestr = JsonConvert.SerializeObject(this);
                        textWriter.Write(jsonsavestr);
                        textWriter.Flush();
                    }
                    if (File.Exists(filepathstring))
                    {
                        FileInfo fi = new FileInfo(filepathstring);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        private static DeviceManager Read()
        {

            string filepathstring = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config") + "\\DeviceManager.json"; //Application.StartupPath
            if (filepathstring == "")
                return null;
            if (!File.Exists(filepathstring))
            {
                using (TextWriter textWriter = File.CreateText(filepathstring))
                {
                    textWriter.Write("{}");
                    textWriter.Flush();
                }
            }
            using (StreamReader sr = new StreamReader(filepathstring))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.TypeNameHandling = TypeNameHandling.Objects;//这一行就是设置Json.NET能够序列化接口或继承类的关键，将TypeNameHandling设置为All
                    //构建Json.net的读取流
                    JsonReader reader = new JsonTextReader(sr);
                    //对读取出的Json.net的reader流进行反序列化，并装载到模型中
                    DeviceManager instancetmp = serializer.Deserialize<DeviceManager>(reader);

                    return instancetmp;
                }
                catch
                {
                    return new DeviceManager();
                }
            }
        }

        public void Dispose()
        {
            lock (syncObj)
            {
                instance = null;
            }
        }

        #endregion End

        /// <summary>
        /// 脖戴设备
        /// </summary>
        public List<RemoteControl> NeckWearWireless = new List<RemoteControl>();


    }
}
