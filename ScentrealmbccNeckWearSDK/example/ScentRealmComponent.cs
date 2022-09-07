using System;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
#endif 

#if UNITY_EDITOR 
[AddComponentMenu("ScentRealm/SmellPlayer")]
#endif 
public class ScentRealmComponent : MonoBehaviour
{
    void Log(string msg)
    {
        var msgsss = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + msg;

        Debug.Log(msgsss);
    }

    public void Start()
    {
        Log("Start");
        init();

        PlayScript();
    }


    public int init()
    {
        int ret = -1;

        ret = Scentrealm_AutoConnectCTL();
        this.Log("Scentrealm_AutoConnectCTL = " + ret);
        ret = Scentrealm_WakeUp(false);
        this.Log("Scentrealm_WakeUp = " + ret);

        return ret;
    }

    public int PlayScript()
    {
        int ret = -1;
        ret = Scentrealm_ScriptRunScript(System.Text.Encoding.Default.GetBytes(@"F:\vlc\脚本\cccc - 副本.srt"), 0);
        this.Log("Scentrealm_ScriptRunScript = " + ret);

        return ret;
    }


    public int WakeUp()
    {
        int ret = -1;
        ret = Scentrealm_WakeUp(true);
        this.Log("Scentrealm_WakeUp ret = " + ret);
        return ret;
    }


    public int PlaySmell(int smell, int duration)
    {
        int ret = -1;
        ret = Scentrealm_PlaySmell((UInt32)smell, (UInt32)duration * 1000, 0);
        this.Log("Scentrealm_PlaySmell smell = " + smell + " duration = " + duration + " ret = " + ret);
        return ret;
    }

    public int GetControllerPhysicalChannel()
    {
        int ret = -1;
        ret = Scentrealm_GetControllerPC();
        if (ret >= 0)
            return ret;

        this.Log("Scentrealm_GetControllerPC ERROR");
        return ret;
    }

    public int SetControllerPhysicalChannel(Byte PhysicalChannel)
    {
        int ret = -1;
        ret = Scentrealm_SetControllerPC(PhysicalChannel);
        if (ret >= 0)
            return ret;

        this.Log("Scentrealm_GetControllerPC ERROR:" +  ret);
        return ret;
    }

    public int PlaySmellWithPC(int smell, int duration,Byte physical_channel)
    {
        int ret = -1;
        ret = Scentrealm_PlaySmellWithPC((UInt32)smell, (UInt32)duration * 1000, physical_channel, 0);
        this.Log("Scentrealm_PlaySmellWithPC smell = " + smell + " duration = " + duration + " ret = " + ret);
        return ret;
    }

    public int StopSmell(int smell)
    {
        int ret = -1;
        ret = Scentrealm_StopPlaySmell(0);
        this.Log("Scentrealm_StopPlaySmell = " + ret);
        return ret;
    }


    public int Close()
    {
        int ret = -1;
        ret = Scentrealm_DisConnect();
        this.Log("Scentrealm_DisConnect = " + ret);
        return ret;
    }

    #region Basic APIs

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_AutoConnectCTL();

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_WakeUp(bool sync);

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_PlaySmell(UInt32 SmellID, UInt32 DurationInMilliSecond, Byte Channel);

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_PlaySmellWithPC(UInt32 SmellID, UInt32 DurationInMilliSecond, Byte PhysicalChannel, Byte Channel);

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_GetControllerPC();

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_SetControllerPC(Byte PhysicalChannel);

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_DisConnect();

    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_StopPlaySmell(Byte Channel);


    [DllImport("scentrealm_bcc", CallingConvention = CallingConvention.Cdecl)]
    static extern private int Scentrealm_IsAlive(UInt32 DeviceID);

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
}
