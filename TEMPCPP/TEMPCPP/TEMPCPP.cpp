// TEMPCPP.cpp : �������̨Ӧ�ó������ڵ㡣
//

/*����SandBoxModeֵΪ1*/
#include <cwindows>
#include <cstring>
#include <cstdio>
#include <cstdlib>
#include "stdafx.h"
void Usage(void);

int main(){
	HKEY hkey;
	DWORD lpType=0,ret;
	//char *lpData;
	int lpData;
	char *lpData1;
	char *key;
	DWORD SizeF,SizeF1;
	//lpData="test";
	ZeroMemory(&lpData,sizeof(lpData));
	SizeF=sizeof(lpData);
	lpData1="\1";
	SizeF1=sizeof(lpData1);
	//SizeF=10;
	key="SOFTWARE\\Microsoft\\Jet\\4.0\\Engines";
	//��ָ����ע������
	ret=RegOpenKeyEx(HKEY_LOCAL_MACHINE,
		key,
		0,
		KEY_ALL_ACCESS,
		&hkey);
	if(ret!=ERROR_SUCCESS)
	{ printf("RegOpenKeyex error! %x\n",GetLastError());
	return 0;
	}
	else{ //����������SandBoxModֵ,����ΪREG_DWORD,����Ϊ1:
		Usage();

		ret= RegSetValueEx(hkey,"SandBoxMode",0,REG_DWORD,(unsigned char *)lpData1,SizeF1);
		if(ret!=ERROR_SUCCESS)
		{ printf("RegSetValueEx error!%x\n",GetLastError());
		return 0;}
	}

	//�������SandBoxMode��ֵ������lpData��:
	ret= RegQueryValueEx(hkey,"SandBoxMode",NULL,&lpType,&lpData,&SizeF);
	//ret= RegQueryValue(hkey,"PortNumber",lpData,&SizeF);
	if(ret!=ERROR_SUCCESS)
	{ printf("RegQueryValueEx error! %x\n",GetLastError());
	return 0;}

	printf("SandBoxMode=%d\n",lpData);
	printf("lpType=%d\n",lpType);
	return 0;
}


//��������ĵ��ͷ���:
void Usage (void)
{
	fprintf(stdout,"===============================================================================\n"
		"\t���ƣ�����ɳ��ģʽ����\n"
		"\t���ߣ�pt007@vip.sina.com\n"
		"\tQQ�� 7491805\n"
		"\t�������������pt007ԭ����ת����ע��������лл!\n"
		"===============================================================================\n");
}