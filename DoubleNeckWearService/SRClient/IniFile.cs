using System.Runtime.InteropServices;
using System.Text;

namespace SRClient
{
    /// <summary>
    /// Ini文件类
    /// </summary>
    public class IniFile
    {
        private readonly string _filePath;

        public IniFile(string filePath) => _filePath = filePath;

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        /// <summary>
        /// 读取INI文件A方法
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        public static extern uint GetPrivateProfileStringA(string section, string key, string def, byte[] retVal, int size, string filePath);

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteContentValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this._filePath);
        }

        /// <summary>
        /// 读取INI文件中的内容方法
        /// </summary>
        /// <param name="section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        public string ReadContentValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(10240);
            GetPrivateProfileString(section, key, string.Empty, temp, 10240, this._filePath);

            return temp.ToString();
        }
    }
}
