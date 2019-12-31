// CPP_MAC.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "nb30"
#include "cstring"
#include < windows.h >  
#include < wincon.h >  
#include < stdlib.h >  
#include < stdio.h >  
#include < time.h >  
#pragma comment(lib,"netapi32.lib")

typedef struct _ASTAT_
{
	ADAPTER_STATUS adapt;
	NAME_BUFFER    NameBuff[30];
} ASTAT, * PASTAT;


CString GetMacAddress(CString sNetBiosName)
{
	ASTAT Adapter;

	NCB ncb;
	UCHAR uRetCode;

	memset(&ncb, 0, sizeof(ncb));
	ncb.ncb_command = NCBRESET;
	ncb.ncb_lana_num = 0;

	uRetCode = Netbios(&ncb);

	memset(&ncb, 0, sizeof(ncb));
	ncb.ncb_command = NCBASTAT;
	ncb.ncb_lana_num = 0;

	sNetBiosName.MakeUpper();

	FillMemory(ncb.ncb_callname, NCBNAMSZ - 1, 0x20);

	strcpy((char *)ncb.ncb_callname, (LPCTSTR) sNetBiosName);

	ncb.ncb_callname[sNetBiosName.GetLength()] = 0x20;
	ncb.ncb_callname[NCBNAMSZ] = 0x0;

	ncb.ncb_buffer = (unsigned char *) &Adapter;
	ncb.ncb_length = sizeof(Adapter);

	uRetCode = Netbios(&ncb);

	CString sMacAddress;

	if (uRetCode == 0)
	{
		sMacAddress.Format(_T("%02x%02x%02x%02x%02x%02x"),
			Adapter.adapt.adapter_address[0],
			Adapter.adapt.adapter_address[1],
			Adapter.adapt.adapter_address[2],
			Adapter.adapt.adapter_address[3],
			Adapter.adapt.adapter_address[4],
			Adapter.adapt.adapter_address[5]);
	}
	return sMacAddress;
}



int _tmain(int argc, _TCHAR* argv[])
{


	return 0;
}

