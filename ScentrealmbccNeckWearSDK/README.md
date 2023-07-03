# scentrealm_bcc.dll 说明文档

## 接入资源

（1）资源内容

| 名称 | 描述 |
| --- | --- |
| dist/x86/scentrealm_bcc.dll | SDK C++ DLL x86 |
| dist/x64/scentrealm_bcc.dll | SDK C++ DLL x64 |
| dependence/vc_redist.x86.exe | Visual C++ Redistributable for Visual Studio 2015 x86,x86 DLL 依赖，win7电脑需要 |
| dependence/vc_redist.x64.exe | Visual C++ Redistributable for Visual Studio 2015 x64,x64 DLL 依赖，win7电脑需要 |
| driver/CP210x_Windows_Drivers.zip | 控制器驱动，先安装此驱动才能连接到遥控器 |
| example/DllTest.exe | 无线控制实例程序 x64 |
| example/DllTest.cpp | 无线控制实例程序源代码 |
| example/DllTest_Script.exe | 无线控制脚本实例程序 x64 |
| example/DllTest_Script.cpp | 无线控制脚本实例程序源代码 |
| example/scentrealm_bcc.dll | SDK C++ DLL x64 |
| example/ScentRealmComponent.cs | Unity3D Component Demo |
| example/QueryDevStatusDemo.cs | 获取设备状态接口的示例代码 |
| CHANGEGLOG.txt | 更新日志 |
| docs/API_BCC.docx | 无线控制API文档 |
| docs/API_Script.docx | 无线控制脚本API文档 |
| docs/ScriptFormat.docx | 气味脚本格式说明 |
| docs/DllTest.docx | DllTest程序说明文件 |
| docs/DllTest_Script.docx | DllTest_Script程序说明文件 |
| README.docx | 说明文档（本文档） |
|  |  |

（2）名词解析

| 名词 | 说明 |
| --- | --- |
| 遥控器 | 和气味设备通讯的设备，DLL通过遥控器发出指令来和设备交互 |
| 设备编号 | 唯一标识一个气味设备的数字编号，显示在设备侧边 |
| 信道 | 用于将设备分组的一个逻辑概念，取值0 ~255，0 信道可控制其他信道的设备，默认情况下设备处于 0 信道 |
| 气路 | 一个气味设备至多可播放N种气味，这N个气味用1 ~N来表示 |
| 唤醒 | 设备5分钟内未收到指令，会进入低功耗休眠状态，需要唤醒后才能再度控制，设备开机时也处于休眠状态，需要唤醒才能被控制 |

（3）DLL说明

| 参数 | 说明 |
| --- | --- |
| Windows SDK版本 | 10.0.17134.0 |
| 平台工具集 | Visual Studio 2017 (v141) |

## 接入说明

### （1）脖戴式气味设备接入说明

1. 准备硬件设备：window系统的电脑（win7及以上版本），气味控制器，气味设备（脖戴式设备）
2. 气味设备开机（绿灯慢闪），气味控制器连接到电脑上，控制器开机（红绿灯闪）
3. 电脑安装 driver/CP210x_Windows_Drivers.zip 驱动
4. win7系统可能需要安装vc_redist.x86.exe/vc_redist.x64.exe 程序，您可以先运行DllTest.exe程序进行测试，如果运行未报错，则无需安装
5. 导入 scentrealm_bcc.dll
6. 调用连接方法，连接控制器( Scentrealm_AutoConnectCTL/Scentrealm_ManualConnectCTL)
7. 调用唤醒方法，**唤醒气味设备(** Scentrealm_WakeUp **)**
8. 调用播放气味方法，控制设备播放气味（安装了料盒会出现对应的气味）( Scentrealm_PlaySmell )

### （2）unity3d接入请参考example/ScentRealmComponent.cs 文件

1. 控制器的连接具有独占性，即某个软件已连接上控制器后，其他软件无法再连接到控制器，需要先断开连接；
2. unity3d进行过调试后，不会释放控制器的连接。此时，如果运行编译好的程序会出现无法连接到控制器的问题。这个时候，您可以选择关闭unity3d或者物理上断开控制器的usb线，重新连接节课解决问题；
3. 千万不要忘记**唤醒设备**，开机后设备处于休眠状态，请唤醒后再进行控制。设备5分钟内未收到指令会进入休眠状态。
