using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRClient
{
    class Program
    {
        static void Main(string[] args)
        {
			int port = 10086;
			IniFile ini = new IniFile(Environment.CurrentDirectory  + "\\config.ini");
			string portStr = ini.ReadContentValue("Setting", "ServerPort").ToString();
			if (!string.IsNullOrEmpty(portStr))
			{
				int sport = Convert.ToInt32(portStr);
				if(sport > 0 && sport < 65535)
                {
					port = sport;
				}
			}

			try
            {
				using (SimpleTcpClient simpleTcpClient = new SimpleTcpClient())
                {
					simpleTcpClient.StringEncoder = Encoding.UTF8;
					simpleTcpClient.Connect("127.0.0.1", port);//
					if (args.Length == 2)
					{
						simpleTcpClient.WriteAndGetReply(args[0] + ":" + args[1], false);
						return;
					}
					//simpleTcpClient.WriteAndGetReply("1:2", false);
				}
			}
			catch
			{ }
		}
    }
}
