# 脖戴式企微播放器 SDK

开发语言： C#

### 使用说明

在VS平台下，把 ScentrealmBCC.dll 放在项目目录下，工程上引用此SDK，把 PortManager.cs 包含到项目中，在使用时可以先调用单例中的 DevLink() 进行打开串口连接设备(目前自动连接设备)；
在需要播放气味时，调用PlaySmell()方法，参数气味编号和持续时间(秒)，在需要停止的时候发送StopPlaySmell(),结合这二个指令进行应用。

<aside>
💡 ScentrealmBCC.dll 为集成的 SDK，PortManager.cs 为设备控制单例。

</aside>

### 主要功能

连接设备：DevLink()

播放气味：PlaySmell(int smellid, int durationtime)

| 参数名 | 类型 | 说明 | 示例 |
| --- | --- | --- | --- |
| smellid | int | 气味通道编号（1-12） | 1 |
| durationtime | int | 播放时间（单位 S） | 3 |

停止播放：StopPlaySmell()

断开设备：DisConnect()
