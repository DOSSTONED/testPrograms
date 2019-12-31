// SevenToElse.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <string>
#include <cmath>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int a = 0;
	string s;
	while(1)
	{
		cin>>s;
		a=0;
		int carry=0;
		if ( s == "end" )
			break;
		for(int i = 0 ;i < s.length(); i++)
		{
			a=a+ pow((double)7,(double)i) *((int)s[s.length()-i-1]-48);
		}
		printf("	d:%d\n	h:%x",a,a);
	}
	return 0;
}

