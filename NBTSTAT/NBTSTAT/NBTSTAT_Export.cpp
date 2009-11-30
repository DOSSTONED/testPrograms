// NBTSTAT.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	for(int  i= 2 ; i < 255 ; i++)
	{
		string s;
		char temp[10];
		sprintf_s(temp, "%d", i);
		s="c:\\windows\\system32\\nbtstat.exe -a 10.21.5." + string(temp) + " >10.21.5." + string(temp) + ".txt";
		
		system(s.c_str());
		cout<<s<<"\t Finished\n";
		
	}
	system("pause");
	return 0;
}

