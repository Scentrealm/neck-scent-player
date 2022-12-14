// DllTest.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "windows.h"
#include <string>
			  

namespace DllTest_BCC {


	typedef int(*pScentrealm_ManualConnectCTL)(UCHAR COMID);
	typedef int(*pScentrealm_AutoConnectCTL)();
	typedef int(*pScentrealm_DisConnect)();
	typedef int(*pScentrealm_WakeUp)(bool sync);
	typedef int(*pScentrealm_IsCTLConnected)();
	typedef int(*pScentrealm_StopPlaySmell)(UCHAR Channel);
	typedef int(*pScentrealm_PlaySmell)(UCHAR SmellID, UINT32 DurationInMilliSecond, UCHAR Channel);
	typedef int(*pScentrealm_PlaySmellWithGear)(UINT16 SmellID, UINT32 DurationInMilliSecond, UINT16 Gear, UCHAR Channel);
	typedef int(*pScentrealm_IsAlive)(UINT32 DeviceID, UCHAR Channel);
	typedef int(*pScentrealm_SetWaitTimeoutMS)(DWORD Timeout);
	typedef int(*pScentrealm_SetDeviceChannel)(UINT32 DeviceID, UCHAR Channel);


	typedef int(*pScentrealm_GetControllerPC)();
	typedef int(*pScentrealm_SetControllerPC)(UCHAR PC);
	typedef int(*pScentrealm_PlaySmellWithPC)(UCHAR SmellID, UINT32 DurationInMilliSecond, UCHAR PC, UCHAR Channel);


	pScentrealm_ManualConnectCTL Scentrealm_ManualConnectCTL = NULL;
	pScentrealm_AutoConnectCTL Scentrealm_AutoConnectCTL = NULL;
	pScentrealm_DisConnect Scentrealm_DisConnect = NULL;
	pScentrealm_WakeUp Scentrealm_WakeUp = NULL;
	pScentrealm_IsCTLConnected Scentrealm_IsCTLConnected = NULL;
	pScentrealm_StopPlaySmell Scentrealm_StopPlaySmell = NULL;
	pScentrealm_PlaySmell Scentrealm_PlaySmell = NULL;
	pScentrealm_PlaySmellWithGear Scentrealm_PlaySmellWithGear = NULL;
	pScentrealm_IsAlive Scentrealm_IsAlive = NULL;
	pScentrealm_SetWaitTimeoutMS Scentrealm_SetWaitTimeoutMS = NULL;
	pScentrealm_SetDeviceChannel Scentrealm_SetDeviceChannel = NULL;
	pScentrealm_GetControllerPC Scentrealm_GetControllerPC = NULL;
	pScentrealm_SetControllerPC Scentrealm_SetControllerPC = NULL;
	pScentrealm_PlaySmellWithPC Scentrealm_PlaySmellWithPC = NULL;

