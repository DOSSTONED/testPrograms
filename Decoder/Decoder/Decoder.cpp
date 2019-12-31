// Decoder.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<iostream>

int chartoint(char c)
{
	if( ('0' <= c ) && ( c <= '9'))
		return (int)c - 48;
	if( ('A' <= c ) && ( c <= 'F'))
		return (int)c - 55;
	else
		return -1;
}

int _tmain(int argc , _TCHAR* argv[])
{
	char a[]="63636363 63636363 72464663 6F6D6F72 466D203A 65693A72 43646E20 6F54540A 5920453A 54756F0A 6F6F470A 21643A6F 594E2020 206F776F 79727574 4563200A 6F786F68 6E696373 6C206765 796C656B 2C336573 7420346E 20216F74 726F5966 7565636F 20206120 6C616763 74206C6F 20206F74 74786565 65617276 32727463 6E617920 680A6474 6F697661 20646E69 21687467 63002065 6C6C7861 78742078 6578206F 72747878 78636178 00783174";
	char* p = a;
	int b;
	while(p)
	{
		if(p[0]==' ')
			p++;
		else
		{
			if(!p)
				break;
			b =16 * chartoint(p[0]) + chartoint(p[1]);
			printf("%c",b);
			p++;
			p++;
		}
	}
	system("pause");
	return 0;
}

