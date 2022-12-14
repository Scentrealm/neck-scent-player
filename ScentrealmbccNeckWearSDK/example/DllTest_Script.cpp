// DllTest.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "windows.h"
#include <string>
#include <iostream>
#include <sstream>		  
using namespace std;

namespace DllTest_Script {

	typedef int(*pScentrealm_ManualConnectCTL)(UCHAR COMID);
	typedef int(*pScentrealm_AutoConnectCTL)();
	typedef int(*pScentrealm_DisConnect)();
	typedef int(*pScentrealm_WakeUp)(bool sync);
	typedef int(*pScentrealm_IsCTLConnected)();


	typedef int(*pScentrealm_ScriptLoad)(const char * file);
	typedef int(*pScentrealm_ScriptRun)(int start_position_ms);
	typedef int(*pScentrealm_ScriptRunScript)(const char *  file, int start_position_ms);
	typedef int(*pScentrealm_ScriptChangePosition)(int start_position_ms);
	typedef int(*pScentrealm_ScriptPause)();
	typedef int(*pScentrealm_ScriptContinue)();
	typedef int(*pScentrealm_ScriptStop)();
	typedef int(*pScentrealm_ScriptIsRunning)();


	pScentrealm_ManualConnectCTL Scentrealm_ManualConnectCTL = NULL;
	pScentrealm_AutoConnectCTL Scentrealm_AutoConnectCTL = NULL;
	pScentrealm_DisConnect Scentrealm_DisConnect = NULL;
	pScentrealm_WakeUp Scentrealm_WakeUp = NULL;
	pScentrealm_IsCTLConnected Scentrealm_IsCTLConnected = NULL;

	pScentrealm_ScriptLoad Scentrealm_ScriptLoad = NULL;
	pScentrealm_ScriptRun Scentrealm_ScriptRun = NULL;
	pScentrealm_ScriptRunScript Scentrealm_ScriptRunScript = NULL;
	pScentrealm_ScriptChangePosition Scentrealm_ScriptChangePosition = NULL;
	pScentrealm_ScriptPause Scentrealm_ScriptPause = NULL;
	pScentrealm_ScriptContinue Scentrealm_ScriptContinue = NULL;
	pScentrealm_ScriptStop Scentrealm_ScriptStop = NULL;
	pScentrealm_ScriptIsRunning Scentrealm_ScriptIsRunning = NULL;


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