	void LoadScentRealmDLL()
	{
		HMODULE my_dll = NULL;
		setlocale(LC_ALL, "chs");
		my_dll = LoadLibrary(__T("scentrealm_bcc.dll"));

		if (my_dll)
		{
			printf("Import dll succeed.\n");
			Scentrealm_ManualConnectCTL = (pScentrealm_ManualConnectCTL)GetProcAddress(my_dll, "Scentrealm_ManualConnectCTL");
			Scentrealm_AutoConnectCTL = (pScentrealm_AutoConnectCTL)GetProcAddress(my_dll, "Scentrealm_AutoConnectCTL");
			Scentrealm_DisConnect = (pScentrealm_DisConnect)GetProcAddress(my_dll, "Scentrealm_DisConnect");
			Scentrealm_WakeUp = (pScentrealm_WakeUp)GetProcAddress(my_dll, "Scentrealm_WakeUp");
			Scentrealm_IsCTLConnected = (pScentrealm_IsCTLConnected)GetProcAddress(my_dll, "Scentrealm_IsCTLConnected");
			Scentrealm_StopPlaySmell = (pScentrealm_StopPlaySmell)GetProcAddress(my_dll, "Scentrealm_StopPlaySmell");
			Scentrealm_PlaySmell = (pScentrealm_PlaySmell)GetProcAddress(my_dll, "Scentrealm_PlaySmell");
			Scentrealm_PlaySmellWithGear = (pScentrealm_PlaySmellWithGear)GetProcAddress(my_dll, "Scentrealm_PlaySmellWithGear");
			Scentrealm_IsAlive = (pScentrealm_IsAlive)GetProcAddress(my_dll, "Scentrealm_IsAlive");
			Scentrealm_SetWaitTimeoutMS = (pScentrealm_SetWaitTimeoutMS)GetProcAddress(my_dll, "Scentrealm_SetWaitTimeoutMS");
			Scentrealm_SetDeviceChannel = (pScentrealm_SetDeviceChannel)GetProcAddress(my_dll, "Scentrealm_SetDeviceChannel");

			Scentrealm_SetControllerPC = (pScentrealm_SetControllerPC)GetProcAddress(my_dll,"Scentrealm_SetControllerPC");
			Scentrealm_GetControllerPC = (pScentrealm_GetControllerPC)GetProcAddress(my_dll, "Scentrealm_GetControllerPC");
			Scentrealm_PlaySmellWithPC = (pScentrealm_PlaySmellWithPC)GetProcAddress(my_dll, "Scentrealm_PlaySmellWithPC");

			if (!Scentrealm_ManualConnectCTL)
			{
				printf("Scentrealm_ManualConnectCTL failed.\n");
			}
			if (!Scentrealm_AutoConnectCTL)
			{
				printf("Scentrealm_AutoConnectCTL failed.\n");
			}
			if (!Scentrealm_DisConnect)
			{
				printf("Scentrealm_DisConnect failed.\n");
			}
			if (!Scentrealm_WakeUp)
			{
				printf("Scentrealm_WakeUp failed.\n");
			}
			if (!Scentrealm_IsCTLConnected)
			{
				printf("Scentrealm_IsCTLConnected failed.\n");
			}
			if (!Scentrealm_StopPlaySmell)
			{
				printf("Scentrealm_StopPlaySmell failed.\n");
			}
			if (!Scentrealm_PlaySmell)
			{
				printf("Scentrealm_PlaySmell failed.\n");
			}
			if (!Scentrealm_PlaySmellWithGear)
			{
				printf("Scentrealm_PlaySmellWithGear failed.\n");
			}
			if (!Scentrealm_IsAlive)
			{
				printf("Scentrealm_IsAlive failed.\n");
			}
			if (!Scentrealm_SetWaitTimeoutMS)
			{
				printf("Scentrealm_SetWaitTimeoutMS failed.\n");
			}
			if (!Scentrealm_SetDeviceChannel)
			{
				printf("Scentrealm_SetDeviceChannel failed.\n");
			}
			if (!Scentrealm_SetControllerPC)
			{
				printf("Scentrealm_SetControllerPC failed.\n");
			}
			if (!Scentrealm_GetControllerPC)
			{
				printf("Scentrealm_GetControllerPC failed.\n");
			}
			if (!Scentrealm_PlaySmellWithPC)
			{
				printf("Scentrealm_PlaySmellWithPC failed.\n");
			}
		}
		else
		{
			printf("failed.\n");
		}
	}



