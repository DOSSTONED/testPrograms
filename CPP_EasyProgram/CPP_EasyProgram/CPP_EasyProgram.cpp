// CPP_EasyProgram.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <iostream>
#include <string>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int a(0),b(0);
	string str_a;

	a = getchar();
	putchar(a);

	

	cout<<"Input the string:"<<endl;
	cin>>str_a;
	cout<<"The string you input is:"<<endl<<str_a<<endl;
	cout<<"Test complete!"<<endl;
	
	_flushall();
	
	
	return 0;
}