			Scentrealm_ScriptLoad = (pScentrealm_ScriptLoad)GetProcAddress(my_dll, "Scentrealm_ScriptLoad");
			Scentrealm_ScriptRun = (pScentrealm_ScriptRun)GetProcAddress(my_dll, "Scentrealm_ScriptRun");
			Scentrealm_ScriptRunScript = (pScentrealm_ScriptRunScript)GetProcAddress(my_dll, "Scentrealm_ScriptRunScript");
			Scentrealm_ScriptChangePosition = (pScentrealm_ScriptChangePosition)GetProcAddress(my_dll, "Scentrealm_ScriptChangePosition");
			Scentrealm_ScriptPause = (pScentrealm_ScriptPause)GetProcAddress(my_dll, "Scentrealm_ScriptPause");
			Scentrealm_ScriptContinue = (pScentrealm_ScriptContinue)GetProcAddress(my_dll, "Scentrealm_ScriptContinue");
			Scentrealm_ScriptStop = (pScentrealm_ScriptStop)GetProcAddress(my_dll, "Scentrealm_ScriptStop");
			Scentrealm_ScriptIsRunning = (pScentrealm_ScriptIsRunning)GetProcAddress(my_dll, "Scentrealm_ScriptIsRunning");

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
			if (!Scentrealm_ScriptLoad)
			{
				printf("Scentrealm_ScriptLoad failed.\n");
			}
			if (!Scentrealm_ScriptRun)
			{
				printf("Scentrealm_ScriptRun failed.\n");
			}
			if (!Scentrealm_ScriptRunScript)
			{
				printf("Scentrealm_ScriptRunScript failed.\n");
			}
			if (!Scentrealm_ScriptChangePosition)
			{
				printf("Scentrealm_ScriptChangePosition failed.\n");
			}
			if (!Scentrealm_ScriptPause)
			{
				printf("Scentrealm_ScriptPause failed.\n");
			}
			if (!Scentrealm_ScriptContinue)
			{
				printf("Scentrealm_ScriptContinue failed.\n");
			}
			if (!Scentrealm_ScriptStop)
			{
				printf("Scentrealm_ScriptStop failed.\n");
			}
			if (!Scentrealm_ScriptIsRunning)
			{
				printf("Scentrealm_ScriptIsRunning failed.\n");
			}
		}
		else
		{
			printf("failed.\n");
		}
	}


	void TestScript()
	{

		printf("10: Scentrealm_ManualConnectCTL\n");
		printf("1: Scentrealm_AutoConnectCTL\n");
		printf("2: Scentrealm_ScriptLoad\n");
		printf("3: Scentrealm_ScriptRun\n");
		printf("4: Scentrealm_ScriptChangePosition\n");
		printf("5: Scentrealm_ScriptPause\n");
		printf("6: Scentrealm_ScriptContinue\n");
		printf("7: Scentrealm_ScriptStop\n");
		printf("8: Scentrealm_ScriptIsRunning\n");

		printf("\n");

		int r = 0;
		string input_str;
		while (1)
		{
			char buff[256] = { 0 };
			int cmd = 0;
			fgets(buff, 255, stdin);
			sscanf_s(buff, "%d", &cmd);
			switch (cmd)
			{
			case 10:
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
				cout << "Please Enter Script Full Path:" << endl;
				cin >> input_str;
				r = Scentrealm_ScriptLoad(input_str.c_str());

				if (r < 0)
				{
					printf("Scentrealm_ScriptLoad Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptLoad Success\n");
				}
				break;
			}
			case 3:
			{
				cout << "Please Enter Script Start Time(ms):" << endl;

				cin >> input_str;

				int start = 0;
				stringstream sstr(input_str);
				sstr >> start;

				r = Scentrealm_ScriptRun(start);

				if (r < 0)
				{
					printf("Scentrealm_ScriptRun Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptRun Success AT Position %d\n", start);
				}
				break;
			}
			case 4:
			{
				cout << "Please Enter Changed Position(ms):" << endl;

				cin >> input_str;

				int start = 0;
				stringstream sstr(input_str);
				sstr >> start;

				r = Scentrealm_ScriptChangePosition(start);

				if (r < 0)
				{
					printf("Scentrealm_ScriptChangePosition Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptChangePosition Success AT Position %d\n", start);
				}
				break;
			}
			case 5:
			{

				r = Scentrealm_ScriptPause();

				if (r < 0)
				{
					printf("Scentrealm_ScriptPause Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptPause Success\n");
				}
				break;

			}
			case 6:
			{
				r = Scentrealm_ScriptContinue();

				if (r < 0)
				{
					printf("Scentrealm_ScriptContinue Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptContinue Success\n");
				}
				break;
			}
			case 7:
			{
				r = Scentrealm_ScriptStop();

				if (r < 0)
				{
					printf("Scentrealm_ScriptStop Error %d \n", r);
				}
				else
				{
					printf("Scentrealm_ScriptStop Success\n");
				}
				break;
			}
			case 8:
			{
				r = Scentrealm_ScriptIsRunning();

				if (r < 0)
				{
					printf("Scentrealm_ScriptIsRunning Error %d \n", r);
				}
				else
				{
					if (r == 1)
						printf("Script Is Running\n");
					else

						printf("Script Is Not Running\n");
				}
				break;
			}
			default:
				printf("10: Scentrealm_ManualConnectCTL\n");
				printf("1: Scentrealm_AutoConnectCTL\n");
				printf("2: Scentrealm_ScriptLoad\n");
				printf("3: Scentrealm_ScriptRun\n");
				printf("4: Scentrealm_ScriptChangePosition\n");
				printf("5: Scentrealm_ScriptPause\n");
				printf("6: Scentrealm_ScriptContinue\n");
				printf("7: Scentrealm_ScriptStop\n");
				printf("8: Scentrealm_ScriptIsRunning\n");

				printf("\n");
				break;
			}
		}
	}

}


//int main()
//{
//	DllTest_Script::LoadScentRealmDLL();
//	DllTest_Script::TestScript();
//	return 0;
//}