	void TestBBC()
	{
		printf("8: Scentrealm_ManualConnectCTL\n");
		printf("1: Scentrealm_AutoConnectCTL\n");
		printf("2: Scentrealm_PlaySmell\n");
		printf("3: Play Channel 0 SmellID 1 for 10s\n");
		printf("4: Play Channel 0 SmellID 1 Gear 3 for 10s\n");
		printf("5: Stop Play Channel 0\n");
		printf("6: Scentrealm_IsAlive\n");

		printf("7: Scentrealm_SetDeviceChannel\n");
		printf("7: Scentrealm_SetDeviceChannel Set Device 7 to Channel 12\n");

		printf("9: Scentrealm_WakeUp\n");
		printf("10: Scentrealm_DisConnect\n");
		printf("11: Scentrealm_IsCTLConnected\n");

		printf("12: Scentrealm_GetControllerPC\n");
		printf("13: Scentrealm_SetControllerPC\n");
		printf("14: Scentrealm_PlaySmellWithPC\n");

		//Scentrealm_GetControllerPC
		//Scentrealm_SetControllerPC
		//Scentrealm_PlaySmellWithPC
		printf("\n");

		while (1)
		{
			char buff[256] = { 0 };
			int cmd = 0;
			fgets(buff, 255, stdin);
			sscanf_s(buff, "%d", &cmd);
			switch (cmd)
			{
			case 8:
			{
				UCHAR COMID;
				printf("input  COMID\n");
				scanf_s("%hhd", &COMID);
				if (Scentrealm_ManualConnectCTL(COMID) > 0)
				{
					// Scentrealm_SetControllerPC(115);
					printf("Manual Connect Success\n");
				}
				else
				{
					printf("Manual Connect Failed\n");
				}
				break;
			}
			case 1:
			{
				if (Scentrealm_AutoConnectCTL() > 0)
				{
					// Scentrealm_SetControllerPC(115);
					printf("Auto Connect Success\n");
				}
				else
				{
					printf("Auto Connect Failed\n");
				}
				break;
			}
			case 2:
			{
				BYTE Channel;
				UINT32 SmellID;
				UINT32 DurationInMilliSecond;
				printf("input  SmellID DurationInMilliSecond Channel\n");
				scanf_s("%d%d%hhd", &SmellID, &DurationInMilliSecond, &Channel);
				if (Scentrealm_PlaySmell(SmellID, DurationInMilliSecond, Channel) < 0)
				{
					printf("controller not connected\n");
				}
				else
				{
					printf("play smell success\n");
				}
				break;
			}
			case 3:
			{
				if (Scentrealm_PlaySmell(1, 10000, 0) < 0)
					printf("controller not connected\n");
				else
					printf("play smell success\n");
				break;
			}
			case 4:
			{
				if (Scentrealm_PlaySmellWithGear(1, 10000, 3, 0) < 0)
					printf("controller not connected\n");
				else
					printf("play smell success\n");
				break;
			}
			case 5:
			{
				if (Scentrealm_StopPlaySmell(0) < 0)
					printf("controller not connected\n");
				else
					printf("stop play smell success\n");
				break;

			}
			case 6:
			{
				int device_id = 0;
				BYTE channel = 0;
				printf("input device_id & channel \n");
				scanf_s("%d%hhd", &device_id, &channel);
				if (Scentrealm_IsAlive(device_id, channel) < 0)
					printf("Device %d Is Not Alive\n", device_id);
				else
					printf("Device %d Is Alive\n", device_id);
				break;
			}
			case 7:
			{
				int device_id = 0;
				BYTE channel = 0;
				printf("input device_id & channel \n");
				scanf_s("%d%hhd", &device_id, &channel);
				auto ret = Scentrealm_SetDeviceChannel(device_id, channel);
				if (ret > 0)
					printf("Device %d Set Channel %d Success\n", device_id, channel);
				else
					printf("Device %d Set Channel %d Failed %d \n", device_id, channel, ret);
				break;
			}
			//case 8:
			//{
			//	int device_id = 7;
			//	BYTE channel = 12;
			//	auto ret = Scentrealm_SetDeviceChannel(device_id, channel);
			//	if (ret > 0)
			//		printf("Device %d Set Channel %d Success\n", device_id, channel);
			//	else
			//		printf("Device %d Set Channel %d Failed %d \n", device_id, channel, ret);
			//	break;
			//}
			case 9:
			{
				if (Scentrealm_WakeUp(true) < 0)
					printf("Scentrealm_WakeUp Failed\n");
				else
					printf("Scentrealm_WakeUp Success\n");
				break;
			}
			case 10:
			{
				// Scentrealm_SetDeviceChannel
				Scentrealm_DisConnect();
				printf("Scentrealm_DisConnect\n");
				break;
			}
			case 11:
			{
				printf("Scentrealm_IsCTLConnected = %d \n", Scentrealm_IsCTLConnected());
				break;
			}
			case 12:
			{
				printf("Scentrealm_GetControllerPC = %d \n", Scentrealm_GetControllerPC());
				break;
			}
			case 13:
			{
				UCHAR PC;
				printf("input PhysicalChannel\n");
				scanf_s("%hhd", &PC);
				printf("Scentrealm_SetControllerPC = %d \n", Scentrealm_SetControllerPC(PC));
				break;
			}
			case 14:
			{
				BYTE Channel;
				UINT32 SmellID;
				UINT32 DurationInMilliSecond;
				UCHAR PC;
				printf("input  SmellID DurationInMilliSecond PhysicalChannel Channel\n");
				scanf_s("%d%d%hhd%hhd", &SmellID, &DurationInMilliSecond,&PC, &Channel);
				if (Scentrealm_PlaySmellWithPC(SmellID, DurationInMilliSecond, PC, Channel) < 0)
				{
					printf("controller not connected\n");
				}
				else
				{
					printf("play smell success\n");
				}
				break;
			}
			default:
				printf("8: Scentrealm_ManualConnectCTL\n");
				printf("1: Scentrealm_AutoConnectCTL\n");
				printf("2: Scentrealm_PlaySmell\n");
				printf("3: Play Channel 0 SmellID 1 for 10s\n");
				printf("4: Play Channel 0 SmellID 1 Gear 3 for 10s\n");
				printf("5: Stop Play Channel 0\n");
				printf("6: Scentrealm_IsAlive\n");

				printf("7: Scentrealm_SetDeviceChannel\n");
				printf("7: Scentrealm_SetDeviceChannel Set Device 7 to Channel 12\n");

				printf("9: Scentrealm_WakeUp\n");
				printf("10: Scentrealm_DisConnect\n");
				printf("11: Scentrealm_IsCTLConnected\n");
				printf("12: Scentrealm_GetControllerPC\n");
				printf("13: Scentrealm_SetControllerPC\n");
				printf("14: Scentrealm_PlaySmellWithPC\n");
				printf("\n");
				break;
			}
		}
	}

}


int main()
{
	DllTest_BCC::LoadScentRealmDLL();
	DllTest_BCC::TestBBC();
	return 0;
}

