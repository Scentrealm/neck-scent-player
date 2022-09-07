// DllTest.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "windows.h"
#include <string>


//int DLL_EXPORT Scentrealm_VM_AutoConnect();
//int DLL_EXPORT Scentrealm_VM_DisConnect();
//int DLL_EXPORT Scentrealm_VM_StopPlaySmell();
//int DLL_EXPORT Scentrealm_VM_IsConnected();
//int DLL_EXPORT Scentrealm_VM_PlaySmell(UCHAR SmellID, UINT32 DurationInSecond, UCHAR Power);


typedef int(*pScentrealm_VM_AutoConnect)();
typedef int(*pScentrealm_VM_DisConnect)();
typedef int(*pScentrealm_VM_IsConnected)();
typedef int(*pScentrealm_VM_StopPlaySmell)();
typedef int(*pScentrealm_VM_PlaySmell)(UCHAR SmellID, UINT32 DurationInSecond, UCHAR Power);
pScentrealm_VM_AutoConnect Scentrealm_VM_AutoConnect = NULL;
pScentrealm_VM_DisConnect Scentrealm_VM_DisConnect = NULL;
pScentrealm_VM_IsConnected Scentrealm_VM_IsConnected = NULL;
pScentrealm_VM_StopPlaySmell Scentrealm_VM_StopPlaySmell = NULL;
pScentrealm_VM_PlaySmell Scentrealm_VM_PlaySmell = NULL;



int main1()
{
	HMODULE my_dll = NULL;
	setlocale(LC_ALL, "chs");
	my_dll = LoadLibrary(__T("scentrealm_bcc.dll"));

	if (my_dll)
	{
		printf("Import dll succeed.\n");
		Scentrealm_VM_AutoConnect = (pScentrealm_VM_AutoConnect)GetProcAddress(my_dll, "Scentrealm_VM_AutoConnect");
		Scentrealm_VM_DisConnect = (pScentrealm_VM_DisConnect)GetProcAddress(my_dll, "Scentrealm_VM_DisConnect");
		Scentrealm_VM_IsConnected = (pScentrealm_VM_IsConnected)GetProcAddress(my_dll, "Scentrealm_VM_IsConnected");
		Scentrealm_VM_StopPlaySmell = (pScentrealm_VM_StopPlaySmell)GetProcAddress(my_dll, "Scentrealm_VM_StopPlaySmell");
		Scentrealm_VM_PlaySmell = (pScentrealm_VM_PlaySmell)GetProcAddress(my_dll, "Scentrealm_VM_PlaySmell");
		if (!Scentrealm_VM_AutoConnect)
		{
			printf("Scentrealm_VM_AutoConnect failed.\n");
		}
		if (!Scentrealm_VM_DisConnect)
		{
			printf("Scentrealm_VM_DisConnect failed.\n");
		}
		if (!Scentrealm_VM_IsConnected)
		{
			printf("Scentrealm_VM_IsConnected failed.\n");
		}
		if (!Scentrealm_VM_StopPlaySmell)
		{
			printf("Scentrealm_StopPlaySmell failed.\n");
		}
		if (!Scentrealm_VM_PlaySmell)
		{
			printf("Scentrealm_VM_PlaySmell failed.\n");
		}
	}
	else
	{
		printf("failed.\n");
	}


	printf("1: Scentrealm_VM_AutoConnect\n");
	printf("2: Scentrealm_VM_PlaySmell\n");
	printf("3: Scentrealm_VM_StopPlaySmell\n");
	printf("4: Scentrealm_VM_DisConnect\n");
	printf("5: Scentrealm_VM_IsConnected\n");
	while (1)
	{
		char buff[256] = { 0 };
		int cmd = 0;
		fgets(buff, 255, stdin);
		sscanf_s(buff, "%d", &cmd);
		switch (cmd)
		{
		case 1:
		{
			if (Scentrealm_VM_AutoConnect() > 0)
			{
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
			BYTE SmellID;
			UINT32 DurationInMilliSecond;
			printf("input  SmellID DurationInMilliSecond power\n");
			scanf_s("%hhd%d%hhd", &SmellID, &DurationInMilliSecond, &Channel);
			if (Scentrealm_VM_PlaySmell(SmellID, DurationInMilliSecond, Channel) < 0)
			{
				printf("device not connected\n");
			}
			else
			{
				printf("play smell success\n");
			}
			break;
		}
		case 3:
		{
			if (Scentrealm_VM_StopPlaySmell() < 0)
				printf("device not connected\n");
			else
			{
				printf("stop play smell success\n");
			}
			break;
		}
		case 4:
		{
			if (Scentrealm_VM_DisConnect() < 0)
				printf("disconnect failed\n");
			else
			{
				printf("disconnect success\n");
			}
			break;
		}
		case 5:
		{
			if (Scentrealm_VM_IsConnected() < 0)
				printf("not connected\n");
			else
			{
				printf("connected\n");
			}
			break;
		}


		default:
			printf("1: Scentrealm_VM_AutoConnect\n");
			printf("2: Scentrealm_VM_PlaySmell\n");
			printf("3: Scentrealm_VM_StopPlaySmell\n");
			printf("4: Scentrealm_VM_DisConnect\n");
			printf("5: Scentrealm_VM_IsConnected\n");
			break;
		}
	}
	return 0;
}
