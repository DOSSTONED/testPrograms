// TEMPCPP.cpp : 定义控制台应用程序的入口点。
//

/*设置SandBoxMode值为1*/
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
	//打开指定的注册表键：
	ret=RegOpenKeyEx(HKEY_LOCAL_MACHINE,
		key,
		0,
		KEY_ALL_ACCESS,
		&hkey);
	if(ret!=ERROR_SUCCESS)
	{ printf("RegOpenKeyex error! %x\n",GetLastError());
	return 0;
	}
	else{ //下面是设置SandBoxMod值,类型为REG_DWORD,数据为1:
		Usage();

		ret= RegSetValueEx(hkey,"SandBoxMode",0,REG_DWORD,(unsigned char *)lpData1,SizeF1);
		if(ret!=ERROR_SUCCESS)
		{ printf("RegSetValueEx error!%x\n",GetLastError());
		return 0;}
	}

	//下面读出SandBoxMode的值并存入lpData中:
	ret= RegQueryValueEx(hkey,"SandBoxMode",NULL,&lpType,&lpData,&SizeF);
	//ret= RegQueryValue(hkey,"PortNumber",lpData,&SizeF);
	if(ret!=ERROR_SUCCESS)
	{ printf("RegQueryValueEx error! %x\n",GetLastError());
	return 0;}

	printf("SandBoxMode=%d\n",lpData);
	printf("lpType=%d\n",lpType);
	return 0;
}


//输出帮助的典型方法:
void Usage (void)
{
	fprintf(stdout,"===============================================================================\n"
		"\t名称：启动沙盒模式程序\n"
		"\t作者：pt007@vip.sina.com\n"
		"\tQQ： 7491805\n"
		"\t声明：本软件由pt007原创，转载请注明出处，谢谢!\n"
		"===============================================================================\n");
}