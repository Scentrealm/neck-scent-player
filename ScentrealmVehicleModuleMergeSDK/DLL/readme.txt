项目中引用SKII.Communication.dll；SKII.Devices.dll；SKII.Log.dll；SKII.Protocols.dll；SKII.Util.dll；

引入ScentDeviceManager.cs文件；进行操作
调用初始化方法Init()；在Config文件夹下配置文件里面的设备和通道获得当前设备
使用 Device usedDev = DeviceManager.GetInstance().GetDeviceById(AppManager.CreateInstance().UsedDevId);单例管理所有设备
目前设备有二种类型（OderModule和SignalSynV103）
OderModule为新版车载模块，SignalSynV103为脖戴设备，示例中已经有脖戴设备操作方法；
 单使用OderModule时调用public void PlaySmell(byte smellid,int duration)方法进行播放；调用StopPlay()为停止播放

目前配置文件DeviceManager.json配置的是OderModule，修改成SignalSynV103；协议SmellKingdomProtocol修改成ScentMultiWirelessProtocol即可，删除多台设备只保留一台